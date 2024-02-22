﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;

namespace JwtStore.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            #region Create
            builder.Services.AddTransient<
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
                JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Repository>();

            builder.Services.AddTransient<
                JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
                JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Service>();
            #endregion

            #region Authenticate
            builder.Services.AddTransient<
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
                JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository>();
            #endregion
        }

        public static void MapAccountEndponints(this WebApplication app)
        {
            #region Authenticate
            app.MapPost("api/v1/authenticate", async (
                JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
                IRequestHandler<
                    JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
                    JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());
                if (!result.IsSuccess)
                    return Results.Json(result, statusCode: result.Status);

                if (result.Data is null)
                    return Results.Json(result, statusCode: 500);

                result.Data.Token = JwtExtension.Generate(result.Data);
                return Results.Ok(result);
            })
                .RequireAuthorization();
            #endregion
        }
    }
}
