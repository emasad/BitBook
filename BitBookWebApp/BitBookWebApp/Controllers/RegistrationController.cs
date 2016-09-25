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
        

        //Add basic informations
        public ActionResult BasicInformation()
        {
            if (Session["email"] != null)
            {                
                //Email already exist
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                //check alread exist user
                if (db.BasicInfos.Any(x => x.UserId.Equals(user.Id)))
                {
                    //Update user Info.

                    return RedirectToAction("Home", "Registration");


                }
                else
                {
                    return View();
                    
                }

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //File and BasicInfo Upload
        public ActionResult BasicInfoUpload(IEnumerable<HttpPostedFileBase> files)
        {
            
            //Checked login
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                //check alread exist user
                if (db.BasicInfos.Any(x => x.UserId.Equals(user.Id)))
                {
                    //Update user Info.

                    return RedirectToAction("Home", "Registration");


                }
                //else not exist
                else
                {
                    if (ModelState.IsValid)
                    {
                        if (files != null)
                        {

                            string coverImg = null;
                            string profileImg = null;

                            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;



                            if (list[0] != null)
                            {
                                if (list[0].ContentLength > 0 )
                                {
                                    //Cover Photo

                                    coverImg = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(list[0].FileName);

                                    string physicalPath = System.IO.Path.Combine(Server.MapPath("~/Images/coverPic"), coverImg);

                                    // save image in folder
                                    list[0].SaveAs(physicalPath);

                                }
                            }

                            if (list[1] != null)
                            {

                                if (list[1].ContentLength > 0)
                                {
                                    //Profile Photo

                                    profileImg = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(list[1].FileName);

                                    string physicalPathCover = System.IO.Path.Combine(Server.MapPath("~/images/profilePic"), profileImg);

                                    // save image in folder
                                    list[1].SaveAs(physicalPathCover); ;
                                }
                            }
                            //User Id find
                            
                            //save new record in database
                            BasicInfo aBasicInfo = new BasicInfo();
                            aBasicInfo.UserId = user.Id;
                            aBasicInfo.CoverPicUrl = coverImg;
                            aBasicInfo.ProfilePicUrl = profileImg;
                            aBasicInfo.About = Request.Form["About"];
                            aBasicInfo.AreaOfInterest = Request.Form["AreaOfInterest"];
                            aBasicInfo.Location = Request.Form["Location"];
                            aBasicInfo.Education = Request.Form["Education"];
                            aBasicInfo.Experience = Request.Form["Experience"];
                            //Save in db
                            db.BasicInfos.Add(aBasicInfo);
                            db.SaveChanges();
                        }

                    }
                    //Update user Info
                }

                return RedirectToAction("Home", "Registration");

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }

            
        }


        //User Profile
        //User Profile
        public ActionResult UserProfile()
        {
            return View();
        }



        //

        //
    }
}