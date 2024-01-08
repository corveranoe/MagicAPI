using AutoMapper;
using Magic_API.Modelos;
using Magic_API.Modelos.Dto;

namespace Magic_API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Magic, MagicDto>();
            CreateMap<MagicDto, Magic>();

            CreateMap<Magic, MagicCreateDto>().ReverseMap();
            CreateMap<Magic, MagicUpdateDto>().ReverseMap();
        }
    }
}
