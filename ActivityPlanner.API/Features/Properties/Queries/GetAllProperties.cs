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
    public class GetAllProperties
    {
        public class GetAllPropertiesCmd : IRequest<List<PropertyDTO>>
        {

        }

        public class Handler : IRequestHandler<GetAllPropertiesCmd, List<PropertyDTO>>
        {
            public readonly IPropertyOperations _operations;

            public Handler(IPropertyOperations operations)
            {
                _operations = operations;
            }

            public async Task<List<PropertyDTO>> Handle(GetAllPropertiesCmd request, CancellationToken cancellationToken)
            {
                var properties = await _operations.GetProperties();
                return properties;
            }
        }
    }
}
