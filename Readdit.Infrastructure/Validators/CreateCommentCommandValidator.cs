using FluentValidation;
using Readdit.Infrastructure.Application.Comments.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Comment cannot be empty");
        }
    }
}
