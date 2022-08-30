using MediatR;
using Universal.EBI.Childs.API.Application.DTOs;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Queries
{
    public class GetChildByCpfQuery : Query, IRequest<GetChildByCpfQueryResponse>
    {
        public string Cpf { get; set; }
    }

    public class GetChildByCpfQueryResponse
    {
        public ChildResponseDto Child { get; set; }

        public GetChildByCpfQueryResponse(ChildResponseDto child)
        {
            Child = child;
        }
    }
}
