﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        public CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }
    }
}
