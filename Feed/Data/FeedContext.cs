using Microsoft.EntityFrameworkCore;
using Feed.Models;

namespace Feed.Data
{
    public class FeedContext : DbContext
    {
        public FeedContext (DbContextOptions<FeedContext> options)
            : base(options)
        {

        }

        public DbSet<Feed.Models.FeedModel> FeedModel { get; set; }

        public DbSet<Feed.Models.FeedItemModel> FeedItemModel { get; set; }
    }
}
