namespace Front_End.Controllers
{
    public class DailyActivityData
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public int BlogPosts { get; set; }
        public int Comments { get; set; }
        public int Downvotes { get; set; }
        public int Upvotes { get; set; }
    }

}
