using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(Guid studentId);
        Task<(Student student, bool isValid)> UpdateStudentAsync(Guid studentId, Student studentRequest);
        Task<(Student student, bool isValid)> DeleteStudentAsync(Guid studentId);
        Task<(Student student, bool isValid)> AddStudentAsync(Student studentRequest);
        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);

    }
}
