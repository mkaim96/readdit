using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddit.Queries.GetPopular
{
    public class GetPopularSubReadditsHandler : IRequestHandler<GetPopularSubReaddits, IReadOnlyCollection<SubReadditDto>>
    {
        private readonly ISubReadditRepository _subReadditRepository;

        public GetPopularSubReadditsHandler(ISubReadditRepository subReadditRepository)
        {
            _subReadditRepository = subReadditRepository;
        }

        public async Task<IReadOnlyCollection<SubReadditDto>> Handle(GetPopularSubReaddits request, CancellationToken cancellationToken)
        {
            var subReds = await _subReadditRepository.GetPopular();

            return subReds.Select(x => new SubReadditDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList().AsReadOnly();
        }
    }

}
