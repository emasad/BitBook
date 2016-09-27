using System;
using System.Collections.Generic;
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
        public ActionResult ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePass changePass)
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
                        var pass = changePass.NewPassword;

                        Response.Write("<script> alert('Password changed')</script>");
                    }

                }
            }

            return View();
        }
    }
}