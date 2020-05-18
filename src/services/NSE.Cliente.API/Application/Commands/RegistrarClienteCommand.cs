using FluentValidation;
using NSE.Core.DomainObjects;
using NSE.Core.Messages;
using System;
using System.IO.Pipelines;

namespace NSE.Cliente.API.Application.Commands
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            AgregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);
            return ValidationResult.IsValid; ;
        }
    }

    public class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("Nome do cliente informado");

            RuleFor(c => c.Cpf)
                .Must(TerCpfValido)
                .WithMessage("CPF informado não é válido");

            RuleFor(c => c.Email)
                .Must(TerEmailValido)
                .WithMessage("E-mail informado não é válido");
        }

        protected static bool TerCpfValido(string cpf)
        {
            return Cpf.Validar(cpf);
        }

        protected static bool TerEmailValido(string email)
        {
            return Email.Validar(email);
        }
    }
}
