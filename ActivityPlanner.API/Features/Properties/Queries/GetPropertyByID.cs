using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Features.Properties.Queries
{
    public class GetPropertyByID
    {
        public class GetPropertyByIDCmd : IRequest<PropertyDTO>
        {
            public int property_id { get; set; }
        }

        public class Handler : IRequestHandler<GetPropertyByIDCmd, PropertyDTO>
        {
            public readonly IPropertyOperations _operations;

            public Handler(IPropertyOperations operations)
            {
                _operations = operations;
            }

            public async Task<PropertyDTO> Handle(GetPropertyByIDCmd request, CancellationToken cancellationToken)
            {
                return await _operations.GetProperty(request.property_id);
            }
        }
    }
}
