using AutoMapper;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;

namespace Championship.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieViewModel>().ReverseMap();
        }
    }
}
