using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.ClassStatistics;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IClassService
    {
        public Task<ClassDTO> CreateClass(ClassDTO dto);
        public Task<ClassStatisticsDTO> GetStatistics(int ClassId, int QuestionSet);
    }
}
