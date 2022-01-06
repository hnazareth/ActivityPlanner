
using ActivityPlanner.API.Features.Properties.Commands;
using ActivityPlanner.API.Features.Properties.Queries;
using ActivityPlanner.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Unit>> Create(CreateProperty.CreatePropertyCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpPost]
        [Route("DisableProperty")]
        public async Task<ActionResult<Unit>> Disable(DisableProperty.DisablePropertyCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("GetProperties")]
        public async Task<ActionResult<List<PropertyDTO>>> GetProperties()
        {
            return await _mediator.Send(new GetAllProperties.GetAllPropertiesCmd());
        }

        [HttpGet("GetProperty/{property_id}")]
        public async Task<ActionResult<PropertyDTO>> GetProperty(int property_id)
        {
            return await _mediator.Send(new GetPropertyByID.GetPropertyByIDCmd { property_id = property_id });
        }
    }
}
