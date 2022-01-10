using ActivityPlanner.DomainServices;
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
    public class CancelActivity
    {
        public class CancelActivityCmd : IRequest<ResponseDTO>
        {
            public int id { get; set; }
        }

        public class RunValidation : AbstractValidator<CancelActivityCmd>
        {
            public RunValidation()
            {
                RuleFor(x => x.id).NotNull();
            }
        }

        public class Handler : IRequestHandler<CancelActivityCmd, ResponseDTO>
        {
            public readonly IActivityOperations _operations;

            public Handler(IActivityOperations operations)
            {
                _operations = operations;
            }

            public async Task<ResponseDTO> Handle(CancelActivityCmd request, CancellationToken cancellationToken)
            {
                var result = await _operations.Cancel(request.id);

                return result;

                throw new Exception("Ocurrió un error inesperado en la solicitud");
            }
        }
    }
}
