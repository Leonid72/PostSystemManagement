using API.Data;
using API.Interfaces;
using API.Models;
using API.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PostItemRepository : IPostItemRepository
    {
        private readonly StoreContext _dbcontext;
        private readonly IMapper _mapper;
        public PostItemRepository(StoreContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public async Task<PostItemDto> AddPostItemAsync(PostItemRequestDto PostItemRequestDto)
        {
        //    var imagePath = Path.Combine("wwwroot", "uploads", PostItemRequestDto.Image.FileName);
        //    using (var stream = new FileStream(imagePath, FileMode.Create))
        //    {
        //        await PostItemRequestDto.Image.CopyToAsync(stream);
        //    }


            PostItem PostItem = _mapper.Map<PostItem>(PostItemRequestDto);
            await _dbcontext.AddAsync(PostItem);
            // Save changes to the database
            await _dbcontext.SaveChangesAsync();
            return _mapper.Map<PostItemDto>(PostItem);
        }

        public async Task<bool> DeletePostItemByIdAsync(int id)
        {
            PostItem PostItem = await _dbcontext.PostItems.FindAsync(id);

            // If the item is not found, return false
            if (PostItem == null)
                return false;
            _dbcontext.PostItems.Remove(PostItem);
            int changes = await _dbcontext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<PostItemDto> GetPostItemByIdAsync(int id)
        {
            PostItem PostItem = await _dbcontext.PostItems.FirstOrDefaultAsync(x => x.PostItemId == id);
            return _mapper.Map<PostItemDto>(PostItem);
        }

        public async Task<IReadOnlyList<PostItemDto>> GetPostItemsAsync()
        {
            List<PostItem> PostItems = await _dbcontext.PostItems.Include(b => b.Place).ToListAsync();
            // Map the list of PostItem entities to a list of PostItemDto
            return _mapper.Map<IReadOnlyList<PostItemDto>>(PostItems);
        }

        public async Task<PostItemDto> UpdatePostItemAsync(PostItemRequestDto PostItemRequestDto)  
        {
             // Retrieve the existing PostItem from the database
            PostItem PostItem = await _dbcontext.PostItems.FindAsync(PostItemRequestDto.PostItemId);

            if (PostItem == null)
                return null;

            _mapper.Map(PostItemRequestDto, PostItem);
            await _dbcontext.SaveChangesAsync();
            return _mapper.Map<PostItemDto>(PostItem);
        }
    }
}
