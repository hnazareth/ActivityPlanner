using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using System.Threading.Tasks;

namespace ActivityPlanner.DomainServices
{
    public interface ISurveyOperations
    {
        Task<ResponseDTO> Create(Survey survey);
        Task<SurveyDTO> GetDetail(int id);
    }
}
