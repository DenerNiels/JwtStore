
using JwtStore.Core.Context.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository)
        {
            repository = _repository;
        }
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            #region 01. Valida a requisição
            try
            {
                var res = Specification.Ensure(request);
                if (!res.IsValid)
                    return new Response("Requisição inválida", 400, res.Notifications);
            }
            catch
            {
                return new Response("Não foi possível validar sua requisição", 500);
            }
            #endregion

            #region 02. Recupera o perfil
            User? user;
            try
            {
                user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
                if (user is null)
                    return new Response("Usuário não encontrado", 400);
            }
            catch
            {
                return new Response("Não foi possivel recuperar o seu perfil", 500);
            }
            #endregion

            #region 03. Checa se a senha é valida
            if (!user.Password.Challenge(request.Password))
                return new Response("Usuário ou senha inválidos", 400);
            #endregion

            #region 04. Checa se a conta está verificada

            try
            {
                if (!user.Email.Verification.IsActive)
                    return new Response("Conta inativa", 400);
            }
            catch
            {
                return new Response("Não foi possível verificar seu perfil", 500);
            }

            #endregion

            #region Recupera os perfis do usuário

            #endregion

            #region 05. Retorna os dados
            try
            {
                var data = new ResponseData
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = user.Roles.Select(x=> x.Name).ToArray()
                };

                return new Response(string.Empty, data);
            }
            catch
            {
                return new Response("Nao foi possivel obter os dados do perfil", 500);
            }
            #endregion
        }
    }
}
