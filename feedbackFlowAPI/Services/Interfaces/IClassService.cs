using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IClassService
    {
        public Task<ClassDTO> CreateClass(ClassDTO dto);
    }
}
