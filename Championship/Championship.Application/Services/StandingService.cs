using AutoMapper;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using Championship.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public class StandingService : IStandingService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Standing> _repository;
        public StandingService(IRepository<Standing> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<StandingViewModel> CreateAsync(Tournament tournament)
        {
            var standing = new Standing(tournament);
            await standing.RunMatchesAsync();
            await _repository.InsertOneAsync(standing);
            return _mapper.Map<StandingViewModel>(standing);
        }

        public async Task<Response<StandingViewModel>> GetByIdAsync(string id)
        {
            var standing = await _repository.GetByIdAsync(id);
            if (standing == null)
                return new Response<StandingViewModel>(false, "Standing with the fetched id does not exist");

            return new Response<StandingViewModel>(true, "Standing search successfully performed", _mapper.Map<StandingViewModel>(standing));
        }
    }
}
