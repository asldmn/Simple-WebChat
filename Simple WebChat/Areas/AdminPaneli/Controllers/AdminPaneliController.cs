using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple_WebChat.Models.Ctx;
using Simple_WebChat.Models.Mdl;

namespace Simple_WebChat.Areas.AdminPaneli.Controllers
{
    public class AdminPaneliController : Controller
    {
        ChatDBContext DB = new ChatDBContext();
        // GET: AdminPaneli/AdminPaneli
        public ActionResult AnaSayfa()
        {
            return View();
        }

        public ActionResult UyeleriListele()
        {
            return View(DB.User.ToList());
        }

        public ActionResult UyeEkle()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult UyeEkle(Users tabloUser)
        {
            DB.User.Add(new Users
            {
                UserName = tabloUser.UserName,
                Email = tabloUser.Email
            });
            int sonuc = DB.SaveChanges();
            if (sonuc > 0)
            {
                return RedirectToAction("UyeleriListele", "AdminPaneli");
            }
            return View();
        }

        public ActionResult UyeSil(int UserId)
        {
            return View(DB.User.Where(k => k.UserId == UserId).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("UyeSil")] //KategoriSil methodunun post işlemini yapacak
        public ActionResult UyeDelete(int UserId)
        {
            var kullanıcıSil = DB.User.Where(k => k.UserId == UserId).FirstOrDefault();
            DB.User.Remove(kullanıcıSil);
            int sonuc = DB.SaveChanges();
            if (sonuc > 0)
            {
                return RedirectToAction("UyeleriListele", "AdminPaneli");
            }
            ViewBag.mesajSil = "<h5 style='color:red'>Bir Hata oluştu</h5>";
            return View(kullanıcıSil);
        }

        public ActionResult UyeGuncelle(int UserId)
        {
            return View(DB.User.Where(k => k.UserId == UserId).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UyeGuncelle(int UserId, Users tabloUser)
        {
            Users guncelleUser = DB.User.Where(k => k.UserId == UserId).FirstOrDefault();

            if (guncelleUser != null) {
                guncelleUser.UserName = tabloUser.UserName;
                guncelleUser.Email = tabloUser.Email;

                int sonuc = DB.SaveChanges();
                if (sonuc > 0)
                {
                    return RedirectToAction("UyeleriListele", "AdminPaneli");
                }
            }

            TempData["UyeGuncelle"] = "<h2 style='color:red'>Güncelleme Olmadı!!</h2>";
            return View(DB.User.Where(k => k.UserId == UserId).FirstOrDefault());
        }
    }
}