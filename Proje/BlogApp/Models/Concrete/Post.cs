namespace BlogApp.Models.Concrete
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        // Navigation properties
        public List<Comment>? Comments { get; set; } = [];
        public List<PostTag>? PostTags { get; set; } = [];
    }
}
