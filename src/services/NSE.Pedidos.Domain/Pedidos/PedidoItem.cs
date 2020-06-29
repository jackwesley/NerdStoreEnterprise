using NSE.Core.DomainObjects;
using System;

namespace NSE.Pedidos.Domain.Pedidos
{
    public class PedidoItem : Entity
    {
        public PedidoItem(Guid produtoId, string produtoName, int quantidade, decimal valorUnitario, string produtoImagem)
        {
            ProdutoId = produtoId;
            ProdutoName = produtoName;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ProdutoImagem = produtoImagem;
        }

        //EF Ctor
        public PedidoItem(){}

        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public string ProdutoName { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string ProdutoImagem { get; set; }

        //EF Rel.
        public Pedido Pedido { get; set; }

        
        internal decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
