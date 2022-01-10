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
    public class ActivityOperations : IActivityOperations
    {
        IPropertyOperations _propertyOperations;

        public ActivityOperations()
        {
            _propertyOperations = OperationsFactory.PropertyOperations;
        }

        /// <summary>
        /// Crear nueva actividad
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> Create(Activity activity)
        {
            return await Task.Run(async () =>
            {
                //Validar que el estatus de la propiedad este activa
                bool propertyIsActive = await CheckPropertyIsActive(activity.property_id);
                if (!propertyIsActive)
                    return new ResponseDTO
                    {
                        Message = "La actividad no puede crearse porque su propiedad está desactivada",
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                    };

                //Validar disponibilidad de fecha y horario
                bool checkAvailability = await CheckAvailability(activity.schedule, activity.property_id);
                if (!checkAvailability)
                    return new ResponseDTO
                    {
                        Message = "Ya existe otra actividad para la misma fecha y horario",
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                    };

                using (var repo = ActivityPlannerFactory.GetRepository())
                {
                    var activitiesProperty = repo.GetActivitiesByProperty(activity.property_id);

                    activity.created_at = DateTime.Now;
                    activity.status = HelperEnums.GetDescription(StatusType.Active);
                    activity = repo.CreateActivity(activity);

                    if (activity.id != 0)
                        return new ResponseDTO
                        {
                            Message = "Actividad registrada con éxito",
                            Data = activity,
                            HttpStatusCode = System.Net.HttpStatusCode.OK
                        };
                    else
                        return new ResponseDTO
                        {
                            Message = "No se pudo registrar la actividad",
                            Data = activity,
                            HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                        };
                }
            });
        }

        /// <summary>
        /// Reagendar actividad
        /// </summary>
        /// <param name="item"></param>
        /// <returns>ResponseDTO</returns>
        public async Task<ResponseDTO> Reschedule(Activity item)
        {
            return await Task.Run(async () =>
            {

                Activity activity = ActivityPlannerFactory.GetRepository().GetActivity(item.id);
                if (activity == null)
                    return new ResponseDTO
                    {
                        Message = "Actividad no encontrada",
                        HttpStatusCode = System.Net.HttpStatusCode.NotFound
                    };

                //Validar que el estatus de la propiedad este activa
                bool propertyIsActive = await CheckPropertyIsActive(activity.property_id);
                if (!propertyIsActive)
                    return new ResponseDTO
                    {
                        Message = "La actividad no puede reagendarse porque su propiedad está desactivada",
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                    };

                //Validar que la actividad no esté cancelada
                if (activity.status == HelperEnums.GetDescription(StatusType.Cancelled))
                    return new ResponseDTO
                    {
                        Message = "La actividad no puede reagendarse porque está cancelada",
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                    };

                //Validar disponibilidad de fecha y horario
                item.schedule.AddHours(activity.schedule.Hour).AddMinutes(activity.schedule.Minute).AddSeconds(activity.schedule.Second);
                bool checkAvailability = await CheckAvailability(item.schedule, activity.property_id, activity.id);
                if (!checkAvailability)
                    return new ResponseDTO
                    {
                        Message = "Ya existe otra actividad para la misma fecha y horario",
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                    };

                using (var repo = ActivityPlannerFactory.GetRepository())
                {
                    activity.schedule = item.schedule;
                    activity.updated_at = DateTime.Now;

                    bool updated = repo.UpdateActivity(activity);

                    if (updated)
                        return new ResponseDTO
                        {
                            Message = "La actividad se reagendó con éxito",
                            HttpStatusCode = System.Net.HttpStatusCode.OK
                        };
                    else
                        return new ResponseDTO
                        {
                            Message = "No se pudo reagendar la actividad. Intente más tarde o informe el error al área técnica",
                            HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                        };
                }
            });
        }

        /// <summary>
        /// Cancelar actividad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> Cancel(int id)
        {
            return await Task.Run(() =>
            {
                Activity activity = ActivityPlannerFactory.GetRepository().GetActivity(id);
                if (activity == null)
                    return new ResponseDTO
                    {
                        Message = "Actividad no encontrada",
                        HttpStatusCode = System.Net.HttpStatusCode.NotFound
                    };

                using (var repo = ActivityPlannerFactory.GetRepository())
                {
                    activity.status = HelperEnums.GetDescription(StatusType.Cancelled);
                    bool updated = repo.UpdateActivity(activity);

                    if (updated)
                        return new ResponseDTO
                        {
                            Message = "La actividad se canceló con éxito",
                            HttpStatusCode = System.Net.HttpStatusCode.OK
                        };
                    else
                        return new ResponseDTO
                        {
                            Message = "No se pudo cancelar la actividad. Intente más tarde o informe el error al área técnica",
                            HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                        };
                }
            });
        }

        //Listar actividades programadas
        public async Task<List<ActivityDTO>> GetActivities(string uri, DateTime? fechaInicial, DateTime? fechaFinal, string status = null)
        {

            return await Task.Run(() =>
            {
                using (var repo = ActivityPlannerFactory.GetRepository())
                {
                    List<Activity> activities = repo.GetActivities();

                    //Validar si hay algún parametro a aplicar
                    if ((fechaInicial.HasValue && fechaFinal.HasValue) || !string.IsNullOrEmpty(status))
                    {
                        if (fechaInicial.HasValue && fechaFinal.HasValue)
                            activities = activities.Where(x => x.schedule >= fechaFinal && x.schedule <= fechaInicial).ToList();

                        if (!string.IsNullOrEmpty(status))
                            activities = activities.Where(x => x.status == status).ToList();
                    }
                    else
                    {
                        fechaInicial = DateTime.Parse(DateTime.Now.AddDays(-3).ToShortDateString());
                        fechaFinal = DateTime.Parse(DateTime.Now.AddDays(14).ToShortDateString());

                        activities = activities.Where(x => fechaInicial <= x.schedule && x.schedule <= fechaFinal).ToList();
                    }

                    return activities.
                            Select(x => new ActivityDTO
                            {
                                ID = x.id,
                                schedule = x.schedule,
                                title = x.title,
                                created_at = x.created_at,
                                status = x.status,
                                condition = x.status == HelperEnums.GetDescription(StatusType.Cancelled) ? "Cancelada" : x.status == HelperEnums.GetDescription(StatusType.Done) ? "Finalizada" : (x.status == HelperEnums.GetDescription(StatusType.Active) && x.schedule >= DateTime.Now) ? "Pendiente a realizar" : "Atrasada",
                                property = repo.GetProperties().Select(y => new { ID = y.id, y.title, y.address }).FirstOrDefault(),
                                survey = $"{uri}/{x.id}"
                            }).ToList();
                }
            });



        }

        /// <summary>
        /// Validar el estatus de la propiedad de una actividad
        /// </summary>
        /// <param name="idProperty"></param>
        /// <returns>bool</returns>
        private async Task<bool> CheckPropertyIsActive(int idProperty)
        {
            bool result = false;

            var property = await _propertyOperations.GetProperty(idProperty);
            if (property != null)
                result = property.status != HelperEnums.GetDescription(StatusType.Disabled);

            return result;
        }

        /// <summary>
        /// Validar si el horario programado para la actividad aún está disponible
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="idProperty"></param>
        /// <param name="idActivity">Opcional: si la actividad ya está registrada se valida la fecha reprogramada</param>
        /// <returns></returns>
        private async Task<bool> CheckAvailability(DateTime schedule, int idProperty, int idActivity = 0)
        {
            return await Task.Run(() =>
            {
                bool result = false;

                using (var repo = ActivityPlannerFactory.GetRepository())
                {
                    //Buscar empalmes para el horario programado 
                    var activitiesProperty = repo.GetActivitiesByProperty(idProperty).
                        Where(x => x.status != HelperEnums.GetDescription(StatusType.Cancelled) && ((x.schedule <= schedule && schedule < x.schedule.AddHours(1)) ||
                                (schedule <= x.schedule && x.schedule < schedule.AddHours(1))) &&
                                (x.id != idActivity || idActivity == 0)).ToList();

                    result = !activitiesProperty.Any();
                }

                return result;
            });

        }


    }
}
