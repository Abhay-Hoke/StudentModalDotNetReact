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

namespace StudentAdmissionPortal.Controllers
{
    [Route("api/nationalities")]
    [ApiController]
    public class NationalitiesController : Controller
    {
        private readonly INationalityRepository _nationalityRepository;
        private readonly IMapper _mapper;
        public NationalitiesController(INationalityRepository nationalityRepository, IMapper mapper)
        {
            _nationalityRepository = nationalityRepository;
            _mapper = mapper;
            
        }

        // get  api/Nationalities
        
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<NationalityDto>>> GetAllNationalities()
        {
            var nationalities = await _nationalityRepository.GetAllNationalities();
            var nationalityDtos = _mapper.Map<IEnumerable<NationalityDto>>(nationalities);

            return Ok(nationalityDtos);
        }

        
        // post api/Nationalities
        
        [HttpPost]
        public async Task<ActionResult<NationalityDto>> AddNationality([FromBody] NationalityDto nationalityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var nationality = _mapper.Map<Nationality>(nationalityDto);
            var createdNationality = await _nationalityRepository.AddNationality(nationality);

            
            var createdNationalityDto = _mapper.Map<NationalityDto>(createdNationality);

            return CreatedAtAction(nameof(GetAllNationalities), new { id = createdNationalityDto.ID }, createdNationalityDto);
        }























        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Nationality>>> GetAllNationalities()
        //{
        //    var nationalities =await _nationalityRepository.GetAllNationalities();
        //    return Ok(nationalities);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Nationality>> GetNationalityById([FromRoute] int id)
        //{
        //    if (id == 0)
        //        return BadRequest("Id should be Greater than 0");
        //    var nationality = await _nationalityRepository.GetNationalityById(id);
        //    if (nationality == null)
        //        return NotFound("Not found Wrong Id");
        //    return Ok(nationality);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Nationality>> AddNationality([FromBody] Nationality nationality)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var createdNationality = await _nationalityRepository.AddNationality(nationality);
        //    return CreatedAtAction(nameof(GetNationalityById), new { id = createdNationality.Id }, createdNationality);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateNationality([FromRoute] int id, [FromBody] Nationality nationality)
        //{
        //    if (id != nationality.Id)
        //        return BadRequest("Nationality id mismatch");

        //    var presentNationality = await _nationalityRepository.GetNationalityById(id);
        //    if (presentNationality == null)
        //        return NotFound("not found");

        //    await _nationalityRepository.UpdateNationality(nationality);
        //    return NoContent();
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Deletenationality([FromBody] int id)
        //{
        //    if(id <= 0)
        //    {
        //        return BadRequest("invalid id , please checkkkkkkkkkk it");
        //    }

        //    var deleted =await _nationalityRepository.DeleteNationality(id);

        //    if (!deleted)
        //        return NotFound("not found check id");
        //    return NoContent();
        //}
    }
}
