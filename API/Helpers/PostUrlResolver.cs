using API.Models.Dto;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class PostUrlResolver :IValueResolver<PostItem, PostItemDto, string>
    {
        private readonly IConfiguration _config;
        public PostUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(PostItem source, PostItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImagePath))
            {
                return _config["ApiUrl"] + source.ImagePath;
            }
            return null;
        }
    }
}
