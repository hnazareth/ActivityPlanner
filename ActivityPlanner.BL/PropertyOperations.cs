using ActivityPlanner.DAL;
using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using ActivityPlanner.Entities.Enums;
using ActivityPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner.BL
{
    public class PropertyOperations : IPropertyOperations
    {
        /// <summary>
        /// Crear nueva propiedad
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public async Task<Property> Create(Property property)
        {
            return await Task.Run(() =>
            {
                if (property != null)
                {
                    using (var repo = ActivityPlannerFactory.GetRepository())
                    {
                        property.created_at = DateTime.UtcNow;
                        property.status = HelperEnums.GetDescription(StatusType.Active);
                        property = repo.CreateProperty(property);
                    }
                }
                return property;
            });
        }

        /// <summary>
        /// Retornar propiedad
        /// </summary>
        /// <param name="idProperty"></param>
        /// <returns></returns>
        public async Task<PropertyDTO> GetProperty(int idProperty)
        {
            return await Task.Run(() =>
            {
                PropertyDTO property = null;

                var result = ActivityPlannerFactory.GetRepository().GetProperty(idProperty);

                if (result != null)
                    property = new PropertyDTO
                    {
                        id = result.id,
                        title = result.title,
                        address = result.address,
                        description = result.description,
                        created_at = result.created_at,
                        updated_at = result.updated_at,
                        disabled_at = result.disabled_at,
                        status = result.status
                    };

                return property;
            });
        }

        /// <summary>
        /// Retornar lista de propiedades
        /// </summary>
        /// <returns></returns>
        public async Task<List<PropertyDTO>> GetProperties()
        {
            return await Task.Run(() =>
            {
                return ActivityPlannerFactory.GetRepository().GetProperties().Select(x => new PropertyDTO
                {
                    id = x.id,
                    title = x.title,
                    address = x.address,
                    description = x.description,
                    created_at = x.created_at,
                    updated_at = x.updated_at,
                    disabled_at = x.disabled_at,
                    status = x.status
                }).ToList();
            });
        }

        /// <summary>
        /// Deshabilitar propiedad
        /// </summary>
        /// <param name="idProperty"></param>
        /// <returns></returns>
        public async Task<bool> DisableProperty(int idProperty)
        {
            return await Task.Run(() =>
            {
                bool result = false;

                if (idProperty != 0)
                {
                    using (var repo = ActivityPlannerFactory.GetRepository())
                    {
                        var property = repo.GetProperty(idProperty);
                        if (property != null)
                        {
                            property.status = HelperEnums.GetDescription(StatusType.Disabled);
                            property.disabled_at = DateTime.UtcNow;

                            result = repo.UpdateProperty(property);
                        }
                    }
                }

                return result;
            });
        }
    }
}
