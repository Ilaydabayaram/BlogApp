namespace BlogApp.Models.Concrete
{
    public class User
    {
        public int Id { get; set; } //userın özellikleri, genelde frontta direkt şifre görünmesin dye biz user'ın özelliklerine atarken password kısmını hashleriz 
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Post> Posts { get; set; } = []; //Her kullanıcının birden fazla postu olabileceği için biz burada postlar adı altında bir boş array liste tutuyoruz
        public List<Comment> Comments { get; set; } = []; //Her kullanıcının birden fazla yorumu olabileceği için de biz yorumlar listesi boş array tutuyoruz. Bu en başta hata vermesin diye inital boş array ataması yapıyor. 
    }
}
