namespace BlogApp.Models.Concrete
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; } //her postun bir başlığı olur
        public string? Content { get; set; } //her postun içeriği vardır
        public int? UserId { get; set; } //her postun bir yazarı vardır bu sebeple userıd içinde tutar burada da çoka çoka ilişki olsun diye (many-to-many ) ilişki yani her postun bir yazarı olabileceği gibi her yazarın da birden çok postu oalbilir. Bu sebeğle çoka çok ilişkiyi bu foreign key belirliyor 
        public User? User { get; set; } //navigation prop
        public int? AdminId { get; set; } //User için ne geçerliyse admin için de aynısı geçerli çoka çok ilişki içidneler 
        public Admin? Admin { get; set; } //navigaiton prop
        public DateTime PublishedDate { get; set; } = DateTime.Now; //inital değer atamaası var yayınlanma tarihi post oluşur oluşmaz atanır
        // Navigation properties
        public List<Comment>? Comments { get; set; } = []; //? ---> nullable demek yani null olmasına izin ver demek, null olabilir commentler yanni ben bir post atacağım zaman yorumu olmasına ya da bir yorrum klistesine gerek yok
        public List<PostTag>? PostTags { get; set; } = []; //aynı şekilde her postun bir etiketle bağlantısı olmak zorunda değil bu sebeple POSTTAG kısmına nullable ekleyebiliriz, yine de boş array ile başlatır
    }
}
