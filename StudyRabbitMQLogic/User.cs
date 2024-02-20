using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyRabbitMQLogic
{
    public class User : Entity
    {
        private string _login;

        public Email Login
        {
            get => (Email)_login;
            set => _login = value;
        }

        private string _password;

        public Guid Password
        {
            get => Guid.Parse(_password);
            set => _password = value.ToString();
        }

        public User(Email login, Guid password)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));

            Login = login;
            Password = password;
        }
    }
}
