using Microsoft.VisualStudio.TestTools.UnitTesting;
using VueTest.Data.Enumerations;
using VueTest.Data.Interfaces;
using VueTest.Data.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace VueTest.Data.Tests
{
    [TestClass]
    public class UserDaoTests
    {
        private readonly ILogger _logger;
        private readonly IUserDao _dao;

        public UserDaoTests()
        {
            _logger = new LoggerFactory().AddConsole().CreateLogger<IDaoFactory>();
            var _daoFactory = DaoFactories.GetFactory(DatabaseProvider.Postgres, "host=localhost;port=5432;username=postgres;password=admin;database=vuetest", _logger);
            _dao = _daoFactory.UserDao;
        }

        [TestMethod]
        public async Task GetUsers()
        {
            var users = await _dao.Get(new UserGetOptions());
            var user = users.First();
            users = await _dao.Get(new UserGetOptions { Id = 2 });
            user = users.First();
            users = await _dao.Get(new UserGetOptions { Ids = new List<int> { 2 } });
            user = users.First();
            users = await _dao.Get(new UserGetOptions { Search = "some" });
            user = users.First();
        }

        [TestMethod]
        public async Task CreateUser()
        {
            User user = new User
            {
                Age = 10,
                Email = "some@email.com",
                Firstname = "some first name",
                Gender = Genders.Male,
                Lastname = "some last name"
            };


            await _dao.Create(user);
        }

        [TestMethod]
        public async Task UpdateUser()
        {
            var users = await _dao.Get(new UserGetOptions { Id = 2 });
            var user = users.First();
            user.Age = 10;
            user.Email = "some@email.com";
            user.Firstname = "some first name";
            user.Gender = Genders.Male;
            user.Lastname = "some last name";

            await _dao.Update(user);
        }

        [TestMethod]
        public async Task DeleteUser()
        {
            var users = await _dao.Get(new UserGetOptions());
            var user = users.OrderBy(o=> o.Id).Last();

            await _dao.Delete(new List<int> { user.Id });
        }
    }
}
