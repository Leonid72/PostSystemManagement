using API.Models.Dto;

namespace API.Interfaces
{
    public interface IPostItemRepository
    {
        Task<PostItemDto> GetPostItemByIdAsync(int id);
        Task<PostItemDto> AddPostItemAsync(PostItemRequestDto PostItem);
        Task<IReadOnlyList<PostItemDto>> GetPostItemsAsync();
        Task<bool> DeletePostItemByIdAsync(int id);
        //Task<PostItemDto> UpdatePostItemAsync(PostItemDto PostItem);
        Task<PostItemDto> UpdatePostItemAsync(PostItemRequestDto PostItem);

    }
}
