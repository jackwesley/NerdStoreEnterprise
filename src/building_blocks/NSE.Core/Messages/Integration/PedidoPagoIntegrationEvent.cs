using System;

namespace NSE.Core.Messages.Integration
{
    public class PedidoPagoIntegrationEvent : IntegrationEvent
    {
        public PedidoPagoIntegrationEvent(Guid clienteId, Guid pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }

        public Guid ClienteId { get; set; }
        public Guid PedidoId { get; set; }

    }
}
