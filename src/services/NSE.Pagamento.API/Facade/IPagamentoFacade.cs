using NSE.Core.Messages.Integration;
using NSE.Pagamento.API.Models;
using System;
using System.Threading.Tasks;

namespace NSE.Pagamento.API.Facade
{
    public interface IPagamentoFacade
    {
        Task<Transacao> AutorizarPagamento(NSE.Pagamento.API.Models.Pagamento pagamento);
        Task<Transacao> CapturarPagamento(Transacao pedidoId);
        Task<Transacao> CancelarAutorizacao(Transacao pedidoId);
    }
}
