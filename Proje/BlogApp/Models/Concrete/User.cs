namespace BlogApp.Models.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Post> Posts { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];
    }
}
