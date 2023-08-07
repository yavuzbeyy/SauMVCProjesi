using HayvanBarinagi.Models;
using HayvanBarinagi.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HayvanBarinagi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HayvanControllers : ControllerBase
    {
        private readonly HayvanRepository _hayvanrepo;

        public HayvanControllers(HayvanRepository hayvanrepo)
        {
            _hayvanrepo = hayvanrepo;
        }


        // veritabanından hayvanları sorgulayacak istek
        [HttpGet]
        public IActionResult GetAll()
        {
            var tumHayvanlar = _hayvanrepo.Queryable().ToListAsync().Result;
            if (tumHayvanlar != null)
            {
                return Ok(tumHayvanlar);
            }
            return BadRequest("Hayvanlar Listesi Bulunamadı.");

        }


        [HttpPost]
        public IActionResult Create([FromBody] Hayvan hayvan)
        {
            var YeniHayvan = new Hayvan
            {
                Adi = hayvan.Adi,
                Yas = hayvan.Yas,
                Cins = hayvan.Cins,
            };
            _hayvanrepo.Add(YeniHayvan);
            _hayvanrepo.SaveChanges();

            return Ok(YeniHayvan);
        }

        // hayvanın adıyla bulacak linq sorgusu
        [HttpPut]
        public IActionResult Update([FromBody] Hayvan hayvan)
        {
            var SecilenHayvan = _hayvanrepo.Queryable().Where(x => x.Adi == hayvan.Adi).FirstOrDefault();

            if (SecilenHayvan != null)
            {
                SecilenHayvan.Cins = hayvan.Cins;
                SecilenHayvan.Adi = hayvan.Adi;
                SecilenHayvan.Yas = hayvan.Yas;
                _hayvanrepo.SaveChanges();
                return Ok(SecilenHayvan);
            }
            else {
                return BadRequest("Hayvan bulunamadı.");
            } 
        }

        // Hayvanın id sine göre veritabanından silme işlemi
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var SecilenHayvan = _hayvanrepo.Queryable().Where(x => x.Id == id).FirstOrDefault();
            if (SecilenHayvan != null)
            {
                _hayvanrepo.Delete(SecilenHayvan);
                _hayvanrepo.SaveChanges();
                return Ok("Hayvan kaldırıldı.");
            }
            else {
            return BadRequest("Hayvan bulunamadı.");
            }       
        }


    }
}
