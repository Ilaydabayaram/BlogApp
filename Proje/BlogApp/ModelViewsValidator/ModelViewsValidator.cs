using BlogApp.ModelView;
using FluentValidation;

namespace BlogApp.ModelViewValidator
{
    public class ModelViewsValidator: AbstractValidator<ModelViews>
    {
        public ModelViewsValidator()
        {
            //Admin
            When(a => a.Admin != null, () =>
            {
                RuleFor(a => a.Admin)
                    .NotNull();

                RuleFor(a => a.Admin.Name)
                    .NotEmpty()
                    .WithMessage("Admin adı boş olamaz!");


                RuleFor(a => a.Admin.Email)
                    .NotEmpty()
                    .WithMessage("Admin email boş olamaz!")
                    .EmailAddress()
                    .WithMessage("Geçerli bir email adresi giriniz!");

            });
            //User
            When(u => u.User != null, () =>
            {
                RuleFor(u => u.User)
                   .NotNull();

                RuleFor(u => u.User.UserName)
                    .NotEmpty()
                    .WithMessage("Kullanıcı Adı boş bırakılamaz.")
                    .MaximumLength(50)
                    .WithMessage("Kullanıcı Adı en fazla 50 karakter olmalıdır.");

                RuleFor(u => u.User.Email)
                    .NotEmpty()
                    .WithMessage("E-posta boş bırakılamaz.")
                    .EmailAddress()
                    .WithMessage("Geçerli bir E-posta adresi girin.");

                RuleFor(u => u.User.PasswordHash)
                    .NotEmpty()
                    .WithMessage("Parola boş bırakılamaz.")
                    .MinimumLength(6)
                    .WithMessage("Parola en az 6 karakter olmalıdır.");

            });
            //Post
            When(p => p.Post != null, () =>
            {

                RuleFor(p => p.Post.Title)
                    .NotEmpty()
                    .WithMessage("Başlık boş bırakılamaz.")
                    .MaximumLength(200)
                    .WithMessage("Başlık en fazla 200 karakter olmalıdır.");

                RuleFor(p => p.Post.Content)
                    .NotEmpty()
                    .WithMessage("İçerik boş bırakılamaz.");

            });
            //Comment
            When(c => c.Comment != null, () =>
            {
                RuleFor(c => c.Comment)
                    .NotNull();

                RuleFor(c => c.Comment.AuthorName)
                    .NotEmpty()
                    .WithMessage("Yazar Adı boş bırakılamaz.");

                RuleFor(c => c.Comment.Content)
                    .NotEmpty()
                    .WithMessage("Yorum içeriği boş bırakılamaz.");

            });
            //Tag
            When(t => t.Tag != null, () =>
            {
                RuleFor(t => t.Tag)
                    .NotNull();
                RuleFor(t => t.Tag.Name)
                    .NotEmpty()
                    .WithMessage("Etiket Adı boş bırakılamaz.")
                    .MaximumLength(50)
                    .WithMessage("Etiket Adı en fazla 50 karakter olmalıdır.");
            });
            //UserRegister
            When(ur => ur.UserRegister != null, () =>
            {
                RuleFor(ur => ur.UserRegister.Password)
                   .NotEmpty()
                   .WithMessage("Parola boş bırakılamaz.")
                   .MinimumLength(6)
                   .WithMessage("Parola en az 6 karakter olmalıdır.");
                RuleFor(ur => ur.UserRegister.Email)
                    .NotEmpty()
                    .WithMessage("Kayıt maili boş bırakılamaz.")
                    .MaximumLength(50)
                    .EmailAddress()
                    .WithMessage("Geçerli bir e-posta adresi giriniz.");
                RuleFor(ur => ur.UserRegister.ConfirmPassword)
                    .NotEmpty()
                    .WithMessage("Parola doğrulama boş bırakılamaz.")
                    .MinimumLength(6)
                    .WithMessage("Parola en az 6 karakter olmalıdır.")
                    .Equal(ur => ur.UserRegister.Password)
                    .WithMessage("Parolalar eşleşmelidir.");

            });
            //AdminRegister
            When(ar => ar.AdminRegister != null, () =>
            {
                RuleFor(ar => ar.AdminRegister.Email)
                    .NotEmpty()
                    .WithMessage("Kayıt maili boş bırakılamaz.")
                    .MaximumLength(50)
                    .EmailAddress()
                    .WithMessage("Geçerli bir e-posta adresi girin.");
                RuleFor(ar => ar.AdminRegister.Password)
                    .NotEmpty()
                    .WithMessage("Kayıt parolası zorunludur.")
                    .MinimumLength(6)
                    .WithMessage("Parola en az 6 karakter olmalıdır.");
                RuleFor(ar => ar.AdminRegister.ConfirmPassword)
                    .NotEmpty()
                    .WithMessage("Parola doğrulama boş bırakılamaz")
                    .MinimumLength(6)
                    .WithMessage("Parola en az 6 karakter olmalıdır.")
                    .Equal(ar => ar.AdminRegister.Password)
                    .WithMessage("Parolalar eşleşmelidir.");
            });
            //PostTag
            When(pt => pt.PostTag != null, () =>
            {
                RuleFor(pt => pt.PostTag.PostId)
                    .GreaterThan(0)
                    .WithMessage("Post Id 0'dan büyük olmalıdır.");
                RuleFor(pt => pt.PostTag.TagId)
                    .GreaterThan(0)
                    .WithMessage("Tag Id 0'dan büyük olmalıdır.");
            });
        }
    }
}
