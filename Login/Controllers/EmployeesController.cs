using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.ViewModel;
using Newtonsoft.Json;

namespace Login.Controllers
{
    public class EmployeesController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();
        object result = null;
        // GET: Employees
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                return View(List());
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }
        public JsonResult List()
        {
            List<User> UserList = new List<User>();
            UserList = myContext.Users.Include("Role").ToList();

            return new JsonResult { Data = UserList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertOrUpdate(UserVM userVM)
        {
            var myContent = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (userVM.Id == 0)
            {
                var push = new User(userVM);
                push.Role = myContext.Roles.Where(r => r.id == userVM.Role).FirstOrDefault();
                myContext.Users.Add(push);
                result = myContext.SaveChanges();
            }
            else if (userVM.Id != 0)
            {
                var edituser = myContext.Users.Find(userVM.Id);
                edituser.Email = userVM.Email;
                edituser.Username = userVM.Username;
                edituser.Role = myContext.Roles.Where(r => r.id == userVM.Role).FirstOrDefault();
                myContext.Entry(edituser).State = System.Data.Entity.EntityState.Modified;
                result = myContext.SaveChanges();
            }
            return Json(result);
        }
        public JsonResult GetById(int id)
        {
            var user = myContext.Users.Find(id);
            return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Delete(int id)
        {
            var delete = myContext.Users.Find(id);
            myContext.Users.Remove(delete);
            result = myContext.SaveChanges();
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
