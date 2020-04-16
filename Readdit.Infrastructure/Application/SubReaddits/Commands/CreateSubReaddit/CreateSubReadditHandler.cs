using MediatR;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Readdit.Domain.Models;

namespace Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit
{
    class CreateSubReadditHandler : IRequestHandler<CreateSubReadditCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateSubReadditHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSubReadditCommand request, CancellationToken cancellationToken)
        {
            var subR = new Domain.Models.SubReaddit(request.Name);
            await _context.AddAsync(subR);

            return subR.Id;
        }
    }
}
