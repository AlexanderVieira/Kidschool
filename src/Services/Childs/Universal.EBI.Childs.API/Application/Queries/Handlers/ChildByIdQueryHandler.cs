using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;

namespace Universal.EBI.Childs.API.Application.Queries.Handlers
{
    public class ChildByIdQueryHandler : IRequestHandler<GetChildByIdQuery, GetChildByIdQueryResponse>
    {
        private readonly IChildQueries _childQueries;
        private readonly IMapper _mapper;

        public ChildByIdQueryHandler(IChildQueries childQueries, IMapper mapper)
        {
            _childQueries = childQueries;
            _mapper = mapper;
        }

        public async Task<GetChildByIdQueryResponse> Handle(GetChildByIdQuery request, CancellationToken cancellationToken)
        {
            var child = await _childQueries.GetChildById(request.Id);
            var childDto = _mapper.Map<ChildResponseDto>(child);
            return new GetChildByIdQueryResponse(childDto);
        }
    }
}
