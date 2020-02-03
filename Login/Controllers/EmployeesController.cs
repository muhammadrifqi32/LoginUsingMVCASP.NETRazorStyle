using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Login.Models;
using Login.Util;
using Login.ViewModel;
using Newtonsoft.Json;

namespace Login.Controllers
{
    public class EmployeesController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();
        object result = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

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
        public async Task<ActionResult> List()
        {
            //List<User> UserList = new List<User>();
            //UserList = myContext.Users.Include("Role").ToList();

            //return new JsonResult { Data = UserList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var result = await con.QueryAsync<UserVM>("EXEC SP_GetUserList");
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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
                push.Role = myContext.Roles.Where(r => r.id == userVM.Role_id).FirstOrDefault();
                push.Password = Hashing.HashPassword(userVM.Username);
                myContext.Users.Add(push);
                result = myContext.SaveChanges();
            }
            else if (userVM.Id != 0)
            {
                var edituser = myContext.Users.Find(userVM.Id);
                edituser.Email = userVM.Email;
                edituser.Username = userVM.Username;
                edituser.Role = myContext.Roles.Where(r => r.id == userVM.Role_id).FirstOrDefault();
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
