using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Carrinho.API.Model
{
    public class CarrinhoCliente
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal Valor { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
        public CarrinhoCliente(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
        }

        //For Entity Framework
        public CarrinhoCliente(){ }

    }
}
