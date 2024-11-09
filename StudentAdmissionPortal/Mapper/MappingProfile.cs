using AutoMapper;
using StudentAdmissionPortal.DTO;
using StudentAdmissionPortal.Models;

namespace StudentAdmissionPortal.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Student to Student DTOs
            CreateMap<Student, StudentBasicDto>().ReverseMap();
            CreateMap<Student, StudentNationalityDto>().ReverseMap();

            // FamilyMembers to FamilyMember DTOs
            CreateMap<FamilyMembers, FamilyMemberBasicDto>().ReverseMap();
            CreateMap<FamilyMembers, FamilyMemberNationalityDto>().ReverseMap();

            // Nationality to Nationality DTO
            CreateMap<Nationality, NationalityDto>().ReverseMap();
        }
    }
}
