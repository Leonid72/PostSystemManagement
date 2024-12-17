using API.Models;
using API.Models.Dto;
using AutoMapper;
namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() //Convert Dto to Model
        {
            CreateMap<PostItem, PostItemDto>()
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place.Name))
                .ForMember(d => d.ImagePath, o => o.MapFrom<PostUrlResolver>())
                .ReverseMap()
                .ForMember(dest => dest.Place, opt => opt.Ignore());

            //CreateMap<PostItemDto, PostItem>()
            //    .ForMember(dest => dest.Place, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Place) ? null : new Place { Name = src.Place }))
            //    .ReverseMap();

            CreateMap<PostItemRequestDto, PostItem>()
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.Place, opt => opt.Ignore())
                .ForMember(dest => dest.PlaceId, opt => opt.MapFrom(src => src.PlaceId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Content) ? "" : src.Content)) // Handle optional content
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ReverseMap();

            CreateMap<PostItem, PostItemRequestDto>().ReverseMap();
                
        }
    }
}
