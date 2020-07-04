using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Cliente.API.Models;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;
using System;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAspNetUser _user;
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IMediatorHandler mediatorHandler, IAspNetUser user, IClienteRepository clienteRepository)
        {
            _mediatorHandler = mediatorHandler;
            _user = user;
            _clienteRepository = clienteRepository;
        }

        [HttpGet("cliente/endereco")]
        public async Task<IActionResult> ObterEndereco()
        {
           var endereco =  await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("cliente/endereco")]
        public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoCommand endereco)
        {
            endereco.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediatorHandler.EnviarComando(endereco));
        }
    }
}