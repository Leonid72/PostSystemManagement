using API.Interfaces;
using API.Models;
using API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PostItemController : BaseApiController
    {
        protected PostItemDto _response;
        private readonly IPostItemRepository _itemRepository;
        public PostItemController(IPostItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _response = new PostItemDto();
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PostItemDto>>> Get()
        {
            IReadOnlyList<PostItemDto> PostItems = await _itemRepository.GetPostItemsAsync();
            return Ok(PostItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostItemDto>> Get(int id)
        {
            // Validate the ID (if applicable)
            if (id <= 0)
                return BadRequest("Invalid ID provided.");

            PostItemDto PostItem = await _itemRepository.GetPostItemByIdAsync(id);
            return PostItem == null ? NotFound($"PostItem with ID {id} not found.") : Ok(PostItem);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PostItemRequestDto postItemRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);// Return a bad request with validation errors
            string imagePath = string.Empty;

            if (postItemRequestDto.Image != null && postItemRequestDto.Image.Length > 0)
            {
                // Generate a unique filename for the image
                string fileName = $"{Guid.NewGuid()}_{postItemRequestDto.Image.FileName}";

                // Define the path where the file will be saved
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)!);

                // Save the file to the server
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await postItemRequestDto.Image.CopyToAsync(stream);
                }

                // Set the relative path to the file
                postItemRequestDto.ImagePath = $"/images/{fileName}";
            }


            PostItemDto createdPostItem = await _itemRepository.AddPostItemAsync(postItemRequestDto);
            return Ok(createdPostItem);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostItemDto>> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID provided.");

            bool deleted = await _itemRepository.DeletePostItemByIdAsync(id);
            return deleted ? NoContent() : NotFound($"PostItem with ID {id} not found."); // 404 Not Found
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody] PostItemRequestDto postItemRequestDto )
        {
            if (!ModelState.IsValid)
                 return BadRequest(ModelState);  // Return a bad request with validation errors
            
            if (id != postItemRequestDto.PostItemId)
                 return BadRequest("Task ID mismatch.");

            PostItemDto createdPostItem = await _itemRepository.UpdatePostItemAsync(postItemRequestDto);
            return createdPostItem == null ? NotFound($"PostItem with ID {postItemRequestDto.PostItemId} not found.") 
                                           : Ok(createdPostItem);
        }

        
    }
}
