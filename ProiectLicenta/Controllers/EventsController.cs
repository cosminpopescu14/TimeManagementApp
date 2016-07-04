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
        
        //[Authorize]
        public ActionResult AssignRole()
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
                    var evt = new Eveniment
                    {
                        Denumire = ev.Denumire,
                        Data_Start = ev.Data_Start,
                        Data_Sfarsit = ev.Data_Sfarsit,
                        Activ = false,
                        Id_MO = 2
                    };

                    pl.Eveniments.Add(evt);
                    pl.SaveChanges();

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
                         join ee in pl.Echipa_Eveniment on ev.Id_MO equals ee.Id_Utilizator
                         join u in pl.Utilizatoris on ee.Id_Utilizator equals u.Id
                         where u.Nume_Utilizator == User.Identity.Name
        
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

        public int UserLogged()
        {
            int id = (User.Identity.Name == "Cosmin Popescu") ? 1 : 2; //solutie temporara
            return id;      
        }

        public ActionResult GetTeam([DataSourceRequest] DataSourceRequest request)
        {
            int userIdLogged = UserLogged();
            var team = from eventId in pl.Echipa_Eveniment
                       join e in pl.Eveniments on eventId.Id_Eveniment equals e.Id
                       join u in pl.Utilizatoris on eventId.Id_Utilizator equals u.Id
                       where eventId.Id_Eveniment == userIdLogged
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

        public ActionResult GetTeamWithRoles([DataSourceRequest] DataSourceRequest request)
        {
            int userIdLogged = UserLogged();
            var team = from eventId in pl.Echipa_Eveniment
                       join e in pl.Eveniments on eventId.Id_Eveniment equals e.Id
                       join u in pl.Utilizatoris on eventId.Id_Utilizator equals u.Id
                       join r in pl.Functies on u.Id_Functie equals r.Id
                       where eventId.Id_Eveniment == userIdLogged
                       select new
                       {
                           Id_Utilizator = u.Id,
                           Denumire = e.Denumire,
                           Nume_Utilizator = u.Nume_Utilizator,
                           Email = u.Email,
                           Rol = r.Denumire
                       };

            DataSourceResult result = team.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    } 
}