using ActivityPlanner.DomainServices;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Features.Properties.Commands
{
    public class DisableProperty
    {
        public class DisablePropertyCmd : IRequest
        {
            public int id_property { get; set; }
        }

        public class RunValidation : AbstractValidator<DisablePropertyCmd>
        {
            public RunValidation()
            {
                RuleFor(x => x.id_property).NotEmpty().NotEqual(0);
            }
        }

        public class Handler : IRequestHandler<DisablePropertyCmd>
        {
            public readonly IPropertyOperations _operations;

            public Handler(IPropertyOperations operations)
            {
                _operations = operations;
            }

            public async Task<Unit> Handle(DisablePropertyCmd request, CancellationToken cancellationToken)
            {
                bool result = await _operations.DisableProperty(request.id_property);

                if (result)
                    return Unit.Value;

                throw new Exception("No se pudo desactivar la propiedad");
            }
        }

    }
}
