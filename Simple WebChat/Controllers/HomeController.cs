using Simple_WebChat.Models.Ctx;
using Simple_WebChat.Models.Mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple_WebChat.Controllers
{
    public class HomeController : Controller
    {
        ChatDBContext DB = new ChatDBContext();
        // GET: Home
        public ActionResult Chat()
        {

            return View(DB.Mesaj.ToList());
        }

        public ActionResult Index()
        {
            if (!DB.Database.Exists())
            {
                DB.Database.Create();
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string KullaniciAdi, string KullaniciEmail)
        {
            Users user = DB.User.Where(k => k.UserName == KullaniciAdi || k.Email == KullaniciEmail).FirstOrDefault();
            
            if (user == null)
            {
                DB.User.Add(new Users
                {
                    UserName = KullaniciAdi,
                    Email = KullaniciEmail,
                });
                int sonuc= DB.SaveChanges();
                if (sonuc == 1)
                {
                    Session["isim"] = KullaniciAdi;
                    return RedirectToAction("Chat", "Home");
                }

            }
            else {
                Session["isim"] = KullaniciAdi;
                return RedirectToAction("Chat", "Home");
                //ViewBag.Loginmesaj = "Kayıtlı kullanıcı!!";
            }

            ViewBag.Loginmesaj = "Kullanıcı Adı veya e-maili hatalı!!";
            return View();
        }
    }
}