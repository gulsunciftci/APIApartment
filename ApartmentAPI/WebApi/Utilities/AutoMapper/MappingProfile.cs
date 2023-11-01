using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApartmentDtoForUpdate, Apartment>().ReverseMap();
            CreateMap<Apartment, ApartmentDto>();
            CreateMap<ApartmentDtoForInsertion, Apartment>();
        }
       
    }
}
