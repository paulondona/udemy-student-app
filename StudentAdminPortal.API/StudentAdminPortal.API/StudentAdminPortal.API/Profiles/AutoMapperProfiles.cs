using AutoMapper;
using DomainModel = StudentAdminPortal.API.DomainModels;
using DataModel = StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Profiles.AfterMap;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModel.Student, DomainModel.Student>()
                .ReverseMap();

            CreateMap<DataModel.Gender, DomainModel.Gender>()
                .ReverseMap();

            CreateMap<DataModel.Address, DomainModel.Address>()
                .ReverseMap();

            CreateMap<DomainModel.UpdateStudentRequest, DataModel.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<DomainModel.AddStudentRequest, DataModel.Student>()
                .AfterMap<AddStudentRequestAfterMap>();
        }
    }
}
