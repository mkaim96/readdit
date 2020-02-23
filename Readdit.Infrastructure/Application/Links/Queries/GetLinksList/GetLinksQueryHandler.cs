﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksList
{
    public class GetLinksQueryHandler : IRequestHandler<GetLinksListQuery, IReadOnlyList<LinkDto>>
    {
        private ILinksRepository _linksRepository;
        private IMapper _mapper;

        public GetLinksQueryHandler(ILinksRepository linksRepo, IMapper mapper)
        {
            _linksRepository = linksRepo;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<LinkDto>> Handle(GetLinksListQuery request, CancellationToken cancellationToken)
        {
            var links = await _linksRepository.GetAll();

            return _mapper.Map<IReadOnlyList<LinkDto>>(links);
        }
    }
}
