using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityPlanner.DAL
{
    public class ActivityPlannerRepository : IActivityPlannerRepository
    {
        DbContextActivityPlanner _context;
        bool _isUnitOfWork;

        public ActivityPlannerRepository(bool isUnitOfWork = false)
        {
            _context = new DbContextActivityPlanner();
            _isUnitOfWork = isUnitOfWork;
        }

        public int SaveChanges()
        {
            int result = 0;
            if (_context != null)
            {
                try
                {
                    result = this._context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return result;
        }

        private bool Save()
        {
            int changes = 0;
            if (!_isUnitOfWork)
                changes = SaveChanges();
            return changes == 1;
        }


        #region Property
        public Property CreateProperty(Property property)
        {
            property = _context.Add(property).Entity;
            Save();
            return property;
        }

        public Property GetProperty(int idProperty)
        {
            return _context.Find<Property>(idProperty);
        }

        public List<Property> GetProperties()
        {
            return _context.Property.ToList();
        }

        public bool UpdateProperty(Property property)
        {
            _context.Update(property);
            return Save();
        }
        #endregion

        #region Activity
        public Activity CreateActivity(Activity activity)
        {
            activity = _context.Add(activity).Entity;
            Save();
            return activity;
        }
        
        public bool UpdateActivity(Activity activity)
        {
            _context.Update(activity);
            return Save();
        }

        public Activity GetActivity(int idActivity)
        {
            return _context.Find<Activity>(idActivity);
        }

        public List<Activity> GetActivities()
        {
            return _context.Activity.ToList();
        }

        public List<Activity> GetActivitiesByProperty(int idProperty)
        {
            return _context.Activity.Where(x => x.property_id == idProperty).ToList();
        }
        #endregion

        #region Survey
        public Survey CreateSurvey(Survey survey)
        {
            survey = _context.Add(survey).Entity;
            Save();
            return survey;
        }
        
        public List<Survey> GetSurveys()
        {
            return _context.Survey.ToList();
        }

        public Survey GetSurvey(int idSurvey)
        {
            return _context.Find<Survey>(idSurvey);
        }
        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
