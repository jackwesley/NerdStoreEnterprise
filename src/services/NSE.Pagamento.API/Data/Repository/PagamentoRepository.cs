using NSE.Core.Data;
using NSE.Pagamento.API.Models;


namespace NSE.Pagamento.API.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentosContext _context;
        public PagamentoRepository(PagamentosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void AdicionarPagamento(NSE.Pagamento.API.Models.Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
