using API.Models;
using API.Models.Dto;

namespace API.Interfaces
{
    public interface IPlaceRepository
    {
        Task<IReadOnlyList<Place>> GetPlasesItemsAsync();

    }
}
