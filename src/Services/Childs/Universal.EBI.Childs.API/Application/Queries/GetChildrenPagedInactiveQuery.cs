using MediatR;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Queries
{
    public class GetChildrenPagedInactiveQuery : Query, IRequest<GetChildrenPagedInactiveQueryResponse>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }

    public class GetChildrenPagedInactiveQueryResponse
    {
        public PagedResult<ChildDesignedQueryResponseDto> pagedResult { get; set; }

        public GetChildrenPagedInactiveQueryResponse(PagedResult<ChildDesignedQueryResponseDto> pagedResult)
        {
            this.pagedResult = pagedResult;
        }
    }
}
