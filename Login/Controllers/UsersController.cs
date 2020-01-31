using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Util;
using Login.ViewModel;

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
        public ActionResult Register(UserVM userVM)
        {
            var user = myContext.Users.Where(u => u.Email == userVM.Email).SingleOrDefault();
            if (user == null)
            {
                var push = new User(userVM);
                var roleid = myContext.Roles.FirstOrDefault(r => r.id == 2016);
                push.Password = Hashing.HashPassword(userVM.Password);
                push.Role = roleid;
                myContext.Users.Add(push);
                myContext.SaveChanges();
                //MailMessage mm = new MailMessage("muhammadrifqi0@gmail.com", userVM.Email);
                //mm.Subject = "[Password] " + DateTime.Now.ToString("ddMMyyyyhhmmss");
                //mm.Body = "Hi " + userVM.Username + "\nThis Is Your New Password : " + userVM.Password;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.EnableSsl = true;

                //NetworkCredential nc = new NetworkCredential("muhammadrifqi0@gmail.com", "085376886737");
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = nc;
                //smtp.Send(mm);
                //ViewBag.Message = "Password Has Been Sent.Check your email to login";
                return RedirectToAction("Index", "Users");
            }
            ViewBag.error = "Email Has Been Used";
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var currentAccount = myContext.Users.FirstOrDefault(u => u.Username.Equals(user.Username));
            if (currentAccount != null)
            {
                if (Hashing.ValidatePassword(user.Password, currentAccount.Password))
                {
                    Session["id"] = user.id;
                    Session.Add("username", user.Username);
                    //return View("Welcome");
                    return RedirectToAction("Index", "Roles");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Remove("id");
            Session.Remove("username");
            return RedirectToAction("Index", "Users");
        }
    }
}