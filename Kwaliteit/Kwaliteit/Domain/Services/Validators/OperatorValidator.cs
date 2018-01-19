using FluentValidation;
using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.Validators
{

        public class OperatorValidator : AbstractValidator<Operator>
        {
            public OperatorValidator()
            {
                RuleFor(item => item.Naam)
                    .NotEmpty()
                    .WithMessage("Operator kan niet leeg zijn.");
            }
        }

}
