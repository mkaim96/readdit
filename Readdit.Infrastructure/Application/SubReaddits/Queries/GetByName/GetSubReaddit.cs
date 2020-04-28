using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddits.Queries.GetByName
{
    public class GetSubReadditByName : IRequest<SubReadditDto>
    {
        public string Name { get; set; }
    }

    public class GetSubReadditByNameHandler : IRequestHandler<GetSubReadditByName, SubReadditDto>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetSubReadditByNameHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubReadditDto> Handle(GetSubReadditByName request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }

            return await _context.SubReaddits
                .ProjectTo<SubReadditDto>(_mapper.ConfigurationProvider)
                .FirstAsync(x => x.Name == request.Name);
                
        }
    }
}
