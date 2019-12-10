using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VueTest.Data.Interfaces;
using VueTest.Data.Models;

namespace VueTest.Data.DataAccessObjects.Postgres
{
    public class UserDao : BaseDao, IUserDao
    {
        public UserDao(string connectionString, ILogger logger) : base(connectionString, logger) { }

        public async Task Create(User model)
        {
            try
            {
                _logger.LogInformation("Trying to execute sql create user query");
                model.Id = await QuerySingleOrDefaultAsync<int>($@"
                        insert into {"\"Users\""} (
                            {"\"Firstname\""},
                            {"\"Lastname\""},
                            {"\"Email\""},
                            {"\"Gender\""},
                            {"\"Age\""}
                        ) values (
                            @Firstname,
                            @Lastname,
                            @Email,
                            @Gender,
                            @Age
                        )
                        returning {"\"Id\""};
                ", model);
                _logger.LogInformation("Sql create user query successfully executed");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }

        public async Task Delete(IReadOnlyList<int> ids)
        {
            try
            {
                _logger.LogInformation("Trying to execute sql delete users query");
                await ExecuteAsync("delete from \"Users\" where \"Id\" = any(@ids)", new { ids });
                _logger.LogInformation("Sql delete users query successfully executed");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }

        public async Task<IEnumerable<User>> Get(UserGetOptions options)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                _logger.LogInformation("Try to create get users sql query");

                sql.AppendLine($@"
                    select 
                        {"\"Id\""},
                        {"\"Firstname\""},
                        {"\"Lastname\""},
                        {"\"Email\""},
                        {"\"Gender\""},
                        {"\"Age\""}
                    from {"\"Users\""}
                ");

                int conditionIndex = 0;
                if (options.Id.HasValue)
                {
                    sql.AppendLine($"{(conditionIndex++ == 0 ? "where" : "and")} \"Id\" = @Id");
                }
                if (options.Ids != null)
                {
                    sql.AppendLine($"{(conditionIndex++ == 0 ? "where" : "and")} \"Id\" = any(@Ids)");
                }
                if (!string.IsNullOrEmpty(options.NormalizedSearch))
                {
                    sql.AppendLine($@"
                        {(conditionIndex++ == 0 ? "where" : "and")} lower({"\"Firstname\""}) like lower(@NormalizedSearch)
                        or lower({"\"Lastname\""}) like lower(@NormalizedSearch)
                        or lower({"\"Email\""}) like lower(@NormalizedSearch)
                    ");
                }
                _logger.LogInformation($"Sql query successfully created:\n{sql.ToString()}");

                _logger.LogInformation("Try to execute sql get users query");
                var result = await QueryAsync<User>(sql.ToString(), options);
                _logger.LogInformation("Sql get users query successfully executed");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }

        public async Task Update(User model)
        {
            try
            {
                _logger.LogInformation("Trying to execute sql update user query");
                await ExecuteAsync($@"
                    update {"\"Users\""} set
                        {"\"Firstname\""} = @Firstname, 
                        {"\"Lastname\""} = @Lastname, 
                        {"\"Email\""} = @Email,
                        {"\"Gender\""} = @Gender,
                        {"\"Age\""} = @Age
                    where {"\"Id\""} = @Id
                ", model);
                _logger.LogInformation("Sql update user query successfully executed");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }
    }
}