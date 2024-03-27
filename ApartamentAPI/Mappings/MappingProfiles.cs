using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using AutoMapper;

namespace ApartamentAPI.Mappings
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {


            CreateMap<CreateRequest, Apartament>();
        }

    }
}
