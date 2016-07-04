using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.SqlClient;
using System.Data.Entity;

using ProiectLicenta.DTO;



/*
 * In acest contoller avem logica de gestiune a profilului utilizatorului in aplicatie
 */
namespace ProiectLicenta.Controllers
{
    public class ProfileController : Controller
    {
        Models.ProiectLicenta pl = new Models.ProiectLicenta();
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            var userRole = from roleId in pl.Utilizatoris
                           where roleId.Nume_Utilizator == User.Identity.Name
                           join x in pl.Functies on roleId.Id_Functie equals x.Id
                           

                           select new
                           {
                               x.Denumire
                           };

            ViewBag.role = userRole.ToList();
            return View();
        }

        [Authorize]
        public ActionResult Overview()
        {
            var todayTask = from task in pl.Tasks
                            select new
                            {
                                task.Eveniment,
                                task.Stare_Task,
                                task.Descriere_Suplimentara
                            };

            ViewBag.todayTasks = todayTask;
            return View();
        }
       
        public ActionResult Mail()
        {
            return View();
        }

        public ActionResult MailSent()
        {
            return View("MailSent");
        }

        public ActionResult Account()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SendMail(EMailMessage msg)//aceasta metoda este asincrona
        {
            if (ModelState.IsValid)
            {
                string body = msg.MessageContent;
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(msg.To));
                message.From = new MailAddress("cosmin.popescu93@gmail.com");
                message.Subject = msg.Subject;
                message.Body = body;//pana la aceasta linie se pregateste continutul mesajului

                using (SmtpClient smtp = new SmtpClient())//se introduce  datele de conectare ale serverului de mail. in cazul acesta numele de utilizator si parola de la gmail
                {
                    NetworkCredential credentials = new NetworkCredential
                    {
                        UserName = "cosmin.popescu93@gmail.com",
                        Password = "cosminpop"//niciodata aceasta informatie sa nu se afiseze in plain text
                    };

                    smtp.Credentials = credentials;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);//punem in obiectul smtp propietatile necesare. pot diferii de la un providel de mail la altul

                    
                }
            }
            return RedirectToAction("MailSent");//dupa ce am trmimis mailul intoarce utilizatorul la actinea Index
        }

        public ActionResult UserAccount([DataSourceRequest] DataSourceRequest request, Users usr)//afisare detalii despre user
        {
            string userName = User.Identity.Name;//aflam numele utilizatorului curent. cel care s-a logat
            var userDetails = from user in pl.Utilizatoris
                             where (user.Nume_Utilizator == userName)
                             select new
                             {
                                 Id = user.Id,
                                 Nume_Utilizator = user.Nume_Utilizator,
                                 Email = user.Email
                             };
                             

                             

            DataSourceResult result = userDetails.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUserDetails(Users usr)//actualizare detalii. 
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    var user = pl.Utilizatoris.FirstOrDefault(u => u.Id == usr.Id);
                    user.Nume_Utilizator = usr.Nume_utilizator;
                    user.Email = usr.Email;

                    pl.SaveChanges();
                }
                catch (Exception ex)
                {

                    ViewBag.err = ex.InnerException;
                }
                finally
                {
                    pl.Dispose();
                }

            }

            if (!ModelState.IsValid)//debug porpouse
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }

            return RedirectToAction("Account"); 
        }

        [HttpPost]
        public ActionResult UpdateUserRole(Users usr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = pl.Utilizatoris.FirstOrDefault(u => u.Id == usr.Id);
                    //pl.Utilizatoris.Attach(role);
                    role.Id_Functie = usr.Id_Functie;
                    //var entry = pl.Entry(role);
                    //entry.Property(p => p.Id_Functie).IsModified = true;
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

            return RedirectToAction("ViewTeam", "Events");
        }
    }
}