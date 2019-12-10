using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;
using VueTest.Data.Interfaces;
using VueTest.Data.Models;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Text;

namespace VueTest.Services
{
    [ServiceContract]
    public class UserService
    {
        private IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao ?? throw new ArgumentException(nameof(userDao));
        }

        [OperationContract]
        public async Task<User> Create(User model)
        {
            string message = Validate(model);
            if(!string.IsNullOrEmpty(message))
            {
                throw new Exception(message);
            }
            await _userDao.Create(model);
            return model;
        }

        [OperationContract]
        public async Task Delete(IReadOnlyList<int> ids) => await _userDao.Delete(ids);

        [OperationContract]
        public async Task<IEnumerable<User>> Get(UserGetOptions options) => await _userDao.Get(options);

        [OperationContract]
        public async Task<User> Update(User model)
        {
            string message = Validate(model);
            if(!string.IsNullOrEmpty(message))
            {
                throw new Exception(message);
            }
            await _userDao.Update(model);
            return model;
        }

        public string Validate(User model)
        {
            StringBuilder builder = new StringBuilder();
            if (model.Age < 0 || model.Age > 100)
                builder.AppendLine("Возраст пользователя должен быть больше 0 и меньше 100.");
            if (string.IsNullOrEmpty(model.Email))
                builder.AppendLine("Email обязателен.");
            if (string.IsNullOrEmpty(model.Firstname) || model.Firstname.Length < 3)
                builder.AppendLine("Имя пользователя обязательно и должно быть длиннее 3 символов");
            if (string.IsNullOrEmpty(model.Lastname) || model.Lastname.Length < 3)
                builder.AppendLine("Фамилия пользователя обязательна и должна быть длиннее 3 символов");

            return builder.ToString();
        }
    }
}