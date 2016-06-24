using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;

using ProiectLicenta.DTO;
using ProiectLicenta.Models;
using System.Net;

namespace ProiectLicenta.Controllers
{
    public class TasksController : Controller
    {
        Models.ProiectLicenta pl = new Models.ProiectLicenta();
        // GET: Tasks
        [Authorize]
        public ActionResult Index()
        {
            var x = from task in pl.Tasks
                    join t in pl.MembruEchipaXTask on task.Id equals t.Id_Task
                    join u in pl.Utilizatoris on t.Id_Utilizator equals u.Id
                    select new
                    {
                        u.Nume_Utilizator,
                        task.Descriere_Suplimentara
                    };

            ViewBag.tasks = x;
            return View();
        }

        [Authorize]
        public ActionResult AddTask()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewTasks()
        {
            return View();
        }

        [Authorize]
        public ActionResult EditTask()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewByMember()
        {
           return View();
        }

        [Authorize]
        public ActionResult GetTasksType([DataSourceRequest] DataSourceRequest request)
        {
            var taskType = from type in pl.Tip_Task
                           select new
                           {
                               Id = type.Id,
                               Denumire = type.Denumire
                           };

            return Json(taskType, JsonRequestBehavior.AllowGet);             
        }

        [HttpPost]
        public ActionResult AddTasks([DataSourceRequest] DataSourceRequest request, Tasks task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tasks = new Task
                    {
                        Id_Tip_Task = task.Id_Tip_Task,
                        Id_Eveniment = task.Id_Eveniment,
                        Descriere_Suplimentara = task.Descriere_Suplimentara,
                        Termen = task.Termen,
                        Data_Creare_Task = task.Data_Creare_Task,
                        Data_Sfarsit_Task = task.Data_Sfarsit_Task,
                    };

                    IQueryable<int> taskId = from t in pl.Tasks
                                 select new
                                 {
                                    t.Id
                                 }.Id;

                    IQueryable<int> userId = from u in pl.Utilizatoris
                                 join ee in pl.Echipa_Eveniment on u.Id equals ee.Id_Utilizator
                                 select new
                                 {
                                     u.Id
                                 }.Id;

                    var x = new MembruEchipaXTask
                    {
                        Id_Task = Convert.ToInt32(taskId),
                        Id_Utilizator = Convert.ToInt32(userId)
                    };

                    pl.Tasks.Add(tasks);
                    pl.MembruEchipaXTask.Add(x);
                    pl.SaveChanges();
                }

                catch (Exception ex)//sare aici, nu executa insert. exceptie datorata unui nume gresit de tabel. entityt foloseste varianta la plural a numelui
                {
                    throw ex;
                }
            }

            else//daca modelul nu este valid, vedem ce problema a aparut
            {
                var message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Test([DataSourceRequest] DataSourceRequest request)
        {
            var user = from u in pl.Utilizatoris
                       join ee in pl.Echipa_Eveniment on u.Id equals ee.Id_Utilizator
                       select new
                       {
                           Id = u.Id,
                           Nume_Utilizator = u.Nume_Utilizator
                       };

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEvents([DataSourceRequest] DataSourceRequest request)
        {
            var events = from ev in pl.Eveniments
                         select new
                         {
                             Id = ev.Id,
                             Denumire = ev.Denumire,
                         };

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTasks([DataSourceRequest] DataSourceRequest request)
        {
            var tasks = from t in pl.Tasks
                        select new
                        {
                            t.Id,
                            t.Data_Creare_Task,
                            t.Data_Sfarsit_Task,
                            t.Descriere_Suplimentara,
                            t.Stare_Task
                        };

            DataSourceResult result = tasks.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTasksByTeamMember([DataSourceRequest] DataSourceRequest request)
        {
            var x = from task in pl.Tasks
                    join t in pl.MembruEchipaXTask on task.Id equals t.Id_Task
                    join u in pl.Utilizatoris on t.Id_Utilizator equals u.Id
                    where u.Nume_Utilizator == User.Identity.Name
                    select new
                    {
                        u.Nume_Utilizator,
                        task.Descriere_Suplimentara,
                        task.Data_Creare_Task,
                        task.Data_Sfarsit_Task
                    };

            DataSourceResult results = x.ToDataSourceResult(request);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, Tasks task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var taskEntity = pl.Tasks.FirstOrDefault(t => t.Id == task.Id);
                    taskEntity.Data_Creare_Task = task.Data_Creare_Task;
                    taskEntity.Data_Sfarsit_Task = task.Data_Sfarsit_Task;
                    taskEntity.Descriere_Suplimentara = task.Descriere_Suplimentara;
                    taskEntity.Stare_Task = task.Stare_Task;
                    pl.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }
            return RedirectToAction("EditTask");
        }
    }
}

//coded with love by Cosmin Popescu