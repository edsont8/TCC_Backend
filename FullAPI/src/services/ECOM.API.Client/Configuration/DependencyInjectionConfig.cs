﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ECOM.API.Client.Application.Commands;
using ECOM.API.Client.Models;
using ECOM.Core.Mediator;
using FluentValidation.Results;
using ECOM.API.Client.Data.Repository;
using Application.Commands;
using Data;
using ECOM.API.Client.Application.Events;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;
using ECOM.WebAPI.Core.Usuario;

namespace ECOM.API.Client.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarEnderecoCommand, ValidationResult>, ClientCommandHandler>();
            
            services.AddScoped<INotificationHandler<RegisteredClientEvent>, ClientEventHandler>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ClientsContext>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}