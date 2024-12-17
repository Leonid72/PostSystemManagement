using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Data.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.PostItems.Any()) 
            {
                var postsJson = File.ReadAllText("./Data/SeedData/PostsSeedData.json");
                var placesJson = File.ReadAllText("./Data/SeedData/PlacesSeedData.json");

                var PostPosts = JsonSerializer.Deserialize<List<PostItem>>(postsJson);
                var places = JsonSerializer.Deserialize<List<Place>>(placesJson);

                context.Places.AddRange(places);
                context.PostItems.AddRange(PostPosts);
                await context.SaveChangesAsync();
            }
        }
    }
}
