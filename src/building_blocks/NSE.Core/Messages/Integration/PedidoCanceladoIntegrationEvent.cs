using System;

namespace NSE.Core.Messages.Integration
{
    public class PedidoCanceladoIntegrationEvent : IntegrationEvent
    {
        public PedidoCanceladoIntegrationEvent(Guid clientId, Guid pedidoId)
        {
            ClientId = clientId;
            PedidoId = pedidoId;
        }

        public Guid ClientId { get; set; }
        public Guid PedidoId { get; set; }

        
    }
}
