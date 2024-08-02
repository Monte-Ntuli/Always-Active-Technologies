
using AAT_Crud.Entities;
using AutoMapper;
using SharedClasses.DTOs;

namespace AAT_Crud
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            
            CreateMap<EventsEntity, CreateEventDTO>().ReverseMap();
            CreateMap<EventRegistrationEntity, CreateEventDTO>().ReverseMap();
            CreateMap<EventsEntity, UpdateEventDTO>().ReverseMap();
            CreateMap<EventRegistrationEntity, CreateEventRegDTO>().ReverseMap();
            
        }
    }
}
