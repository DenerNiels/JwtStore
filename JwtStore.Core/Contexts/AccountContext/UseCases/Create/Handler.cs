using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Context.AccountContext.Entities;
using JwtStore.Core.Context.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;


namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(
            Request request,
            CancellationToken cancellation)
        {
            
            #region 01. Valida a requisição
            try
            {
                var res = Specification.Ensure(request);
                if (!res.IsValid)
                    return new Response("Requisição inválida", 400,res.Notifications);
            }
            catch 
            {
                return new Response("Não foi possivel validar sua requisição", 500);
            }
            #endregion

            #region 02. Gerar os objetos
            Email email;
            Password password;
            User user;

            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, 400);
            }
            #endregion
        }
    }
}
