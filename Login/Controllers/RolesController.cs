using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Login.Models;
using Login.ViewModel;
using Newtonsoft.Json;

namespace Login.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();
        object result = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        // GET: Dashboard
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
            var result = await con.QueryAsync<RoleVM>("EXEC SP_GetRoleList");
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

            //List<Role> RoleList = new List<Role>();
            //RoleList = myContext.Roles.ToList();

            //return new JsonResult { Data = RoleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult InsertOrUpdate(Role role)
        {
            var myContent = JsonConvert.SerializeObject(role);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (role.id == 0)
            {
                myContext.Roles.Add(role);
                result = myContext.SaveChanges();
            }
            else if (role.id != 0)
            {
                var editrole = myContext.Roles.Find(role.id);
                editrole.Name = role.Name;
                myContext.Entry(editrole).State = System.Data.Entity.EntityState.Modified;
                result = myContext.SaveChanges();
            }
            return Json(result);
        }
        public JsonResult GetById(int id)
        {
            var role = myContext.Roles.Find(id);
            return new JsonResult { Data = role, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult DeleteRole(int id)
        {
            var delete = myContext.Roles.Find(id);
            myContext.Roles.Remove(delete);
            result = myContext.SaveChanges();
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult LoadRole()
        {
            List<Role> RoleList = new List<Role>();
            RoleList = myContext.Roles.ToList();

            return new JsonResult { Data = RoleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}