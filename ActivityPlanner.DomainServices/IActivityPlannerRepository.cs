using ActivityPlanner.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.DomainServices
{
    public interface IActivityPlannerRepository : IDisposable
    {
        #region Property
        Property CreateProperty(Property property);
        bool UpdateProperty(Property property);
        Property GetProperty(int idProperty);
        List<Property> GetProperties();        
        #endregion

        #region Activity
        Activity CreateActivity(Activity activity);
        bool UpdateActivity(Activity activity);
        Activity GetActivity(int idActivity);
        List<Activity> GetActivities();
        List<Activity> GetActivitiesByProperty(int idProperty);
        #endregion

        #region Survey
        Survey CreateSurvey(Survey survey);
        List<Survey> GetSurveys();
        Survey GetSurveyByActivity(int idActivity);
        #endregion

        int SaveChanges();
    }
}
