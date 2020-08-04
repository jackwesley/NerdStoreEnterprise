using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Core.DomainObjects;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pedidos.Domain.Pedidos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Pedido.API.Services
{
    public class PedidoIntegrationHandler : BackgroundService
    {

        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PedidoIntegrationHandler(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _bus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();

            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<PedidoCanceladoIntegrationEvent>("PedidoCancelado",
                async request => await CancelarPedido(request));

            _bus.SubscribeAsync<PedidoBaixadoEstoqueIntegrationEvent>("PedidoPago",
                async request => await FinalizarPedido(request));
        }

        private async Task FinalizarPedido(PedidoBaixadoEstoqueIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pedidoRepository = scope.ServiceProvider.GetRequiredService<IPedidoRepository>();

                var pedido = await pedidoRepository.ObterPorId(message.PedidoId);
                pedido.FinalizarPedido();

                pedidoRepository.Atualizar(pedido);

                if (!await pedidoRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Falha ao finalizar pedido {message.PedidoId}");
                }
            }
        }

        private async Task CancelarPedido(PedidoCanceladoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pedidoRepository = scope.ServiceProvider.GetRequiredService<IPedidoRepository>();

                var pedido = await pedidoRepository.ObterPorId(message.PedidoId);
                pedido.CancelarPedido();

                pedidoRepository.Atualizar(pedido);

                if (!await pedidoRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Falha ao cancelar pedido {message.PedidoId}");
                }
            }
        }
    }
}
