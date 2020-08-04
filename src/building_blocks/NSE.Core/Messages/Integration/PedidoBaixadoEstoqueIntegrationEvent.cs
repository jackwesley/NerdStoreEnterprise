using System;


namespace NSE.Core.Messages.Integration
{
    public class PedidoBaixadoEstoqueIntegrationEvent : IntegrationEvent
    {
        public PedidoBaixadoEstoqueIntegrationEvent(Guid clientId, Guid pedidoId)
        {
            ClientId = clientId;
            PedidoId = pedidoId;
        }

        public Guid ClientId { get; set; }
        public Guid PedidoId { get; set; }
    }
}
