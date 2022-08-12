using MediatR;
using System;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Queries
{
    public class GetChildByIdQuery : Query, IRequest<GetChildByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetChildByIdQueryResponse
    {
        public ChildResponseDto Child { get; set; }

        public GetChildByIdQueryResponse(ChildResponseDto child)
        {
            Child = child;
        }
    }
}
