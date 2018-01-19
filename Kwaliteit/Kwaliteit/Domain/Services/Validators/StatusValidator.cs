using FluentValidation;
using Kwaliteit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Services.Validators
{

    public class StatusValidator : AbstractValidator<Status>
    {
        public StatusValidator()
        {
            RuleFor(item => item.ArchiefNr)
                .NotEmpty()
                .WithMessage("archienummer kan niet leeg zijn.");

            RuleFor(item => item.PartNummer)
                   .NotEmpty()
                   .WithMessage("partnummer kan niet leeg zijn.");

            RuleFor(item => item.ReparatieNummer)
                   .NotEmpty()
                   .WithMessage("reparatienummer kan niet leeg zijn.");

            RuleFor(item => item.VormNr)
                   .NotEmpty()
                   .WithMessage("vormnummer kan niet leeg zijn.");
        }
    }
}
