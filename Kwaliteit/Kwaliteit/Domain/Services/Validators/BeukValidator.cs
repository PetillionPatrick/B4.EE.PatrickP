using FluentValidation;
using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.Validators
{
    public class BeukValidator : AbstractValidator<Beuk>
    {
        public BeukValidator()
        {
            RuleFor(item => item.Naam)
                .NotEmpty()
                .WithMessage("Beuk naam kan niet leeg zijn.");
        }
    }
}
