using HayvanBarinagi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace HayvanBarinagi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                // Seçilen dili, tarayıcı çerezlerine kaydedelim
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                // returnUrl boş değilse ve güvenli bir yerel URL ise o sayfaya yönlendir
                return LocalRedirect(returnUrl);
            }
            else
            {
                // returnUrl boş veya güvenli bir yerel URL değilse varsayılan olarak Anasayfa'ya yönlendir
                return RedirectToAction("Index", "Home");
            }
        }

        Context context = new Context();
        public IActionResult Index()
        {
            var hayvanListesi = context.Hayvanlar.ToList();
            return View(hayvanListesi);
        }
        
        public IActionResult Hayvanlar()
        {
            var hayvanListesi = context.Hayvanlar.ToList();
            return View(hayvanListesi);
        }
        public IActionResult HayvanSil(int Id)
        {
            var SecilenHayvan = context.Hayvanlar.Find(Id);
            context.Hayvanlar.Remove(SecilenHayvan);
            context.SaveChanges();

            return RedirectToAction("Hayvanlar");
        }
        [HttpGet]
        public IActionResult HayvanEkle()
        {

            return View();
        }
        [HttpPost]
        public IActionResult HayvanEkle(Hayvan hayvan)
        {
            var YeniHayvan = new Hayvan
            {
                Adi = hayvan.Adi,
                Yas = hayvan.Yas,
                Cins = hayvan.Cins,
            };
            context.Hayvanlar.Add(YeniHayvan);
            context.SaveChanges();

            return RedirectToAction("Hayvanlar");
        }
        [HttpGet]
        public IActionResult HayvanDuzenle(int Id)
        {

            var SecilenHayvan = context.Hayvanlar.Find(Id);
            return View(SecilenHayvan);
        }
        [HttpPost]
        public IActionResult HayvanDuzenle(Hayvan hayvan)
        {
            var SecilenHayvan = context.Hayvanlar.Find(hayvan.Id);
            SecilenHayvan.Cins = hayvan.Cins;
            SecilenHayvan.Adi = hayvan.Adi;
            SecilenHayvan.Yas = hayvan.Yas;
            context.SaveChanges();

            return RedirectToAction("Hayvanlar");
        }
        public IActionResult Sahiplendirme()
        {
            
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Basvuru()
        {
            var hayvanListesi = context.Hayvanlar.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = $"{h.Adi} - {h.Yas} yaş"
            }).ToList();

            return View(hayvanListesi);
        }
        [HttpPost]
        public IActionResult Basvuru(IFormCollection form)
        {
            // Form verilerini almak için FormCollection nesnesini kullanımı

            string adSoyad = form["adSoyad"];
            string ePosta = form["ePosta"];
            string telNo = form["telNo"];
            string adres = form["adres"];
            int selectedAnimalId = Convert.ToInt32(form["id"]);
            string gelir = form["gelir"];
            string evTipi = form["evTipi"];
            string petExperience = form["pet_experience"];


            // Veritabanına kaydetme işlemleri (örnek olarak):
            using (var dbContext = new Context())
            {
                // Başvuru modeli oluşturulması ve veritabanına eklenmesi
                Sahiplenme basvuru = new Sahiplenme
                {
                    adSoyad = adSoyad,
                    ePosta = ePosta,
                    telNo = telNo,
                    adres = adres,
                    hayvanId = selectedAnimalId,
                    gelir = gelir,
                    evTipi = evTipi,
                    deneyim = petExperience == "yes"
                };

                dbContext.Sahiplenme.Add(basvuru);
                dbContext.SaveChanges();
            }

            // Başarılı bir şekilde kaydedildiğinde başka bir sayfaya yönlendirme yapabilirsiniz.
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public IActionResult HayvanVerme()
        {

            return View();
        }
        [HttpPost]
        public IActionResult HayvanVerme(IFormCollection form)
        {

            string hayvanAd = form["hayvanAdi"];
            string hayvanTur = form["hayvanTuru"];
            int yas = Convert.ToInt32(form["yas"]);
            string cinsiyet = form["cinsiyet"];
            string saglikDurumu = form["saglikDurumu"];
            string aciklama = form["aciklama"];

            // Veritabanına kaydetme işlemleri (örnek olarak):
            using (var dbContext = new Context())
            {
                // Başvuru modeli oluşturulması ve veritabanına eklenmesi
                HayvanVerme basvuru = new HayvanVerme
                {
                    hayvanAd = hayvanAd,
                    hayvanTur = hayvanTur,
                    yas = yas,
                    cinsiyet = cinsiyet,
                    saglikDurumu = saglikDurumu,
                    aciklama = aciklama
                };

                dbContext.HayvanVerme.Add(basvuru);
                dbContext.SaveChanges();
            }

            // Başarılı bir şekilde kaydedildiğinde başka bir sayfaya yönlendirme yapabilirsiniz.
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Hakkimizda()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Iletisim()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Iletisim(Iletisim iletisim)
        {
            var newIletisim = new Iletisim
            {
                adSoyad = iletisim.adSoyad,
                ePosta = iletisim.ePosta,
                konu = iletisim.konu,
                mesaj = iletisim.mesaj
            };

            context.Iletisim.Add(newIletisim);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult UyeOl()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult UyeOl(Kullanici kullanici)
        {
            var yeniKullanici = new Kullanici 
            {
                Email = kullanici.Email,
                UserName = kullanici.UserName,
                Password = kullanici.Password,

            };
            context.Kullanicilar.Add(yeniKullanici);
            context.SaveChanges();
            return RedirectToAction("GirisYap");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GirisYap(string returnUrl = null)
        {

            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["LoginGerekli"] = "Bu sayfaya erişebilmek için giriş yapmalısınız.";
                TempData["ReturnUrl"] = returnUrl;
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GirisYapAsync(Kullanici kullanici)
        {                           
            var kontrol = context.Kullanicilar.FirstOrDefault(x => x.UserName==kullanici.UserName && x.Password == kullanici.Password);
            if (kontrol != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, kullanici.UserName)
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                string returnUrl = TempData["ReturnUrl"] as string;
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                if (kontrol.UserName == "g211210370@sakarya.edu.tr")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

            
        }
        public async Task<IActionResult> CikisYap()

        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}