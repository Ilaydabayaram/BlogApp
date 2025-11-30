namespace BlogApp.Models.Concrete
{
    public class Comment
    {
        public int Id { get; set; }
        public string? AuthorName { get; set; }
        public string? Content { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        // Foreign key
        public int PostId { get; set; }
        // Navigation property
        public Post? Post { get; set; }
    }
}
