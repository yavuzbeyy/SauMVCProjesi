using HayvanBarinagi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HayvanBarinagi.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public bool AdminControl()
        {
            if (User.Identity.IsAuthenticated) // Kullanıcı giriş yapmış mı?
            {
                // Burada kullanıcı adını alarak gerekli yetkilendirme kontrollerini yapabilirsiniz.
                string userName = User.Identity.Name;

                if (userName == "g211210370@sakarya.edu.tr") // Kullanıcı adı "admin" mi?
                {
                    // Eğer kullanıcı adı "admin" ise, sayfayı göster.
                    return true;
                }
                else
                {
                    // Eğer kullanıcı adı "admin" değilse, yetkisiz erişimi engelle.
                    return false;
                }
            }
            else
            {
                // Kullanıcı giriş yapmamışsa, yetkisiz erişimi engelle ve giriş sayfasına yönlendir.
                return false;
            }
        }

        Context context = new Context();

        public IActionResult Index()
        {
            if (AdminControl())
            {
                var hayvanListesi = context.Hayvanlar.ToList();
                return View(hayvanListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public IActionResult HayvanEkle()
        {
            if (AdminControl())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult HayvanEkle(Hayvan hayvan)
        {
            var newAnimal = new Hayvan
            {
                Adi = hayvan.Adi,
                Cins = hayvan.Cins,
                Yas = hayvan.Yas
            };

            context.Hayvanlar.Add(newAnimal);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult HayvanDuzenle(int id)
        {
            if (AdminControl())
            {
                var selectedAnimal = context.Hayvanlar.Find(id);
                return View(selectedAnimal);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult HayvanDuzenle(Hayvan hayvan)
        {
            var selectedAnimal = context.Hayvanlar.Find(hayvan.Id);

            selectedAnimal.Adi = hayvan.Adi;
            selectedAnimal.Cins = hayvan.Cins;
            selectedAnimal.Yas = hayvan.Yas;

            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult HayvanSil(int id)
        {
            var selectedAnimal = context.Hayvanlar.Find(id);
            context.Hayvanlar.Remove(selectedAnimal);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Sahiplenme()
        {
            if (AdminControl())
            {
                var basvuruListesi = context.Sahiplenme.ToList();
                return View(basvuruListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult SahiplendirmeOnayla(int id, int animalId)
        {
            var selectedAnimal = context.Hayvanlar.Find(animalId);
            context.Hayvanlar.Remove(selectedAnimal);

            var selectedBasvuru = context.Sahiplenme.Find(id);
            context.Sahiplenme.Remove(selectedBasvuru);
            context.SaveChanges();

            return RedirectToAction("Sahiplenme", "Admin");
        }

        public IActionResult SahiplendirmeSil(int id)
        {
            var selectedBasvuru = context.Sahiplenme.Find(id);
            context.Sahiplenme.Remove(selectedBasvuru);
            context.SaveChanges();

            return RedirectToAction("Sahiplenme", "Admin");
        }

        public IActionResult HayvanVerme()
        {
            if (AdminControl())
            {
                var hayvanVermeListesi = context.HayvanVerme.ToList();
                return View(hayvanVermeListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult HayvanKabulOnayla(int id, string ad, string cins, int yas)
        {
            var newAnimal = new Hayvan
            {
                Adi = ad,
                Cins = cins,
                Yas = yas
            };
            context.Hayvanlar.Add(newAnimal);

            var selectedHayvanVerme = context.HayvanVerme.Find(id);
            context.HayvanVerme.Remove(selectedHayvanVerme);
            context.SaveChanges();

            return RedirectToAction("HayvanVerme", "Admin");
        }

        public IActionResult HayvanKabulSil(int id)
        {
            var selectedHayvanVerme = context.HayvanVerme.Find(id);
            context.HayvanVerme.Remove(selectedHayvanVerme);
            context.SaveChanges();

            return RedirectToAction("HayvanVerme", "Admin");
        }

        public IActionResult Iletisim()
        {
            if (AdminControl())
            {
                var iletisimListesi = context.Iletisim.ToList();
                return View(iletisimListesi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult IletisimSil(int id)
        {
            var selectedIletisim = context.Iletisim.Find(id);
            context.Iletisim.Remove(selectedIletisim);
            context.SaveChanges();

            return RedirectToAction("Iletisim", "Admin");
        }

    }
}
