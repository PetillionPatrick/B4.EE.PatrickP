using FluentValidation;
using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.Validators
{
        public class UnitValidator : AbstractValidator<Unit>
        {
            public UnitValidator()
            {
                RuleFor(item => item.Naam)
                    .NotEmpty()
                    .WithMessage("unit kan niet leeg zijn.");
            }
        }
}
