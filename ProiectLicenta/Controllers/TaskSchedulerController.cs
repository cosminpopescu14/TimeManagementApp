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
    public class TaskSchedulerController : Controller
    {
        Models.ProiectLicenta pl = new Models.ProiectLicenta();

        // GET: TaskScheduler
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTasksToSch([DataSourceRequest] DataSourceRequest request/*, TaskScheduler ts*/)
        {
            if (ModelState.IsValid)
            {
                    IQueryable<TaskScheduler> task = pl.Tasks.ToList().Select(t => new TaskScheduler()
                    {
                        TaskID = t.Id,
                        Title = t.Descriere_Suplimentara,
                        Start = DateTime.SpecifyKind((DateTime)t.Data_Creare_Task, DateTimeKind.Local),
                        End = DateTime.SpecifyKind((DateTime)t.Data_Sfarsit_Task, DateTimeKind.Local),
                        Description = t.Descriere_Suplimentara,
                        //IsAllDay = true,
                        //RecurrenceRule = "",
                        //RecurrenceException = "",
                        //RecurrenceID = "",
                        OwnerID = 1
                    })
                    
                    .AsQueryable();

                /*var x = from y in pl.Tasks
                        select new
                        {
                            TaskID = y.Id,
                            Title = "todo",
                            Start = (DateTime)y.Data_Creare_Task,
                            End = (DateTime)y.Data_Sfarsit_Task,
                            Description = y.Descriere_Suplimentara,
                            OwnerID = 1
                        };*/
                DataSourceResult result = task.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ShowTasksByRole()
        {
            return null;
        }
    }
}