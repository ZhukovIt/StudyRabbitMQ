using StudyRabbitMQLogic;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace StudyRabbitMQ
{
    public class UserRepository
    {
        private readonly List<UserDAO> _users;
        private int _lastUserId;

        public UserRepository()
        {
            _users = new List<UserDAO>()
            {
                new UserDAO()
                {
                    Id = 1,
                    Login = "admin@ascon.ru",
                    Password = "d60870e0-c39b-40c7-bb27-dd40381a9f02"
                },

                new UserDAO()
                {
                    Id = 2,
                    Login = "zhukov_vs@ascon.ru",
                    Password = "416f3afd-5eb6-482d-ba72-873248efd627"
                }
            };

            _lastUserId = 2;
        }

        public Result<User> Get(long id)
        {
            UserDAO? existUser = _users.FirstOrDefault(u => u.Id == id);
            if (existUser == null)
                return Result.Failure<User>($"Пользователь с Id = {id} не существует!");

            return Result.Success(
                new User(Email.Create(existUser.Login).Value, Guid.Parse(existUser.Password)));
        }

        public IReadOnlyList<User> GetAll()
        {
            return _users
                .Select(u => new User(Email.Create(u.Login).Value, Guid.Parse(u.Password)))
                .ToList();
        }

        public void AddOrUpdate(User user)
        {
            UserDAO? existUser = _users.FirstOrDefault(u => u.Login == user.Login);
            if (existUser == null)
            {
                _lastUserId++;

                UserDAO newUser = new UserDAO()
                {
                    Id = _lastUserId,
                    Login = user.Login,
                    Password = user.Password.ToString()
                };

                _users.Add(newUser);
            }
            else
            {
                existUser.Login = user.Login;
                existUser.Password = user.Password.ToString();
            }
        }

        public Result Delete(long id)
        {
            UserDAO? existUser = _users.FirstOrDefault(u => u.Id == id);
            if (existUser == null)
                return Result.Failure("Пользователь не существует!");

            _users.Remove(existUser);

            return Result.Success();
        }
    }
}
