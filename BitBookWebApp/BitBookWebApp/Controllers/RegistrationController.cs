using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookWebApp.BitBook.Core.BLL;
using BitBookWebApp.Context;
using BitBookWebApp.Models;

namespace BitBookWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        RegistrationManager aRegistrationManager= new RegistrationManager();

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }


        //Post for Registration
        [HttpPost]
        public ActionResult Index(User aUser)
        {
            if (ModelState.IsValid)
            {
                bool isExists = aRegistrationManager.IsEmailAleadyExist(aUser.Email);
                if (isExists)
                {
                    ViewBag.StatusMessage = "Email Already Exists.";

                }
                else
                {

                    bool isSaved = aRegistrationManager.SaveUserRegistraion(aUser);

                    if (isSaved)
                    {
                        ViewBag.StatusMessage = "Save is successed.";
                    }
                    else
                    {
                        ViewBag.StatusMessage = "Saved Fail";
                    }
                }


                return View();
            }


            return View();
        }



        //Remote, check Email Existance
        public ActionResult IsEmailExist(string Email)
        {
            using (BitBookContext db = new BitBookContext())
            {
                try
                {
                    var email = db.Users.Single(m => m.Email == Email);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {

                    return Json(true, JsonRequestBehavior.AllowGet);

                }
            }
        }


        //Get: Login
        public ActionResult Login()
        {
            return View();
        }


        //Post: Login
        [HttpPost]
        public ActionResult Login(LogViewModel lg)
        {
            if (ModelState.IsValid)
            {
                using (BitBookContext aBitBookContext = new BitBookContext())
                {
                    var log = aBitBookContext.Users.Where(a => a.Email.Equals(lg.Email) && a.Password.Equals(lg.Password)).FirstOrDefault();
                    if (log != null)
                    {
                        Session["email"] = log.Email;
                        return RedirectToAction("Home", "Registration");
                    }

                    else
                    {
                        Response.Write("<script> alert('Invalid Email Or Password')</script>");
                    }
                }
            }
            return View();
        }


        //Get: Home
        public ActionResult Home()
        {
            if (Session["email"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }

        }


        //Get: LogOut
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Registration");
        }
        

        //
    }
}