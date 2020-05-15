using NSE.Core.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(string name, string email, string cpf)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Excluido = false;

        }

        //Entity Framework(EF) Relation
        protected Cliente(){}

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }
    }
}
