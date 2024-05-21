using System;
using System.IO; // Eklenen satır
using denemeKnowling.Data;
using denemeKnowling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using denemeKnowling.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using X.PagedList;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;


namespace denemeKnowling.Controllers
{
    public class KurslarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public KurslarController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;

        }


        public async Task<IActionResult> Index(string SearchString, int page = 1)
        {
            ViewData["CurrentFilter"] = SearchString;
            var kurss = from b in _context.Kurs select b;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kurss = kurss.Where(b => b.Adı.Contains(SearchString) || b.Açıklaması.Contains(SearchString));
            }
            return View(kurss.ToPagedList(page, 3));
        }


        //Goruntule

        public IActionResult Görüntüle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var krs = _context.Kurs.Find(id);
            if (krs == null)
            {
                return NotFound();
            }
            return View(krs);
        }

        //Ekleme

        //yeni

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(CreateViewKurs krs)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                //Dosyatipi ve büyüklüğü
                var mimeType = krs.Photo.ContentType;
                var permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!permittedMimeTypes.Contains(mimeType))
                {
                    TempData["FileError"] = "Dosya tipi uyumsuz jpg,png veya gif uzantılı resim seçiniz.";
                    return View();
                }
                //Validating the File Size
                if (krs.Photo.Length > 10000000) // Limit to 10 MB
                {
                    TempData["FileError"] = "Dosyanın boyutu 10MB'dan daha büyük.";
                    return View();
                }

                //Dosya yükleme
                if (krs.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + krs.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    krs.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Kurs newKurs = new Kurs
                {
                    Adı = krs.Adı,
                    Açıklaması = krs.Açıklaması,
                    Kategorisi = krs.Kategorisi,
                    Fiyatı = krs.Fiyatı,
                    Süresi = krs.Süresi,
                    Öğretmeni = krs.Öğretmeni,
                    PhotoPath = uniqueFileName


                };

                _context.Kurs.Add(newKurs);
                _context.SaveChanges();
                TempData["SuccessMsg"] = krs.Adı + " isimli ürün , ürün listesine eklendi";
                return RedirectToAction("Index");
            }


            return View();

        }



        public IActionResult Düzenle(int? id)
        {
            var kurs = _context.Kurs.Find(id);

            CreateViewKurs createViewKurs = new CreateViewKurs
            {
                Adı = kurs.Adı,
                Açıklaması = kurs.Açıklaması,
                Kategorisi = kurs.Kategorisi,
                Fiyatı = kurs.Fiyatı,
                Süresi = kurs.Süresi,
                Öğretmeni = kurs.Öğretmeni,
                PhotoFileName = kurs.PhotoPath

            };

            if (kurs == null)
            {
                return NotFound();
            }
            return View(createViewKurs);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Düzenle(CreateViewKurs krs)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = krs.PhotoFileName;
                if (krs.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + krs.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    krs.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Kurs updateKurs = new Kurs
                {
                    Id = krs.Id,
                    Adı = krs.Adı,
                    Açıklaması = krs.Açıklaması,
                    Kategorisi = krs.Kategorisi,
                    Fiyatı = krs.Fiyatı,
                    Süresi = krs.Süresi,
                    Öğretmeni = krs.Öğretmeni,
                    PhotoPath = uniqueFileName
                };
                _context.Kurs.Update(updateKurs);
                _context.SaveChanges();
                TempData["SuccessMsg"] = updateKurs.Adı + " isimli ürün , güncellendi";
                return RedirectToAction("Index");
            }
            return View(krs);
        }






        //Silme

        public IActionResult Sil(int? id)
        {
            var kurs = _context.Kurs.Find(id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SilKurs(int? id)
        {

            var kurs = _context.Kurs.Find(id);
            if (kurs == null)
            {
                return NotFound();
            }
            _context.Kurs.Remove(kurs);
            _context.SaveChanges();
            TempData["SuccessMsg"] = kurs.Adı + " isimli ürün, ürün listesinden silindi";
            return RedirectToAction("Index");
        }



        ////giriş

        ////   [AllowAnonymous]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult UserRegister(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingUser = _context.Users.FirstOrDefault(u => u.EMail == user.EMail);
        //        if (existingUser != null)
        //        {
        //            TempData["RegisterMsg"] = "Bu e-posta adresi zaten kullanılıyor.";
        //            return View("Register");
        //        }

        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        TempData["RegisterMsg"] = "Lütfen Bilgileri Eksiksiz ve Doğru Girin!";
        //    }
        //    return View("Register");
        //}
    }
}


