using AutoMapper;
using MoviesApi.Core.Dtos;
using MoviesApi.Core.Models;

namespace MoviesApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<MovieUpdateDto,Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<UpdateUserDetailsDto, User>();

        }
    }
}
