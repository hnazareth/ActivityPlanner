using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityPlanner.API.Features.Activities.Queries
{
    public class GetActivities
    {
        public class GetActivitiesCmd : IRequest<List<ActivityDTO>>
        {
            public DateTime? fecha_inicial { get; set; }
            public DateTime? fecha_final { get; set; }
            public string status { get; set; } = null;
        }

        public class Handler : IRequestHandler<GetActivitiesCmd, List<ActivityDTO>>
        {
            public readonly IActivityOperations _operations;

            public Handler(IActivityOperations operations)
            {
                _operations = operations;
            }

            public async Task<List<ActivityDTO>> Handle(GetActivitiesCmd request, CancellationToken cancellationToken)
            {
                return await _operations.GetActivities("", request.fecha_inicial, request.fecha_final, request.status);
            }
        }
    }
}
