using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitBookWebApp.BitBook.Core.BLL;
using BitBookWebApp.Context;
using BitBookWebApp.Models;

namespace BitBookWebApp.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult ChangePassword()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePass changePass)
        {
            if (Session["Email"] != null)
            {
                if (ModelState.IsValid)
                {
                    using (BitBookContext aBitBookContext = new BitBookContext())
                    {
                        var log = aBitBookContext.Users.FirstOrDefault(a => a.Password.Equals(changePass.NewPassword));
                        if (log != null)
                        {
                            Response.Write("<script> alert('You entered same password')</script>");
                        }
                        else
                        {
                            string userEmail = null;
                            userEmail = Session["Email"].ToString();
                            var usr = aBitBookContext.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                            usr.Password = changePass.NewPassword;
                            aBitBookContext.Users.Attach(usr);
                            aBitBookContext.Entry(usr).Property(x => x.Password).IsModified = true;
                            aBitBookContext.Configuration.ValidateOnSaveEnabled = false;
                            
                            aBitBookContext.SaveChanges();

                            Response.Write("<script> alert('Password changed')</script>");
                        }

                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Registration");
            }

            return View();
        }
    }
    }