using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
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
                throw new InvalidOperationException($"Constraint {pgEx.ConstraintName} violated");
            }

            return _mapper.ToDTO(result.Entity);
        }

        public async Task<List<ClassStudentDTO>> GetStudentsForClass(int classId)
        {
            var classEntity = await _context.Classes
                .AsNoTracking()
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId && c.DeletedAt == null);

            if (classEntity == null)
            {
                return new List<ClassStudentDTO>();
            }

            return classEntity.Students
                .Where(s => s.DeletedAt == null)
                .OrderBy(s => s.Firstname)
                .ThenBy(s => s.Lastname)
                .Select(s => new ClassStudentDTO
                {
                    Id = s.Id,
                    Firstname = s.Firstname,
                    Lastname = s.Lastname,
                    Email = s.Email
                })
                .ToList();
        }

        public async Task<List<StudentLookupDTO>> SearchStudents(string? query, int? classId)
        {
            var studentQuery = _context.Users
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .Where(u => u.UserRoles.Any(r => r.Name == "Student"));

            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = query.Trim();
                studentQuery = studentQuery.Where(u =>
                    EF.Functions.ILike(u.Firstname, $"%{term}%") ||
                    EF.Functions.ILike(u.Lastname, $"%{term}%") ||
                    EF.Functions.ILike(u.Email, $"%{term}%"));
            }

            var students = await studentQuery
                .OrderBy(u => u.Firstname)
                .ThenBy(u => u.Lastname)
                .Take(100)
                .Select(u => new StudentLookupDTO
                {
                    Id = u.Id,
                    FullName = u.Firstname + " " + u.Lastname,
                    Email = u.Email,
                    IsInClass = false
                })
                .ToListAsync();

            if (!classId.HasValue)
            {
                return students;
            }

            var classEntity = await _context.Classes
                .AsNoTracking()
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId.Value && c.DeletedAt == null);

            if (classEntity == null)
            {
                return students;
            }

            var studentIdsInClass = classEntity.Students
                .Where(s => s.DeletedAt == null)
                .Select(s => s.Id)
                .ToHashSet();

            foreach (var student in students)
            {
                student.IsInClass = studentIdsInClass.Contains(student.Id);
            }

            return students;
        }

        public async Task<bool> AddStudentToClass(int classId, int studentId)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId && c.DeletedAt == null);

            if (classEntity == null)
            {
                return false;
            }

            var student = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == studentId && u.DeletedAt == null);

            if (student == null)
            {
                return false;
            }

            var isStudentRole = student.UserRoles.Any(r => r.Name == "Student");
            if (!isStudentRole)
            {
                return false;
            }

            if (classEntity.Students.Any(s => s.Id == studentId))
            {
                return true;
            }

            classEntity.Students.Add(student);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
