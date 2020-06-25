using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Mediator;
using NSE.Pedidos.Infra.Data;



namespace NSE.Pedido.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<PedidosContext>();
        }
    }
}
