using ActivityPlanner.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.DomainServices
{
    public interface IActivityPlannerRepository
    {
        #region Property
        Property CreateProperty(Property property);
        bool UpdateProperty(Property property);
        bool DeleteProperty(int idProperty);
        List<Property> GetProperties();
        #endregion

        #region Activity
        Activity CreateActivity(Activity activity);
        bool UpdateActivity(Activity activity);
        bool DeleteActivity(int idActivity);
        List<Activity> GetActivities();
        #endregion

        #region Survey
        Survey CreateSurvey(Survey survey);
        bool UpdateSurvey(Survey survey);
        bool DeleteSurvey(int idSurvey);
        List<Survey> GetSurveys();
        #endregion
    }
}
