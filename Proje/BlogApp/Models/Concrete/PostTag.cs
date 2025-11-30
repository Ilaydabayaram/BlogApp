namespace BlogApp.Models.Concrete
{
    public class PostTag //KENDİNE AİT BİR İDSİ YOK UNUTMA BUNU SADECE İÇİNDE FOREİNG KEYLER TAŞIR KEND PRİMARY KEYİ YOKTUR
    {
        //GEÇŞ İÇİN KULLANILIR. burada postlar ve taglar arasındaki bağlantular yazılır. Bu ayede ben herhangi bir post ve herhangi bir etiket için veritabanında kolaylıkla bu bağlantıyı araştırabilirim sırf kolaylık olsun diye bir geçiş sayfasıdır. 
        public int PostId { get; set; } //foreign key 
        public Post? Post { get; set; } //navigation prop
        public int TagId { get; set; } //foreign key 
        public Tag? Tag { get; set; }//navigation prop
    }
}
