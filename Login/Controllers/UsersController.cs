using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Util;

namespace Login.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            return View("Index", new User());
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register", new User());
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            user.Password = Hashing.HashPassword(user.Password);
            //user.Role = ;
            myContext.Users.Add(user);
            myContext.SaveChanges();
            MailMessage mm = new MailMessage("muhammadrifqi0@gmail.com", user.Email);
            mm.Subject = "[Password] " + DateTime.Now.ToString("ddMMyyyyhhmmss");
            mm.Body = "Hi " + user.Username + "\nThis Is Your New Password : " + user.Password;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("muhammadrifqi0@gmail.com", "085376886737");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Password Has Been Sent.Check your email to login";
            return RedirectToAction("Index", "Users");
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var currentAccount = myContext.Users.FirstOrDefault(u => u.Username.Equals(user.Username));
            if (currentAccount != null)
            {
                if (Hashing.ValidatePassword(user.Password, currentAccount.Password))
                {
                    Session.Add("username", user.Username);
                    //return View("Welcome");
                    return RedirectToAction("Index", "Dashboard");
                }
                //else
                //{
                //    Session.Add("username", user.Username);
                //    return View("Welcome");
                //}
            }
            ViewBag.error = "Invalid";
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Remove("username");
            return RedirectToAction("Index", "Users");
        }
    }
}