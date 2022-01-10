using ActivityPlanner.API.Features.Surveys.Commands;
using ActivityPlanner.API.Features.Surveys.Queries;
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
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ResponseDTO>> Create(CreateSurvey.CreateSurveyCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("GetSurvey/{survey_id}")]
        public async Task<ActionResult<SurveyDTO>> GetProperty(int survey_id)
        {
            return await _mediator.Send(new GetSurvey.GetSurveyCmd { survey_id = survey_id });
        }
    }
}
