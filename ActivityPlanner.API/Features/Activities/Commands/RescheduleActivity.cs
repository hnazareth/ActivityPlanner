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

namespace ActivityPlanner.API.Features.Activities.Commands
{
    public class RescheduleActivity
    {
        public class RescheduleActivityCmd:IRequest<ResponseDTO>
        {
            public int id { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd HH:MM}")]
            public DateTime schedule { get; set; }
        }

        public class RunValidation: AbstractValidator<RescheduleActivityCmd>
        {
            public RunValidation()
            {
                RuleFor(x => x.id).NotNull();
                RuleFor(x => x.schedule).NotNull().GreaterThan(DateTime.Now).WithMessage("El horario programado debe ser mayor a la fecha y hora actual");                
            }
        }

        public class Handler : IRequestHandler<RescheduleActivityCmd, ResponseDTO>
        {
            public readonly IActivityOperations _operations;

            public Handler(IActivityOperations operations)
            {
                _operations = operations;
            }

            public async Task<ResponseDTO> Handle(RescheduleActivityCmd request, CancellationToken cancellationToken)
            {
                Activity activity = new Activity
                {
                    id = request.id,
                    schedule = DateTime.Parse(request.schedule.ToString("yyyyy-MM-dd HH:mm"))
                };

                var result = await _operations.Reschedule(activity);

                return result;

                throw new Exception("Ocurrió un error inesperado en la solicitud");
            }
        }
    }
}
