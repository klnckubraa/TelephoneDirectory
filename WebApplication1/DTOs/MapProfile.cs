using AutoMapper;
using WebApplication1.DbModel;

namespace WebApplication1.DTOs
{

    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Detail, DetailDto>().ReverseMap();
            CreateMap<TypeEntity, TypeDto>().ReverseMap();
            CreateMap<Detail, DetailCreateDto>();
            CreateMap<Person, PersonCreateDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<DetailCreateDto, Detail>();


        }
    }
}
