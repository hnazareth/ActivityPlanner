using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Features.Properties.Commands
{
    public class CreateProperty
    {
        public class CreatePropertyCmd : IRequest
        {
            public string title { get; set; }

            public string address { get; set; }

            public string description { get; set; }
        }

        public class RunValidation:AbstractValidator<CreatePropertyCmd>
        {
            public RunValidation()
            {
                RuleFor(x => x.title).NotEmpty();
                RuleFor(x => x.address).NotEmpty();
                RuleFor(x => x.description).NotEmpty();
            }
        }

        public class Handler:IRequestHandler<CreatePropertyCmd>
        {
            public readonly IPropertyOperations _operations;

            public Handler(IPropertyOperations operations)
            {
                _operations = operations;
            }

            public async Task<Unit> Handle(CreatePropertyCmd request, CancellationToken cancellationToken)
            {
                Property property = new Property
                {
                    title = request.title,
                    address = request.address,
                    description = request.description
                };

                property = await _operations.Create(property);

                if (property.id != 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar la propiedad");
            }
        }
    }

    
}
