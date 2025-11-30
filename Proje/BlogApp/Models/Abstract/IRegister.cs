namespace BlogApp.Models.Abstract
{
    public interface IRegister
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
