using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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
        //[Authorize]
        //public ActionResult TaskAdded()
        //{
        //    return View();
        //}

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
                        pl.Tasks.Add(tasks);
                        pl.SaveChanges();
                }

                catch (Exception ex)
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