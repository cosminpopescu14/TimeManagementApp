using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data.SqlClient;
using System.Web.Security;


using ProiectLicenta.Models;
using ProiectLicenta.DTO;
using ProiectLicenta.Utilities;

namespace ProiectLicenta.Controllers
{
    public class UsersController : Controller
    {
        Models.ProiectLicenta pl = new Models.ProiectLicenta();
        
        // GET: Users
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateUsers()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LOGIN()
        {
            return View();
        }

        
        public JsonResult SelectRoles([DataSourceRequest] DataSourceRequest request)//selectam funtiile din tabelul Functii
        {
            var roles = from r in pl.Functies
                        select new
                        {
                            Denumire = r.Denumire,
                            Id = r.Id
                        };
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUsers(Users usr)//adaugam utilizatori
        {
            if (ModelState.IsValid)
            {
                string CallSP = "AddUsers @Nume_Utilizator, @Email, @Parola";
                SqlParameter NumeUtilizator = new SqlParameter("Nume_utilizator", usr.Nume_utilizator);
                SqlParameter Email = new SqlParameter("Email", usr.Email);
                SqlParameter Parola = new SqlParameter("Parola", HashPassword.ComputeSHA1(usr.Parola));
                //SqlParameter Id_Functie = new SqlParameter("Id_Functie", usr.Id_Functie);

                object[] parameters = new object[] { NumeUtilizator, Email, Parola };
                var result = pl.Database.SqlQuery<Users>(CallSP, parameters);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index", "Users");    
        }

        [HttpPost]
        public ActionResult LogIn(Users usr)//metoda de logare.  se va baza pe functia ComputeSHA1(parola) si o functie de comparare pt hash 
        {
            if (ModelState.IsValid)
            {
                var dateLoagare = pl.Utilizatoris //selectare date de logare din baza de date
                .Where(u => u.Nume_Utilizator == usr.Nume_utilizator)
                .Select(u => new { u.Nume_Utilizator, u.Parola });

                try
                {
                    string usrName = dateLoagare.FirstOrDefault().Nume_Utilizator;//extragere numele utilizator si parola corecte
                    string password = dateLoagare.FirstOrDefault().Parola;

                    string hashedPasswordFromUser = HashPassword.ComputeSHA1(usr.Parola);
                    bool pass = CompareHashes.ComparePasswords(password, hashedPasswordFromUser);

                    if (pass == true && usr.Nume_utilizator == usrName) //verificam ca ceea ce a introdus user-ul este corect
                    {
                        FormsAuthentication.SetAuthCookie(usr.Nume_utilizator, false);//cookie
                        return RedirectToAction("Index", "Profile");//il ducem catre o alta actiune din alt controller
                    }

                    else
                    {
                        ModelState.AddModelError("", "Date de logare incorecte !\nIncercati din nou !");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.loginError = ex.Message + "Numele de utilizator nu exista" + ex.StackTrace;  
                }
                
            }
            return LOGIN();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Users");
        }

        public ActionResult ForgotCredentials(Users usr)//utilizatorul isi poate recupera n umele du utilizator sau parola
        {
            return null;
        }
    }
}