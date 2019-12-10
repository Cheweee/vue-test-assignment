using System.Collections.Generic;
using System.Threading.Tasks;
using VueTest.Data.Models;

namespace VueTest.Data.Interfaces
{
    public interface IUserDao
    {
        Task Create(User model);
        Task Delete(IReadOnlyList<int> ids);
        Task<IEnumerable<User>> Get(UserGetOptions options);
        Task Update(User model);
    }
}