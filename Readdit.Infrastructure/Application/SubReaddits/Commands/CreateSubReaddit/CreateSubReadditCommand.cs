using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit
{
    public class CreateSubReadditCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class CreateSubReadditHandler : IRequestHandler<CreateSubReadditCommand, int>
    {
        private readonly ISubReadditRepository _subReadditRepository;

        public CreateSubReadditHandler(ISubReadditRepository subReadditRepository)
        {
            _subReadditRepository = subReadditRepository;
        }

        public async Task<int> Handle(CreateSubReadditCommand request, CancellationToken cancellationToken)
        {
            var subR = new Domain.Models.SubReaddit(request.Name, request.User, request.Description);
            await _subReadditRepository.Add(subR);

            return subR.Id;
        }
    }
}
