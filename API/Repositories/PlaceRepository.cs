using API.Data;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly StoreContext _dbcontext;
        private readonly IMapper _mapper;
        public PlaceRepository(StoreContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Place>> GetPlasesItemsAsync()
        {
            return await _dbcontext.Places.ToListAsync();
        }
    }
}
