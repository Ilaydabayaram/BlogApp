using BlogApp.Models.Abstract;

namespace BlogApp.Models.Concrete
{
    public class AdminRegister: IRegister //neden iki nokta koydun, miras alsın diye, çünkü hem admin hem de user bu registerı kullanacak 
    {
        public string? Name { get; set; } //admin özelinde isim, created at ve name sadece admine özel özellikler
        public string? Email { get; set; } //impenmente ettiklerim 
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Id { get; set; }
    }
}
