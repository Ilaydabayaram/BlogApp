using BlogApp.Models.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    //Entity framework core sayesinde dbcontext isimli bir classtan miras alabilir bu daa bizim migraation atmamız için oluşturacağımız tabloları ve bağlantıları içeren ONMODELCREATİNG isiml methoda sahiptir. Bu method sayesinde vertabanındaki tablolarımızı kurgulayabilmiş, foreign key ve primary keyleri bu özelde ezebilir hale gelriz. Hatta dbsetleri yazmamızı sağlayan yer tam olarak burasıdır. Dbseetler benim veritabanımdaki tablo isimlerimi olulturacaktır. 
    public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Posttag için belirledik
            modelBuilder.Entity<PostTag>()
             .HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTag>()
             .HasOne(pt => pt.Post)
             .WithMany(p => p.PostTags)
             .HasForeignKey(pt => pt.PostId); //BİR TANE POST İÇERİR, BİR SÜRÜ POSTTAGLARIYLA BİRLİKTE, FOREİGN KEY OLARAK DA POST İD'Yİ BAZ ALIR

            modelBuilder.Entity<PostTag>()
             .HasOne(pt => pt.Tag)
             .WithMany(t => t.PostTags)
             .HasForeignKey(pt => pt.TagId); //BİR TANE TAG İÇERİR, BİR SÜRÜ POSTTAGLARIYLA BİRLİKTE, FOREİGN KEY OLARAK DA TAG İD'Yİ BAZ ALIR

            modelBuilder.Entity<Comment>()
             .HasOne(c => c.User)
             .WithMany(u => u.Comments)
             .HasForeignKey(c => c.UserId) //bağlantılardan dolayı silemeyebiliriz.
             .OnDelete(DeleteBehavior.Restrict); //ON DELETE BEHAVİOUR---> SİLME DURUMLARINI BURADA KONTROL EDEBİLİRİZ. şU AN RESTRİCT MODUNDA. rESTRİCT KISITLAMA DEMEK. Kısıtlama--> bu modda aslında sen veritabanında bir yorumu silmeyi engellemek için yapılır.
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Admin> Admin { get; set; }
    }
}
