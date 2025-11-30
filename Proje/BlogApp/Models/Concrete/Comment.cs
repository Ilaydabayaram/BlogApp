namespace BlogApp.Models.Concrete
{
    public class Comment
    {
        public int Id { get; set; } //Yorumun idsi var
        public string? AuthorName { get; set; } //yorumun bir yazarı var, kime ait olduğu bilgisi burada tutuluyor
        public string? Content { get; set; } //yorumun içeriği kısmı
        public DateTime PostedDate { get; set; } = DateTime.Now; //initial olarak datetime now şimiki zaman ekleniyor sebebi ise bir yorum oluşturduğun gibi oluşturma tairih güncellensin 
        public int? AdminId { get; set; } //admin yorumların tamamına ulaşabilsin diye burada bir FOREING KEY TUTUYORUZ VE ALTINA NAVİGATİON PROPERTYSİNİ YAZIYORUZ
        public Admin? Admin { get; set; } //bu navigation propery----> entity framework core biiz anlasın ve aralarında bir ilişki olduğunu görsün diye yönlendirme yapıyor 
        public int? UserId { get; set; } //Aynı şekilde userıd olarak bir foreign key taşıyor, ef core anlasın diye navigation propu altına yazıyoruz
        public User? User { get; set; } //navigation prop
        // Foreign key
        public int PostId { get; set; } //bu foreign key kısmı postlar da yorumlarla ilişkii olacak, bu sebeple post id tutuluyor
        // Navigation property
        public Post? Post { get; set; } //navigatio porp
    }
}
