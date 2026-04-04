using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Mappers.Interface;
using Humanizer;

namespace feedbackFlowAPI.Mappers.Implementations
{
    public class ClassMapper: IClassMapper
    {
        public Class ToEntity(ClassDTO dto)
        {
            return new Class
            {
                Name = dto.Name,
                Year = dto.Year.ToUniversalTime(),
                Education = dto.Education,
                ClassLevel = dto.ClassLevel,
                CourseId = dto.CourseId,
                DeletedAt = dto.DeletedAt,
                TeacherId = dto.TeacherId,
            };
        }

        public ClassDTO ToDTO(Class entity)
        {
            return new ClassDTO
            {
                Name = entity.Name,
                Year = entity.Year,
                Education = entity.Education,
                ClassLevel = entity.ClassLevel,
                CourseId = entity.CourseId,
                DeletedAt = entity.DeletedAt,
                TeacherId = entity.TeacherId,
            };
        }
    }
}
