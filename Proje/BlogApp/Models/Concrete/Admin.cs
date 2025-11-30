namespace BlogApp.Models.Concrete
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; } //yine şifreler hashlenir
        public string? ConfirmPassword { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public List<Post> Posts { get; set; } = []; //Admin de aynı şekilde user gibi postlar ve yorumları birden fazla yapabileceği için burada liste halinde tutulur. İnitial değer olarak da boş array dönddürür. 
        public List<Comment> Comments { get; set; } = [];
    }
}
