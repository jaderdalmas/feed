using Feed.Models;
using System.Linq;

namespace Feed.Data
{
    public static class SeedFeed
    {

        public static void Initialize(FeedContext context)
        {
            if (context.FeedModel.Any()) return; // DB has been seeded

            for (int i = 1; i < 11; i++)
            {
                var feed = new FeedModel(i);

                var items = 6;
                for (int j = 1; j < items; j++)
                    feed.Items.Add(new FeedItemModel(i * items + j));

                context.FeedModel.Add(feed);
            }

            context.SaveChanges();
        }
    }
}
