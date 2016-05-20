using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data;
using System.Data.SqlClient;

using ProiectLicenta.DTO;
using System.Net;
using ProiectLicenta.Models;
using ProiectLicenta.Utilities;

namespace ProiectLicenta.Controllers
{
    public class EventsController : Controller
    {
        Models.ProiectLicenta pl = new Models.ProiectLicenta();

        // GET: Events
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult CreateEvent()
        {
            return View();
        }

        [Authorize]
        public ActionResult Update()
        {
            return View();
        }

        [Authorize]
        public ActionResult AssignTeam()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewTeam()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult AddEvents([DataSourceRequest] DataSourceRequest request, Events ev)//adaugare in tabela eveniment
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string CallSP = "AdaugareEveniment @Denumire, @Data_Start, @Data_Sfarsit, @Activ, @Id_MO";
                    SqlParameter Denumire = new SqlParameter("Denumire", ev.Denumire);
                    SqlParameter DataStart = new SqlParameter("Data_Start", ev.Data_Start);
                    SqlParameter DataSfarsit = new SqlParameter("Data_Sfarsit", ev.Data_Sfarsit);
                    SqlParameter Activ = new SqlParameter("Activ", ev.Activ);
                    SqlParameter Id_MO = new SqlParameter("Id_MO", 1);//id-ul mo-ului este hardcodat. NU ESTE CORECT ACEST LUCRU. VA TREBUI MODIFICAT

                    object[] parameters = new object[] { Denumire, DataStart, DataSfarsit, Activ, Id_MO };
                    var result = pl.Database.SqlQuery<Events>(CallSP, parameters);

                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {

                    ViewData["error"] = ex.Message;
                }

                finally
                {
                    pl.Dispose();
                }
 
            }

            else//daca modelul nu este vaid, vedem ce problema a aparut
            {
                var message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateEvent([DataSourceRequest] DataSourceRequest request, Events ev)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var evt = pl.Eveniments.FirstOrDefault(e => e.Id == ev.Id);
                    evt.Data_Start = ev.Data_Start;
                    evt.Data_Sfarsit = ev.Data_Sfarsit;
                    evt.Activ = false;
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

            return RedirectToAction("Update");
        }

        public ActionResult GetEvents([DataSourceRequest] DataSourceRequest request)
        {
            var events = from ev in pl.Eveniments
                         select new
                         {
                             Id = ev.Id,
                             Denumire = ev.Denumire,
                             Data_Start = ev.Data_Start,
                             Data_Sfarsit = ev.Data_Sfarsit
                         };

            DataSourceResult result = events.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsers([DataSourceRequest] DataSourceRequest request)
        {
                var users = from u in pl.Utilizatoris
                            select new
                            {
                                Nume_Utilizator = u.Nume_Utilizator,
                                Id = u.Id
                            };

                return Json(users, JsonRequestBehavior.AllowGet);  
        }
           
        [HttpPost]
        public ActionResult CreateTeam(EventXTeam et)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    for (int i = 0; i < et.Id_Utilizator.Length; i++)
                    {
                        var Team = new Echipa_Eveniment
                        {
                            Id_Eveniment = et.Id_Eveniment,
                            Id_Utilizator = et.Id_Utilizator[i]
                        };
                        pl.Echipa_Eveniment.Add(Team);
                    }

                    pl.SaveChanges();
                    /*Mail.SendMail("cosminel93@yahoo.com");
                    Mail.SendMail("cosmin.popescu93@gmail.com");*/
                }
                catch (Exception ex)
                {
                    throw ex;
                } 
            }

            else//daca modelul nu este vaid, vedem ce problema a aparut
            {
                var message = string.Join(" | ", ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }

            return RedirectToAction("AssignTeam");
        }

        public ActionResult GetTeam([DataSourceRequest] DataSourceRequest request)
        {
            var team = from eventId in pl.Echipa_Eveniment
                           //from userId in pl.Echipa_Eveniment
                       join e in pl.Eveniments on eventId.Id_Eveniment equals e.Id
                       join u in pl.Utilizatoris on eventId.Id_Utilizator equals u.Id
                       select new 
                       {
                           Id_Utilizator = u.Id,
                           Denumire = e.Denumire,
                           Nume_Utilizator = u.Nume_Utilizator,
                           Email = u.Email
                       };

            DataSourceResult result = team.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    } 
}