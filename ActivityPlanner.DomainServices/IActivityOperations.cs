using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner.DomainServices
{
    public interface IActivityOperations
    {
        Task<ResponseDTO> Create(Activity activity);
        Task<ResponseDTO> Reschedule(Activity activity);
        Task<ResponseDTO> Cancel(int id);
        Task<List<ActivityDTO>> GetActivities(string uri, DateTime? fechaInicial, DateTime? fechaFinal, string status = null);
    }
}
