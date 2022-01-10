using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Features.Surveys.Queries
{
    public class GetSurvey
    {
        public class GetSurveyCmd : IRequest<SurveyDTO>
        {
            public int survey_id { get; set; }
        }

        public class Handler : IRequestHandler<GetSurveyCmd, SurveyDTO>
        {
            public readonly ISurveyOperations _operations;

            public Handler(ISurveyOperations operations)
            {
                _operations = operations;
            }

            public async Task<SurveyDTO> Handle(GetSurveyCmd request, CancellationToken cancellationToken)
            {
                return await _operations.GetDetail(request.survey_id);
            }
        }
    }
}
