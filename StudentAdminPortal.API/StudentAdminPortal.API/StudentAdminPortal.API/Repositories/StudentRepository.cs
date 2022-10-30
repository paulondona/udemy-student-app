using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public StudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(student => student.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Student.AnyAsync(student => student.Id == studentId);
        }

        public async Task<(Student student, bool isValid)> UpdateStudent(Guid studentId, Student studentRequest)
        {
            var currentStudent = await GetStudentAsync(studentId);
            bool isValid = false;

            if (currentStudent != null)
            {
                currentStudent.FirstName = studentRequest.FirstName;
                currentStudent.LastName = studentRequest.LastName;
                currentStudent.DateOfBirth = studentRequest.DateOfBirth;
                currentStudent.Email = studentRequest.Email;
                currentStudent.Mobile = studentRequest.Mobile;
                currentStudent.GenderId = studentRequest.GenderId;
                currentStudent.Address.PhysicalAddress = studentRequest.Address.PhysicalAddress;
                currentStudent.Address.PostalAddress = studentRequest.Address.PostalAddress;

                var value = await context.SaveChangesAsync();

                return (currentStudent, isValid: true);
            }

            return (currentStudent, isValid);
        }
    }
}
