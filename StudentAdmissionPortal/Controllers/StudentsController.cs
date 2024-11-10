using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using StudentAdmissionPortal.Data;
using StudentAdmissionPortal.DTO;
using StudentAdmissionPortal.Models;
using StudentAdmissionPortal.Repositories.Abstract;
using StudentAdmissionPortal.Repositories.Implementation;

namespace StudentAdmissionPortal.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IFamilyMembersRepository familyMembersRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _familyMembersRepository = familyMembersRepository;
        }




        // grt: api/Students
        [HttpGet]

        public async Task<ActionResult<IEnumerable<StudentBasicDto>>> GetAllStudents() {

            var students = await _studentRepository.GetAllStudents();
            return Ok(_mapper.Map<IEnumerable<StudentBasicDto>>(students));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<StudentByIdDto>> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentById(id);

            if (student == null) { return NotFound($"Student with ID {id} was not found."); }
            return Ok(_mapper.Map<StudentByIdDto>(student));
        }


        // post: api/Students
        
        [HttpPost]
        public async Task<ActionResult<StudentBasicDto>> AddStudent([FromBody] StudentBasicDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = _mapper.Map<Student>(studentDto);
            var createdStudent = await _studentRepository.AddStudent(student);

            if (createdStudent == null) { return BadRequest(ModelState); }
            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, _mapper.Map<StudentBasicDto>(createdStudent));
        }




        //put: api/Students/{id}


        [HttpPut("{id}")]
        public async Task<ActionResult<StudentBasicDto>> UpdateStudent([FromRoute] int id, [FromBody] StudentBasicDto studentDto)
        {
            if (id != studentDto.ID)
            {
                return BadRequest("Stdudent Id Mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


           // var existingStudent = await _studentRepository.GetStudentById(id);
           // if (existingStudent == null)
           // {
           //     return NotFound($"Student with Id {id} was not found");
           // }
            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.UpdateStudent(student);
            return NoContent();
        }



        //get: api/Students/{id}/Nationality

        [HttpGet("{id}/Nationality")]

        public async Task<ActionResult<StudentNationalityDto>> GetStudentNationality(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null) {
                return NotFound();
            }
            return Ok(_mapper.Map<StudentNationalityDto>(student));
        }


        //put: api/Students/{id}/Nationality/{nationalityId}

        [HttpPut("{id:int}/Nationality/{nationalityId:int}")]

        public async Task<ActionResult<StudentNationalityDto>> UpdateStudentNationality(int id, int nationalityId)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null) { return NotFound(); }

            student.NationalityId = nationalityId;
            await _studentRepository.UpdateStudent(student);

            return Ok(_mapper.Map<StudentNationalityDto>(student));
        }



        // get: api/Students/{id}/FamilyMembers
        
        [HttpGet("{id}/FamilyMembers")]

        public async Task<ActionResult<IEnumerable<FamilyMemberBasicDto>>> GetFamilyMembers(int id)
        {

            var familyMembers = await _familyMembersRepository.GetFamilyMembersByStudentId(id);
            return Ok(_mapper.Map<IEnumerable<FamilyMemberBasicDto>>(familyMembers));
        }


        // post : api/Student/{id}/fa,ilymembers
        [HttpPost("{id}/Familymembers")]

        public async Task<ActionResult<FamilyMemberBasicDto>> AddFamilyMember(int id, [FromBody] FamilyMemberBasicDto familyMemberDto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var familyMember = _mapper.Map<FamilyMembers>(familyMemberDto);
            familyMember.StudentId = id;

            var createdFamilyMember = await _familyMembersRepository.AddFamilyMember(familyMember);

            return CreatedAtAction(nameof(GetFamilyMembers), new { id = createdFamilyMember.Id }, _mapper.Map<FamilyMemberBasicDto>(createdFamilyMember));


        }



        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var deleted = await _studentRepository.DeleteStudent(id);

            if (!deleted)
            {
                return NotFound($"Student id {id} not found ");

            }

            return NoContent();
        }
      
       
    }
}
