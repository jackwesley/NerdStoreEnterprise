﻿using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Pagamento.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Transacao>> ObterTransacoesPorPedidoId(Guid pedidoId)
        {
            return await _context.Transacoes.AsNoTracking()
                .Where(t => t.Pagamento.PedidoId == pedidoId).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }
    }
}
