using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public record Request(
        string Name,
        string Email,
        string Password
        ) : IRequest<Response>;
}
