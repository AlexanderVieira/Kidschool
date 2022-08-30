using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.Queries.Handlers
{
    public class ChildrenPagedQueryHandler : IRequestHandler<GetChildrenPagedQuery, GetChildrenPagedQueryResponse>
    {
        private readonly IChildQueries _childQueries;
        private readonly IMapper _mapper;

        public ChildrenPagedQueryHandler(IChildQueries childQueries, IMapper mapper)
        {
            _childQueries = childQueries;
            _mapper = mapper;
        }

        public async Task<GetChildrenPagedQueryResponse> Handle(GetChildrenPagedQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _childQueries.GetChildren(request.PageSize, request.PageIndex, request.Query);

            var childrenDto = new List<ChildDesignedQueryResponseDto>();
            var pagedResultDto = new PagedResult<ChildDesignedQueryResponseDto>
            {
                List = new List<ChildDesignedQueryResponseDto>(),
                PageIndex = pagedResult.PageIndex,
                PageSize = pagedResult.PageSize,
                Query = pagedResult.Query,
                TotalResults = pagedResult.TotalResults
            };

            foreach (var child in pagedResult.List)
            {
                var childResponse = _mapper.Map<ChildDesignedQueryResponseDto>(child);
                childrenDto.Add(childResponse);
            }

            pagedResultDto.List = childrenDto;
            var response = new GetChildrenPagedQueryResponse(pagedResultDto);

            return response;
        }
    }
}
