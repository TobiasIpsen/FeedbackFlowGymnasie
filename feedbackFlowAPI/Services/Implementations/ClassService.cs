using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Implementations;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;

namespace feedbackFlowAPI.Services.Implementations
{
    public class ClassService : IClassService
    {
        private FbfDbContext _context;
        private readonly IClassMapper _mapper;

        public ClassService(FbfDbContext context, IClassMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClassDTO> CreateClass(ClassDTO dto)
        {
            Class entity = _mapper.ToEntity(dto);
            EntityEntry<Class> result = await _context.Classes.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                throw new Exception($"Constraint {pgEx.ConstraintName} violated");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.ToDTO(result.Entity);
        }
    }
}
