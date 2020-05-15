﻿using NSE.Core.DomainObjects;
using System;

namespace NSE.Cliente.API.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Excluido = false;

        }

        //Entity Framework(EF) Relation
        protected Cliente() { }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }

        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
    }
}
