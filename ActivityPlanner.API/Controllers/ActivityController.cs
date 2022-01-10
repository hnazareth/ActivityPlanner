using ActivityPlanner.API.Features.Activities.Commands;
using ActivityPlanner.API.Features.Activities.Queries;
using ActivityPlanner.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ResponseDTO>> Create(CreateActivity.CreateActivityCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpPost("Reschedule")]
        public async Task<ActionResult<ResponseDTO>> Reschedule(RescheduleActivity.RescheduleActivityCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpPost("Cancel")]
        public async Task<ActionResult<ResponseDTO>> Cancel(CancelActivity.CancelActivityCmd data)
        {
            return await _mediator.Send(data);
        }


        [HttpGet("GetActivities")]
        public async Task<ActionResult<List<ActivityDTO>>> GetProperties(DateTime? fechaInicial, DateTime? fechaFinal, string status = null)
        {
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/survey/GetSurvey";
            return await _mediator.Send(new GetActivities.GetActivitiesCmd { fecha_inicial = fechaInicial, fecha_final = fechaFinal, status = status, baseUrl = baseUrl });
        }
    }
}
