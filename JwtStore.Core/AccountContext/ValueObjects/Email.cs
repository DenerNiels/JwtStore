using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JwtStore.Core.AccountContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public Email(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new Exception("Email inválido");

            Address = address.Trim().ToLower();

            if (Address.Length < 5)
                throw new Exception("Email inválido");

            if (!EmailRegex().IsMatch(address))
                throw new Exception("Email inválido");
        }

        public string Address { get; }
        public string Hash => Address.ToBase64();
        public Verification Verification { get; private set; } = new();

        public void ResendVerification()
        {
            Verification = new Verification();
        }



        public static implicit operator string(Email email)
            => email.ToString();

        public static implicit operator Email(string address)
            => new Email(address);

        public override string ToString()
            => Address;
        

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}
