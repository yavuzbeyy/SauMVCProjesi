using HayvanBarinagi.Models;
using HayvanBarinagi.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HayvanBarinagi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciControllers : ControllerBase
    {
        private readonly KullaniciRepository _kullaniciRepo;

        public KullaniciControllers(KullaniciRepository kullaniciRepo)
        {
            _kullaniciRepo = kullaniciRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tumKayitliKullanicilar = _kullaniciRepo.List();
            if (tumKayitliKullanicilar != null)
            {
                return Ok(tumKayitliKullanicilar);
            }
            return BadRequest("Kullanicilar Listesi Bulunamadı.");

        }

        [HttpPost]
        public IActionResult Create([FromBody] Kullanici kullanici)
        {
            var YeniKullanici = new Kullanici
            {
                UserName = kullanici.UserName,
                Email = kullanici.Email,
                Password = kullanici.Password,
            };
            _kullaniciRepo.Add(YeniKullanici);
            _kullaniciRepo.SaveChanges();

            return Ok(YeniKullanici);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Kullanici kullanici)
        {
            var SecilenKullanici = _kullaniciRepo.Queryable().Where(x => x.UserName == kullanici.UserName).FirstOrDefault();

            if (SecilenKullanici != null)
            {
                SecilenKullanici.UserName = kullanici.UserName;
                SecilenKullanici.Email = kullanici.Email;
                SecilenKullanici.Password = kullanici.Password;

                _kullaniciRepo.SaveChanges();
                return Ok(SecilenKullanici);
            }
            else
            {
                return BadRequest("Kullanici bulunamadı.");
            }
        }

        // Kullanicinin id sine göre veritabanından silme işlemi
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var SecilenKullanici = _kullaniciRepo.Queryable().Where(x => x.Id == id).FirstOrDefault();
            if (SecilenKullanici != null)
            {
                _kullaniciRepo.Delete(SecilenKullanici);
                _kullaniciRepo.SaveChanges();
                return Ok("Kullanici kaldırıldı.");
            }
            else
            {
                return BadRequest("Kullanici bulunamadı.");
            }


        }

    }
}
