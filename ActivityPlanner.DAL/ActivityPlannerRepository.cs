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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
