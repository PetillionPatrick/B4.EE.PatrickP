using FluentValidation;
using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.Validators
{
    public class LiValidator : AbstractValidator<LineInspector>
    {
        public LiValidator()
        {
            RuleFor(item => item.Naam)
                .NotEmpty()
                .WithMessage("LineInspector kan niet leeg zijn.");
        }
    }
}
