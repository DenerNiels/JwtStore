using JwtStore.Core.SharedContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.AccountContext.ValueObjects
{
    public class Verification : ValueObject
    {
        public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? VerifiedAt { get; private set; } = null;
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;

        public void Verify(string code)
        {
            if (IsActive)
            {
                throw new Exception("este item ja esta ativo");
            }

            if (ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("este codigo ja expirou");
            }

            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase)) 
            {
                throw new Exception("Código de verificação inválido");
            }

            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}
