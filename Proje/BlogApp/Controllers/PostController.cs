using BlogApp.Data;
using BlogApp.Models.Concrete;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogContext _context;

        public PostController(BlogContext context) => _context = context;

        public async Task<IActionResult> Index(string searchString)
        {
            var posts = _context.Posts.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Title.Contains(searchString) || s.Content.Contains(searchString));
            }
            // Tarihe göre tersten sırala (en yeni en üstte)
            return View(await posts.OrderByDescending(p => p.PublishedDate).ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) return NotFound();

            var post = await _context.Posts
                .Include(p => p.Comments) // Yorumları dahil et
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null) return NotFound();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            // Comment modeline [Required] eklediğimiz için ModelState çalışacak
            if (ModelState.IsValid)
            {
                comment.PostedDate = DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = comment.PostId });
            }
            // Hata varsa sayfayı tekrar yükle
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(m => m.Id == comment.PostId);
            post.Comments.Add(comment); // Hatalı yorumu da listeye ekle ki kullanıcı ne yazdığını görsün (frontendde ele alınmalı)
            return View("Details", post);
        }

        [Authorize] // Sadece giriş yapanlar görebilir
        public IActionResult Create()
        {
            return View();
        }

        // Create kısmında da kime ait olduğunu kaydetmemiz lazım:
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);

                // Eğer giriş yapan kişi User tablosundaysa:
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    post.UserId = user.Id;
                }
                else
                {
                    // Yoksa Adminde ara
                    var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == email);
                    if (admin != null) post.AdminId = admin.Id;
                }

                post.PublishedDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [Authorize] // Sadece giriş yapanlar görebilir
        public async Task<IActionResult> MyPosts()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // Çerezden emaili al

            // User ise kendi id'siyle, Admin ise kendi id'siyle eşleşen postları getir
            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Admin)
                .Where(p => p.User.Email == userEmail || p.Admin.Email == userEmail)
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();

            return View("Index", posts); // Index view'ını kullanarak listele
        }

        // 2. ÖZELLİK: GÜVENLİ EDİT (BAŞKASI DÜZENLEYEMESİN)
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var post = await _context.Posts.Include(p => p.User).Include(p => p.Admin).FirstOrDefaultAsync(x => x.Id == id);
            if (post == null) return NotFound();

            // GÜVENLİK KONTROLÜ:
            // Eğer kullanıcı Admin DEĞİLSE VE yazı kullanıcıya ait DEĞİLSE hata ver.
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            var isAdmin = User.IsInRole("Admin");

            // Yazı User tarafından yazılmışsa ve şu anki giren o user değilse
            if (!isAdmin && post.User?.Email != currentUserEmail && post.Admin?.Email != currentUserEmail)
            {
                return Unauthorized(); // Yetkiniz yok sayfasına atar veya hata verir
            }

            return View(post);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id)) return NotFound();
                    throw;
                }
            }
            return View(post);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        // GET: Post/Delete/5
        [Authorize] // Sadece giriş yapanlar
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            // Postu, yazarı ve admin bilgisiyle birlikte çekiyoruz
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null) return NotFound();

            // --- GÜVENLİK KONTROLÜ BAŞLANGICI ---
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            var isAdmin = User.IsInRole("Admin");

            // Yazının sahibi ben değilsem VE Admin değilsem -> Yetkisiz işlem
            bool isOwner = (post.User != null && post.User.Email == currentUserEmail) ||
                           (post.Admin != null && post.Admin.Email == currentUserEmail);

            if (!isOwner && !isAdmin)
            {
                return Unauthorized(); // Veya RedirectToAction("Index");
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")] // URL'de /Delete olarak görünsün ama metod adı DeleteConfirmed olsun
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts
                        .Include(p => p.User)
                        .Include(p => p.Admin)
                        .FirstOrDefaultAsync(m => m.Id == id);

            if (post != null)
            {
                // --- GÜVENLİK KONTROLÜ (POST için tekrar kontrol şart) ---
                var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
                var isAdmin = User.IsInRole("Admin");

                bool isOwner = (post.User != null && post.User.Email == currentUserEmail) ||
                               (post.Admin != null && post.Admin.Email == currentUserEmail);

                if (!isOwner && !isAdmin)
                {
                    return Unauthorized();
                }

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}