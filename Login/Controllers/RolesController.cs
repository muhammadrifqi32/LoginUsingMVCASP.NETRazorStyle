using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Newtonsoft.Json;

namespace Login.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();
        object result = null;

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


        public ActionResult Details(int id)
        {
            var list = myContext.Roles.Find(id);
            return View(list);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(Role role)
        {
            try
            {
                // TODO: Add insert logic here
                myContext.Roles.Add(role);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = myContext.Roles.Find(id);
            return View(edit);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Role role)
        {
            try
            {
                // TODO: Add update logic here
                var editrole = myContext.Roles.Find(id);
                editrole.Name = role.Name;
                myContext.Entry(editrole).State = System.Data.Entity.EntityState.Modified;
                var result = myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var delete = myContext.Roles.Find(id);
            return View(delete);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Role role)
        {
            try
            {
                // TODO: Add delete logic here
                var deleterole = myContext.Roles.Find(id);
                myContext.Roles.Remove(deleterole);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult List()
        {
            List<Role> RoleList = new List<Role>();
            RoleList = myContext.Roles.ToList();

            return new JsonResult { Data = RoleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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