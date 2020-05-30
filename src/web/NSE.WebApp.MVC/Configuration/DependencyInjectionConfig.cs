using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebAPI.Core.Usuario;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                //police from polly
                //.AddTransientHttpErrorPolicy(
                //p => p.WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: _ => TimeSpan.FromMilliseconds(600)))
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 5,
                        durationOfBreak: TimeSpan.FromSeconds(30)));
           
            #region Refit
            //services.AddHttpClient(name: "Refit", opions =>
            //     {
            //         opions.BaseAddress = new Uri(uriString: configuration.GetSection(key: "CatalogoUrl").Value);
            //     })
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
        }

    }

    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            var retry = HttpPolicyExtensions
              .HandleTransientHttpError()
              .WaitAndRetryAsync(sleepDurations: new[]
              {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
              }, onRetry: (outcomeType, timespan, retryCount, context) =>
              {
                  Console.ForegroundColor = ConsoleColor.Blue;
                  Console.WriteLine($"Tentando pela {retryCount} vez!");
                  Console.ForegroundColor = ConsoleColor.White;
              });

            return retry;
        }
    }
}
