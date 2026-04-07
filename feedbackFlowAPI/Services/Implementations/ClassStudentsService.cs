using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace feedbackFlowAPI.Services.Implementations
{
    public class ClassStudentsService : IClassStudentsService
    {
        private readonly FbfDbContext _context;

        public ClassStudentsService(FbfDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClassStudentDTO>?> GetClassStudentsAsync(int classId)
        {
            bool classExists = await _context.Classes.AnyAsync(c => c.Id == classId);
            if (!classExists)
            {
                return null;
            }

            return await _context.Classes
                .Where(c => c.Id == classId)
                .SelectMany(c => c.Users!)
                .Where(u => u.DeletedAt == null)
                .OrderBy(u => u.Firstname)
                .ThenBy(u => u.Lastname)
                .Select(u => new ClassStudentDTO
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<List<StudentSearchDTO>?> SearchStudentsAsync(int classId, string query)
        {
            bool classExists = await _context.Classes.AnyAsync(c => c.Id == classId);
            if (!classExists)
            {
                return null;
            }

            string trimmedQuery = query.Trim();

            IQueryable<User> users = _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.Classes)
                .Where(u => u.DeletedAt == null)
                .Where(u =>
                    EF.Functions.ILike((u.Firstname + " " + u.Lastname), $"%{trimmedQuery}%") ||
                    EF.Functions.ILike(u.Email, $"%{trimmedQuery}%"));

            bool hasStudentRoleDefinitions = await _context.UserRoles
                .AnyAsync(r => r.Name.ToLower() == "student" || r.Name.ToLower() == "elev");

            if (hasStudentRoleDefinitions)
            {
                users = users.Where(u => u.UserRoles.Any(r =>
                    r.Name.ToLower() == "student" || r.Name.ToLower() == "elev"));
            }

            return await users
                .OrderBy(u => u.Firstname)
                .ThenBy(u => u.Lastname)
                .Take(50)
                .Select(u => new StudentSearchDTO
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    IsInClass = u.Classes!.Any(c => c.Id == classId)
                })
                .ToListAsync();
        }

        public async Task<(bool Success, string? Error)> AddStudentToClassAsync(int classId, int studentId)
        {
            Class? schoolClass = await _context.Classes
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (schoolClass == null)
            {
                return (false, "Class not found.");
            }

            User? user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == studentId && u.DeletedAt == null);

            if (user == null)
            {
                return (false, "Student not found.");
            }

            bool hasStudentRoleDefinitions = await _context.UserRoles
                .AnyAsync(r => r.Name.ToLower() == "student" || r.Name.ToLower() == "elev");

            if (hasStudentRoleDefinitions)
            {
                bool isStudent = user.UserRoles.Any(r =>
                    r.Name.ToLower() == "student" || r.Name.ToLower() == "elev");

                if (!isStudent)
                {
                    return (false, "User is not a student.");
                }
            }

            if (schoolClass.Users == null)
            {
                schoolClass.Users = new List<User>();
            }

            if (schoolClass.Users.Any(u => u.Id == studentId))
            {
                return (false, "Student is already in this class.");
            }

            schoolClass.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> RemoveStudentFromClassAsync(int classId, int studentId)
        {
            Class? schoolClass = await _context.Classes
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (schoolClass == null)
            {
                return (false, "Class not found.");
            }

            if (schoolClass.Users == null)
            {
                return (false, "Student is not in this class.");
            }

            User? user = schoolClass.Users.FirstOrDefault(u => u.Id == studentId);
            if (user == null)
            {
                return (false, "Student is not in this class.");
            }

            schoolClass.Users.Remove(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }
    }
}
