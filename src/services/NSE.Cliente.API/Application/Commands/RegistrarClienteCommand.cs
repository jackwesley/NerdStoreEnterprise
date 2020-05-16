using NSE.Core.DomainObjects;
using NSE.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Application.Commands
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }

        public RegistrarClienteCommand(Guid id, string nome, Email email, Cpf cpf)
        {
            AgregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }


    }
}
