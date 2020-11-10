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
            CreateMap<Tournament, TournamentViewModel>().ReverseMap();
            CreateMap<Ranking, RankingViewModel>()
                .ForMember(d => d.MovieTitulo, opt => opt.MapFrom(src => src.Movie.Titulo))
                .ReverseMap();
            CreateMap<Standing, StandingViewModel>().ReverseMap();
        }
    }
}
