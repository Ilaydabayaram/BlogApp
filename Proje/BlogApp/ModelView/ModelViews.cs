using BlogApp.Models.Concrete;

namespace BlogApp.ModelView
{
    public class ModelViews //Bütün modellerimi tek bir yerde tutmaya yarar yani o model türünde propertylere sahiptir
    {
        public Admin? Admin { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
        public Tag? Tag { get; set; }
        public PostTag? PostTag { get; set; }
        public UserRegister? UserRegister { get; set; }
        public AdminRegister? AdminRegister { get; set; }
    }
}

