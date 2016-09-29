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
                            //return RedirectToAction("Home", "Registration");
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

        public ActionResult ChangeCover()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeCover(HttpPostedFileBase file)
        {
            if (Session["email"] != null)
            {
                BitBookContext aBitBookContext = new BitBookContext();
                string postImage = null;
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (file != null)
                        {
                            if (file.ContentLength > 0)
                            {
                                //Cover Photo

                                postImage = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                                string physicalPath = System.IO.Path.Combine(Server.MapPath("~/Images/coverPic"), postImage);

                                // save image in folder
                                file.SaveAs(physicalPath);

                            }

                            string userEmail = null;
                            userEmail = Session["Email"].ToString();
                            var user = aBitBookContext.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                            var usr = aBitBookContext.BasicInfos.Where(x => x.UserId.Equals(user.Id)).FirstOrDefault();

                            usr.CoverPicUrl = postImage;
                            aBitBookContext.BasicInfos.Attach(usr);
                            aBitBookContext.Entry(usr).Property(x => x.CoverPicUrl).IsModified = true;
                            aBitBookContext.Configuration.ValidateOnSaveEnabled = false;

                            aBitBookContext.SaveChanges();
                            return RedirectToAction("UserProfile", "Registration");
                        }

                    }
                    return RedirectToAction("Home", "Registration");


                }
                return RedirectToAction("Home", "Registration");
            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        public ActionResult ChangeProfilePhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeProfilePhoto(HttpPostedFileBase file)
        {
            if (Session["email"] != null)
            {
                BitBookContext aBitBookContext = new BitBookContext();
                string postImage = null;
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (file != null)
                        {
                            if (file.ContentLength > 0)
                            {
                                //Cover Photo

                                postImage = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                                string physicalPath = System.IO.Path.Combine(Server.MapPath("~/Images/profilePic"), postImage);

                                // save image in folder
                                file.SaveAs(physicalPath);

                            }

                            string userEmail = null;
                            userEmail = Session["Email"].ToString();
                            var user = aBitBookContext.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                            var usr = aBitBookContext.BasicInfos.Where(x => x.UserId.Equals(user.Id)).FirstOrDefault();

                            usr.ProfilePicUrl = postImage;
                            aBitBookContext.BasicInfos.Attach(usr);
                            aBitBookContext.Entry(usr).Property(x => x.ProfilePicUrl).IsModified = true;
                            aBitBookContext.Configuration.ValidateOnSaveEnabled = false;

                            aBitBookContext.SaveChanges();
                            return RedirectToAction("UserProfile", "Registration");
                        }

                    }
                    return RedirectToAction("Home", "Registration");


                }
                return RedirectToAction("Home", "Registration");
            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }
    }
}