namespace BlogApp.Models.Abstract
{
    public interface IRegister //Sadece kayıt olurken kullanılacak özellikleri içeriyor
    {
        public int Id { get; set; } //Hangi id ile kayıt oluyorsan
        public string Email { get; set; } //Hangi emaili kullanıyorsan 
        public string Password { get; set; } //Hangi şifreyi seçtiysen
        public string ConfirmPassword { get; set; } //Aynı şifreyi tekrar girebilelim diye bir özellik
        public DateTime CreatedAt { get; set; }
    }
} //Bu sayfayı neden interface olarak açtın? çünkü alt sınıflar üst sınıfları miras alsın ve solide uysun
//solid prensiplerinin single responsibility, open close, interface segregation prensiplerine uydurmak amaçlı böyle bir sayfa açtım. 
