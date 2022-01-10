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
    public class CreateActivity
    {
        public class CreateActivityCmd : IRequest<ResponseDTO>
        {
            public int property_id { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-mm-dd HH:MM}")]
            public DateTime schedule { get; set; }

            public string title { get; set; }
        }

        public class RunValidation : AbstractValidator<CreateActivityCmd>
        {
            public RunValidation()
            {
                RuleFor(x => x.property_id).NotNull();
                RuleFor(x => x.schedule).NotNull().GreaterThan(DateTime.Now).WithMessage("El horario programado debe ser mayor a la fecha y hora actual");
                RuleFor(x => x.title).NotEmpty();                
            }
        }

        public class Handler : IRequestHandler<CreateActivityCmd, ResponseDTO>
        {
            public readonly IActivityOperations _operations;

            public Handler(IActivityOperations operations)
            {
                _operations = operations;
            }

            public async Task<ResponseDTO> Handle(CreateActivityCmd request, CancellationToken cancellationToken)
            {
                Activity activity = new Activity
                {
                    property_id = request.property_id,
                    schedule = DateTime.Parse(request.schedule.ToString("yyyyy-MM-dd HH:mm")),
                    title = request.title
                };

                var result = await _operations.Create(activity);

                return result;

                throw new Exception("Ocurrió un error inesperado en la solicitud");
            }
        }
    }
}
