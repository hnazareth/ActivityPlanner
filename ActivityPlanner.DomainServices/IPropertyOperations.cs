using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner.DomainServices
{
    public interface IPropertyOperations
    {
        Task<Property> Create(Property property);
        Task<bool> DisableProperty(int idProperty);
        Task<PropertyDTO> GetProperty(int idProperty);
        Task<List<PropertyDTO>> GetProperties();
    }
}
