using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.DAL
{
    public class ActivityPlannerRepository : IActivityPlannerRepository
    {
        #region Property
        public Property CreateProperty(Property property)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProperty(int idProperty)
        {
            throw new NotImplementedException();
        }

        public List<Property> GetProperties()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProperty(Property property)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Activity
        public Activity CreateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }
        
        public bool UpdateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteActivity(int idActivity)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetActivities()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Survey
        public Survey CreateSurvey(Survey survey)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSurvey(Survey survey)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSurvey(int idSurvey)
        {
            throw new NotImplementedException();
        }
        public List<Survey> GetSurveys()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
