using FluentValidation;
using Readdit.Infrastructure.Application.SubReaddits.Commands.CreateLinkForSubReaddit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Validators
{
    public class CreateLinkForCommunityCommandValidator : AbstractValidator<CreateLinkCommand>
    {
        public CreateLinkForCommunityCommandValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url cannot be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty");
        }
    }
}
