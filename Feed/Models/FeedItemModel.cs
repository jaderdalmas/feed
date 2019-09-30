namespace Feed.Models
{
    public class FeedItemModel
    {
        public FeedItemModel() { }

        public FeedItemModel(int i)
        {
            Title = $"Item {i}";
            Details = $"It's a feed item {i}";
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}