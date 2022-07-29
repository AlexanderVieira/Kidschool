using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Childs.API.Application.Commands;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;
using Universal.EBI.Childs.API.Application.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace Universal.EBI.Childs.API.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IChildQueries _childQueries;

        public ChildController(IMediatorHandler mediator, IAspNetUser user, IChildQueries childQueries)
        {
            _mediator = mediator;
            _user = user;
            _childQueries = childQueries;
        }

        [HttpGet("api/childs")]
        public async Task<PagedResult<ChildDto>> GetChilds([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResult = await _childQueries.GetChilds(ps, page, q);
            //var pagedResultDto = new PagedResult<ChildDto>();
            var childrenDto = new List<ChildDto>();
            ResponsibleDto responsibleDto;
            var pagedResultDto = new PagedResult<ChildDto>
            {
                List = new List<ChildDto>(),
                PageIndex = pagedResult.PageIndex,
                PageSize = pagedResult.PageSize,
                Query = pagedResult.Query,
                TotalResults = pagedResult.TotalResults
            };
            
            for (int i = 0; i < pagedResult.List.ToList().Count; i++)
            {
                var item = pagedResult.List.ToList()[i];
                var responsibles = item.Responsibles.ToList();
                var childDto = new ChildDto
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    FullName = item.FullName,
                    BirthDate = item.BirthDate.Date.ToShortDateString(),
                    GenderType = item.GenderType.ToString(),
                    AgeGroupType = item.AgeGroupType.ToString(),
                    Cpf = item.Cpf != null ? item.Cpf.Number : null,
                    Email = item.Email != null ? item.Email.Address : null,
                    Excluded = item.Excluded,
                    Address = new AddressDto(),
                    Phones = new List<PhoneRequestDto>(),
                    PhotoUrl = item.PhotoUrl,
                    Responsibles = new List<ResponsibleDto>()

                };

                for (int j = 0; j < responsibles.Count; j++)
                {
                    responsibleDto = new ResponsibleDto
                    {
                        Id = responsibles[j].Id,
                        FirstName = responsibles[j].FirstName,
                        LastName = responsibles[j].LastName,
                        FullName = responsibles[j].FullName,
                        BirthDate = responsibles[j].BirthDate.ToShortDateString(),
                        Cpf = responsibles[j].Cpf.Number,
                        Email = responsibles[j].Email.Address,
                        GenderType = responsibles[j].GenderType.ToString(),
                        KinshipType = responsibles[j].KinshipType.ToString(),
                        Excluded = responsibles[j].Excluded,
                        PhotoUrl = responsibles[j].PhotoUrl,
                        Address = new AddressDto
                        {
                            Id = responsibles[j].Address.Id,
                            PublicPlace = responsibles[j].Address.PublicPlace,
                            Number = responsibles[j].Address.Number,
                            Complement = responsibles[j].Address.Complement,
                            District = responsibles[j].Address.District,
                            City = responsibles[j].Address.City,
                            State = responsibles[j].Address.State,
                            Country = responsibles[j].Address.Country,
                            ZipCode = responsibles[j].Address.ZipCode
                        },
                        Phones = new List<PhoneRequestDto>()
                    };

                    var phones = responsibles[j].Phones.ToList();
                    for (int k = 0; k < phones.Count; k++)
                    {
                        var phoneDto = new PhoneRequestDto
                        {
                            Id = phones[k].Id,
                            Number = phones[k].Number,
                            PhoneType = phones[k].PhoneType.ToString()
                        };
                        responsibleDto.Phones.Add(phoneDto);
                    }

                    childDto.Responsibles.Add(responsibleDto);

                }

                childrenDto.Add(childDto);

            }

            pagedResultDto.List = childrenDto;

            return pagedResultDto;
        }

        [HttpGet("api/child/{id}")]
        public async Task<IActionResult> GetChildById(Guid id)
        {
            var child = await _childQueries.GetChildById(id);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("api/child/{cpf:length(11)}", Name = "GetChildByCpf")]
        public async Task<ActionResult> GetChildByCpf(string cpf)
        {
            var child = await _childQueries.GetChildByCpf(cpf);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpPost("api/child/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();
                request.CreatedBy = _user.GetUserEmail();
                var command = new RegisterChildCommand(request);
                return CustomResponse(await _mediator.SendCommand(command));
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
            
        }
        
        [HttpPut("api/child/update")]
        public async Task<IActionResult> UpdateChild([FromBody] UpdateChildCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpDelete("api/child/delete/{id}")]
        public async Task<IActionResult> DeleteChild(Guid id)
        {
            var command = new DeleteChildCommand { Id = id };
            return CustomResponse(await _mediator.SendCommand(command));
        }
       
    }
}
