using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository repository, IMapper mapper)
        {
            this.studentRepository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();

            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            var student = await studentRepository.GetStudentAsync(studentId );

            if (student == null)
            {
                return NotFound();
            }    

            return Ok(mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest student)
        {

            if (!await studentRepository.Exists(studentId))
            {
                return NotFound();
            }

            var updatedStudent = await studentRepository.UpdateStudentAsync(studentId, mapper.Map<DataModels.Student>(student));

            if (!updatedStudent.isValid)
            {
                return BadRequest();
            }
              
            return Ok(mapper.Map<Student>(updatedStudent.student));
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (!await studentRepository.Exists(studentId))
            {
                return NotFound();
            }

            var deleteResult = await studentRepository.DeleteStudentAsync(studentId);

            if (!deleteResult.isValid)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<Student>(deleteResult.student));
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var (student, isValid) = await studentRepository.AddStudentAsync(mapper.Map<DataModels.Student>(request));

            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id}, mapper.Map<Student>(student));
        }

    }
}
