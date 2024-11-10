using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAdmissionPortal.Data;
using StudentAdmissionPortal.DTO;
using StudentAdmissionPortal.Models;
using StudentAdmissionPortal.Repositories.Abstract;
using StudentAdmissionPortal.Repositories.Implementation;

namespace StudentAdmissionPortal.Controllers
{
    [Route("api/Familymembers")]
    [ApiController]
    public class FamilyMembersController : Controller
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;
        private readonly IMapper _mapper;


        public FamilyMembersController(IFamilyMembersRepository familyMembersRepository, IMapper mapper)
        {
            _familyMembersRepository = familyMembersRepository;
            _mapper = mapper;
        }


        //put api/Familymember/id

        [HttpPut("{id}")]
        public async Task<ActionResult<FamilyMemberBasicDto>> UpdateFamilymember(int id, [FromBody] FamilyMemberBasicDto familyMemberBasicDto)

        {
            if (id != familyMemberBasicDto.ID) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var familyMember = _mapper.Map<FamilyMembers>(familyMemberBasicDto);
            await _familyMembersRepository.UpdateFamilyMember(familyMember);
            return Ok(familyMember);

        }



        // delete api/FamilyMembers/{id}

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFamilyMember(int id)
        {
            var deleted = await _familyMembersRepository.DeleteFamilyMember(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
        

        // grt  api/FamilyMembers/{id}/Nationality/{nationalityId}
        
        [HttpGet("{id:int}/Nationality")]
        public async Task<ActionResult<FamilyMemberNationalityDto>> GetFamilyMemberNationality(int id)
        {
            var familyMember = await _familyMembersRepository.GetFamilyMemberById(id);
            if (familyMember == null) return NotFound();

            return Ok(_mapper.Map<FamilyMemberNationalityDto>(familyMember));
        }



        
        // put  api/FamilyMembers/{id}/Nationality/{nationalityId}
        
        [HttpPut("{id:int}/Nationality/{nationalityId:int}")]
        public async Task<ActionResult<FamilyMemberNationalityDto>> UpdateFamilyMemberNationality(int id, int nationalityId)
        {
            var familyMember = await _familyMembersRepository.GetFamilyMemberById(id);
            if (familyMember == null) return NotFound();

            familyMember.NationalityId = nationalityId;
            await _familyMembersRepository.UpdateFamilyMember(familyMember);

            return Ok(_mapper.Map<FamilyMemberNationalityDto>(familyMember));
        }










        //[HttpGet("student/{studentId}")]
        //public async Task<ActionResult<IEnumerable<FamilyMembers>>> GetFamilyMembersByStudentId([FromRoute] int studentId)
        //{
        //    if (studentId <= 0)
        //        return BadRequest("Invaid student id");

        //    var familyMembers =await _familyMembersRepository.GetFamilyMembersByStudentId(studentId);
        //    return Ok(familyMembers);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<FamilyMembers>> GetFamilyMemberById([FromRoute] int id)
        //{

        //    var familyMember = await _familyMembersRepository.GetFamilyMemberById(id);

        //    if (familyMember == null)
        //        return NotFound("Family Not found");
        //    return Ok(familyMember);
        //}

        //[HttpPost]
        //public async Task<ActionResult<FamilyMembers>> AddFamilyMember([FromBody] FamilyMembers familyMember)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var createdFamilyMember =await _familyMembersRepository.AddFamilyMember(familyMember);
        //    return CreatedAtAction(nameof(GetFamilyMemberById),new {id = createdFamilyMember.Id},createdFamilyMember);

        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateFamilyMember([FromRoute] int id,[FromBody] FamilyMembers familyMember)
        //{
        //    if (id != familyMember.Id)
        //        return BadRequest("family member id mismatch");

        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var existingMember = await _familyMembersRepository.GetFamilyMemberById(id);
        //    if (existingMember == null)
        //        return NotFound(" check id again Member not found");

        //    await _familyMembersRepository.UpdateFamilyMember(existingMember);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFamilyMember([FromRoute] int id)
        //{
        //    if (id <= 0) return BadRequest("Invalid family member delete id");

        //    var deleted =await _familyMembersRepository.DeleteFamilyMember(id);
        //    if(!deleted)
        //    {
        //        return NotFound("Family mrmber id is invalid ,please check it");
        //    }
        //    return NoContent();
        //}
    }
}
