using FluentValidation;
using Readdit.Infrastructure.Application.Links.Commands.CreateLink;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Validators
{
    public class CreateLinkCommandValidator : AbstractValidator<CreateLinkCommand>
    {
        public CreateLinkCommandValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url cannot be empty");
            RuleFor(x => x.Url).MinimumLength(4).WithMessage("Url is too short");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty");
        }
    }
}
