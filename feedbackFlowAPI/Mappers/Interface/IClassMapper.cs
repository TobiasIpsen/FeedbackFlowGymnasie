using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;

namespace feedbackFlowAPI.Mappers.Interface
{
    public interface IClassMapper
    {
        public Class ToEntity(ClassDTO dto);
        public ClassDTO ToDTO(Class entity);
    }
}
