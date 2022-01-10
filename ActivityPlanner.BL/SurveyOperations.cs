using ActivityPlanner.DAL;
using ActivityPlanner.DomainServices;
using ActivityPlanner.Entities;
using ActivityPlanner.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner.BL
{
    public class SurveyOperations : ISurveyOperations
    {
        public async Task<ResponseDTO> Create(Survey survey)
        {
            return await Task.Run(() =>
            {
                using (var repo = ActivityPlannerFactory.GetRepository())
                {
                    survey = repo.CreateSurvey(survey);

                    if (survey.id != 0)
                        return new ResponseDTO
                        {
                            Message = "Encuesta registrada con éxito",
                            Data = survey,
                            HttpStatusCode = System.Net.HttpStatusCode.OK
                        };
                    else
                        return new ResponseDTO
                        {
                            Message = "No se pudo registrar la encuesta",
                            Data = survey,
                            HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                        };
                }                            
            });
        }

        public async Task<SurveyDTO> GetDetail(int activityId)
        {
            return await Task.Run(() =>
            {
                SurveyDTO survey = null;

                var result = ActivityPlannerFactory.GetRepository().GetSurveyByActivity(activityId);

                if (result != null)
                    survey = new SurveyDTO
                    {
                        id = result.id,
                        activity_id = result.activity_id,
                        answers = result.answers
                    };

                return survey;
            });
        }

    }
}
