using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;

namespace Universal.EBI.Childs.API.Application.Queries.Handlers
{
    public class ChildByCpfQueryHandler : IRequestHandler<GetChildByCpfQuery, GetChildByCpfQueryResponse>
    {
        private readonly IChildQueries _childQueries;
        private readonly IMapper _mapper;

        public ChildByCpfQueryHandler(IChildQueries childQueries, IMapper mapper)
        {
            _childQueries = childQueries;
            _mapper = mapper;
        }

        public async Task<GetChildByCpfQueryResponse> Handle(GetChildByCpfQuery request, CancellationToken cancellationToken)
        {
            var child = await _childQueries.GetChildByCpf(request.Cpf);
            var childDto = _mapper.Map<ChildResponseDto>(child);
            return new GetChildByCpfQueryResponse(childDto);
        }
    }
}
