using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Context.AccountContext.ValueObjects;
using JwtStore.Core.Context.SharedContext.Entities;
using JwtStore.Core.Contexts.AccountContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Context.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string name, Email email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public User(string email, string? password = null)
        {

            Email = email;
            Password = new Password(password);
        }

        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; private set; } = string.Empty;
        public IEnumerable<Role> Roles { get; set; } = Enumerable.Empty<Role>();

        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.OrdinalIgnoreCase))
                throw new Exception("Código de restauração inválido");
            var password = new Password(plainTextPassword);
            Password = password;
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        public void ChancePassword(string plainTextPassword)
        {
            var password = new Password(plainTextPassword);
            Password = password;
        }

    }
}
