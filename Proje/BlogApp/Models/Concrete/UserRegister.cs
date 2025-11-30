using BlogApp.Models.Abstract;

namespace BlogApp.Models.Concrete
{
    public class UserRegister: IRegister
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Id { get; set; }
    }
}
