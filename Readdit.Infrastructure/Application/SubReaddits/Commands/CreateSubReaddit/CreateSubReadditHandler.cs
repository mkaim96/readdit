using MediatR;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Readdit.Domain.Models;
using Readdit.Domain.Interfaces;

namespace Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit
{
    class CreateSubReadditHandler : IRequestHandler<CreateSubReadditCommand, int>
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
