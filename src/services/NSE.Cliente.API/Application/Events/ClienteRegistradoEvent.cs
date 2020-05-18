using NSE.Core.Messages;
using System;

namespace NSE.Cliente.API.Application.Events
{
    public class ClienteRegistradoEvent : Event
    {
        public ClienteRegistradoEvent(Guid id, string name, string email, string cpf)
        {
            AgregateId = id;
            Id = id;
            Nome = name;
            Email = email;
            Cpf = cpf;

        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

    }
}
