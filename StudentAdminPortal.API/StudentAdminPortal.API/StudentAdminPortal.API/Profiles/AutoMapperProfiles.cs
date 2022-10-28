using AutoMapper;
using DomainModel = StudentAdminPortal.API.DomainModels;
using DataModel = StudentAdminPortal.API.DataModels;

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
        }
    }
}
