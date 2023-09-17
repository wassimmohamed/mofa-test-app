using Microsoft.AspNetCore.Mvc;
using mofa_test.Models;
using mofa_test.Repositories;

namespace mofa_test.Controllers
{
    [ApiController]
    [Route("api")]
    public class StudentController : ControllerBase
    {
      
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository, IFamilyMemberRepository familyMemberRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _familyMemberRepository = familyMemberRepository;
        }

        [HttpGet]
        [Route("Students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return Ok(await _studentRepository.GetAll());
        }
        [HttpPost]
        [Route("Students")]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            return Ok(await _studentRepository.Add(student));
        }
        [HttpPut]
        [Route("Students/{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
        {
            if (await _studentRepository.Get(id) == null)
                return NotFound(student);

            return Ok(await _studentRepository.Update(id, student));
        }
        [HttpGet]
        [Route("Students/{id}/Nationality")]
        public async Task<ActionResult<StudentNationalityVM>> StudentNationality(int id)
        {
            var studentNationality = await _studentRepository.GetStudentNationality(id);
            if (studentNationality == null)
                return NotFound(id);

            return Ok(studentNationality);
        }

        [HttpPut]
        [Route("Students/{id}/Nationality/{nationalityId}")]
        public async Task<ActionResult<StudentNationalityVM>> UpdateStudentNationality(int id, int nationalityId)
        {
            if (await _studentRepository.Get(id) == null)
                return NotFound(id);

            return Ok(await _studentRepository.UpdateStudentNationality(id, nationalityId));
        }
        [HttpGet]
        [Route("Students/{id}/FamilyMembers")]
        public async Task<ActionResult<IEnumerable<FamilyMember>>> GetStudentFamilyMembers(int id)
        {
            return Ok(await _studentRepository.GetStudentFamilyMembers(id));
        }

        [HttpPost]
        [Route("Students/{id}/FamilyMembers")]
        public async Task<ActionResult<FamilyMember>> AddStudentFamilyMember(int id,FamilyMember familyMember)
        {
            await Task.Delay(1);            
            var result = await _studentRepository.AddFamilyMember(id, familyMember);            
            return Ok(result);
        }
    }
}