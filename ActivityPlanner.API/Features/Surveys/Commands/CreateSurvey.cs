using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Features.Surveys.Commands
{
    public class CreateSurvey
    {
        public class CreateSurveyCmd : IRequest<ResponseDTO>
        {
            public int activity_id { get; set; }
            
            public string answers { get; set; }
        }

        public class RunValidation : AbstractValidator<CreateSurveyCmd>
        {
            public RunValidation()
            {
                RuleFor(x => x.activity_id).NotNull();                
                RuleFor(x => x.answers).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateSurveyCmd, ResponseDTO>
        {
            public readonly ISurveyOperations _operations;

            public Handler(ISurveyOperations operations)
            {
                _operations = operations;
            }

            public async Task<ResponseDTO> Handle(CreateSurveyCmd request, CancellationToken cancellationToken)
            {
                Survey survey = new Survey
                {
                    activity_id = request.activity_id,
                    answers = request.answers
                };

                var result = await _operations.Create(survey);

                return result;

                throw new Exception("Ocurrió un error inesperado en la solicitud");
            }
        }
    }
}
