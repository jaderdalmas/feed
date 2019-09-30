using System.Collections.Generic;

namespace Feed.Models
{
    public class FeedModel
    {
        public FeedModel() { }

        public FeedModel(int i)
        {
            Title = $"Feed {i}";
            Items = new List<FeedItemModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<FeedItemModel> Items { get; set; }
    }
}
