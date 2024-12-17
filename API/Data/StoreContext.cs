using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext>  options): base(options) { }

        public DbSet<PostItem> PostItems { get; set; }
        public DbSet<Place>  Places { get; set; }
    }
}
