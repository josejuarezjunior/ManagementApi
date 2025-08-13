using AutoMapper;
using Core.Entities;

namespace ManagementApi.DTOs.Mappings
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
