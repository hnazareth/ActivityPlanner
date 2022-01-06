using ActivityPlanner.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.DAL
{
    public static class ActivityPlannerFactory
    {
        public static IActivityPlannerRepository GetRepository(bool isUnitOfWork = false) => new ActivityPlannerRepository(isUnitOfWork);
    }
}
