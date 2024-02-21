using JwtStore.Core.Context.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings.UseCases.Create
{
    public class Service : IService
    {
        public Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
