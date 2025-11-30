namespace BlogApp.Models.Concrete
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<PostTag>? PostTags { get; set; } = []; //gEÇİŞ POST TAGINI içerir sebei ise her postun bir tagı olma ihtimaline karşıdır ama nullable yapmalıyızıdr
    }
}
