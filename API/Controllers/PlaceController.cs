using API.Interfaces;
using API.Models;
using API.Models.Dto;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PlaceController : BaseApiController
    {
       private readonly IPlaceRepository _placeRepository;

        public PlaceController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Place>>> Get()
        {
            IReadOnlyList<Place> PostItems = await _placeRepository.GetPlasesItemsAsync();
            return Ok(PostItems);
        }


    }
}
