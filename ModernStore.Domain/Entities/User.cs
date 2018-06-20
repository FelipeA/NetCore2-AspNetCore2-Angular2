using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Entities
{
    public class User : Shared.Entities.Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }

        protected User() { }

        public User(string username, string password, string confirmPassword)
        {
            Username = username;
            Password = EncryptPassword(password);
            Active = true;

            if (password != confirmPassword)
                AddNotification("Password", "Confirmação de password inválido!");
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public bool Authenticate(string userName, string password)
        {
            if (Username == userName && Password == EncryptPassword(password))
                return true;

            AddNotification("User", "Usuário ou senha inválidos");
            return false;
        }

        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";

            var password = (pass += "9435DD7D-8719-40D7-B6F9-69EE8494461F");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();

            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
