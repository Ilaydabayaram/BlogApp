using BlogApp.Models.Abstract;

namespace BlogApp.Models.Concrete
{
    public class UserRegister: IRegister
    {
        public string? UserName { get; set; } //username özel user için
        public string? Email { get; set; } //implemente edildi
        public string? Password { get; set; } //implemente edildi
        public string? ConfirmPassword { get; set; }//implemente edildi
        public DateTime CreatedAt { get; set; } = DateTime.Now; //intial bir başlangıç değeri atadım, regisrter olur olmaz bana şu anın tarihini atasın//implemente edildi
        public int Id { get; set; }//implemente edildi
    }
}
