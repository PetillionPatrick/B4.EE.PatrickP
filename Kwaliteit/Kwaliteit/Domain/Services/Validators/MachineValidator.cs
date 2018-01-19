using FluentValidation;
using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.Validators
{
    public class MachineValidator : AbstractValidator<Machine>
    {
        public MachineValidator()
        {
            RuleFor(item => item.Naam)
                .NotEmpty()
                .WithMessage("Beuk naam kan niet leeg zijn.");
        }
    }
}
