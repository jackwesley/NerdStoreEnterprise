﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Usuario;


namespace NSE.Pagamento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            //Commands
            

            //Events
            


            //Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

        }
    }
}
