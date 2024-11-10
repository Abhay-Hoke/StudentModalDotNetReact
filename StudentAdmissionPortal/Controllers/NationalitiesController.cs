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























        
         
    }
}
