using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Concrete
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [Display(Name = "Başlık")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "İçerik zorunludur.")]
        [Display(Name = "İçerik")]
        public string? Content { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        public List<Comment>? Comments { get; set; } = [];
        public List<PostTag>? PostTags { get; set; } = [];
    }
}