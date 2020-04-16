using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddit.Queries.Search
{
    public class SearchSubReadditsHandler : IRequestHandler<SearchSubReaddits, IReadOnlyCollection<SubReadditDto>>
    {
        private readonly ISubReadditRepository _subReadditRepository;

        public SearchSubReadditsHandler(ISubReadditRepository subReadditRepository)
        {
            _subReadditRepository = subReadditRepository;
        }
        public async Task<IReadOnlyCollection<SubReadditDto>> Handle(SearchSubReaddits request, CancellationToken cancellationToken)
        {
            var subReds = await _subReadditRepository.Search(request.SearchString);
            return subReds.Select(x => new SubReadditDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList().AsReadOnly();
        }
    }
}
