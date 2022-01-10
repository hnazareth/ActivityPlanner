using ActivityPlanner.DomainServices;

namespace ActivityPlanner.BL
{
    public class OperationsFactory
    {
        public static IActivityOperations ActivityOperations => new ActivityOperations();
        public static IPropertyOperations PropertyOperations => new PropertyOperations();
        public static ISurveyOperations SurveyOperations => new SurveyOperations();
    }
}
