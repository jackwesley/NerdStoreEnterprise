using NetDevPack.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Pedidos.Domain.Pedidos
{
    public interface IPedidosRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clientId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);

        /*Pedido Item*/
        Task<PedidoItem> ObterItemPorId(Guid id);
        Task<PedidoItem> ObnterItemPorPedido(Guid pedidoId, Guid produtoId);
    }
}
