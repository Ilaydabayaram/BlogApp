using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Concrete
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        public string? AuthorName { get; set; }

        [Required(ErrorMessage = "Yorum içeriği boş olamaz.")]
        public string? Content { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}