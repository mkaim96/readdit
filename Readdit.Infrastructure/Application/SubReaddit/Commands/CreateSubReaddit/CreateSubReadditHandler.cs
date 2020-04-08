using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddit.Commands.CreateSubReaddit
{
    class CreateSubReadditHandler : IRequestHandler<CreateSubReadditCommand, int>
    {
        public CreateSubReadditHandler()
        {

        }

        public Task<int> Handle(CreateSubReadditCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
