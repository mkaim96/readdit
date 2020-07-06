using FluentValidation;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Validators
{
    public class CreateCommunityCommandValidator : AbstractValidator<CreateCommunityCommand>
    {
        private ICommunityRepository _communityRepository;

        public CreateCommunityCommandValidator(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Description cannot empty");
            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellation) => await IsNameUnique(name))
                .WithMessage("Community with that name already exists");
        }

        private async Task<bool> IsNameUnique(string name)
        {
            var exists = await _communityRepository.Exists(name);

            return exists ? false : true;
        }
    }
}
