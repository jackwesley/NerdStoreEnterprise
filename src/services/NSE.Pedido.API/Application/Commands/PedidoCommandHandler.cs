using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.API.Application.Events;
using NSE.Pedidos.Domain.Pedidos;
using NSE.Pedidos.Domain.Vouchers;
using NSE.Pedidos.Domain.Vouchers.Specs;
using NSE.Pedidos.Infra.Data.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Pedido.API.Application.Commands
{
    public class PedidoCommandHandler : CommandHandler, IRequestHandler<AdicionarPedidoCommand, ValidationResult>
    {

        private readonly IVoucherRepository _voucherRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IVoucherRepository voucherRepository, IPedidoRepository pedidoRepository)
        {
            _voucherRepository = voucherRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoCommand message, CancellationToken cancellationToken)
        {
            //Validacao do comando
            if (!message.EhValido()) return message.ValidationResult;

            //Mapear Pedido
            var pedido = MapearPedido(message);

            //Aplicar voucher se houver
            if (!await AplicarVoucher(message, pedido)) return ValidationResult;

            //Validar pedido
            if (!ValidarPedido(pedido)) return ValidationResult;

            //Processar pagamento
            if (!ProcessarPagamento(pedido)) return ValidationResult;

            //Se pagamento tudo ok
            pedido.AutorizarPedido();

            //Adicionar Evento
            pedido.AdicionarEvento(new PedidoRealizadoEvent(pedido.Id, pedido.ClienteId));

            //Adicionar pedido repositorio
            _pedidoRepository.Adicionar(pedido);

            //Persistir dados de pedido e voucher
            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        private NSE.Pedidos.Domain.Pedidos.Pedido MapearPedido(AdicionarPedidoCommand message)
        {
            var endereco = new Endereco
            {
                Logradouro = message.Enderedo.Logradouro,
                Numero = message.Enderedo.Numero,
                Complemento = message.Enderedo.Complemento,
                Bairro = message.Enderedo.Bairro,
                Cep = message.Enderedo.Cep,
                Cidade = message.Enderedo.Cidade,
                Estado = message.Enderedo.Estado
            };

            var pedido = new NSE.Pedidos.Domain.Pedidos.Pedido(message.ClienteId, message.ValorTotal, message.PedidoItems.Select(PedidoItemDTO.ParaPedidoItem).ToList(), message.VoucherUtilizado, message.Desconto);

            pedido.AtribuirEndereco(endereco);

            return pedido;
        }

        private async Task<bool> AplicarVoucher(AdicionarPedidoCommand message, NSE.Pedidos.Domain.Pedidos.Pedido pedido)
        {
            if (!message.VoucherUtilizado) return true;

            var voucher = await _voucherRepository.ObterVoucherPorCodigo(message.VoucherCodigo);
            if(voucher == null)
            {
                AdicionarErro("O voucher informado não existe!");
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);
            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(m => AdicionarErro(m.ErrorMessage));
                return false;
            }

            pedido.AtribuirVoucher(voucher);
            voucher.DebitarQuantidade();

            _voucherRepository.Atualizar(voucher);

            return false;
        }

        private bool ValidarPedido(NSE.Pedidos.Domain.Pedidos.Pedido pedido)
        {
            var pedidoValorOriginal = pedido.ValorTotal;
            var pedidoDesconto = pedido.Desconto;

            pedido.CalcularValorPedido();

            if(pedido.ValorTotal != pedidoValorOriginal)
            {
                AdicionarErro("O valor total do pedido nao confere com o cálculo do pedido");
                return false;
            }

            if(pedido.Desconto != pedidoDesconto)
            {
                AdicionarErro("O valor total não confere com o cálculo do pedido");
                return false;
            }

            return true;
        }


        public bool ProcessarPagamento(NSE.Pedidos.Domain.Pedidos.Pedido pedido)
        {
            return true;
        }
    }
}
