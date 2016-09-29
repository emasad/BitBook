using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            if (Session["email"] != null)
            {
                
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                var userInfo = db.BasicInfos.Where(x => x.UserId.Equals(user.Id)).FirstOrDefault();
                ViewBag.UserInfo = userInfo;
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //specific user profile 
        public ActionResult Profile(int id)
        {
            if (Session["email"] != null)
            {
                //check that user id is exist
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var userInfo = db.Users.FirstOrDefault(x => x.Email.Equals(userEmail));
                var user = db.BasicInfos.FirstOrDefault(x => x.UserId.Equals(id));

                if (user != null && userInfo.Id!=id)
                {
                    //if friend


                    //else not friend
                    ViewBag.UserInfo = user;

                }

                else
                {
                    return RedirectToAction("UserProfile", "Registration");
                    
                }


                //BitBookContext db = new BitBookContext();

                //string userEmail = "";
                //userEmail = Session["email"].ToString();

                //var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                //var userInfo = db.BasicInfos.Where(x => x.UserId.Equals(user.Id)).FirstOrDefault();
                //ViewBag.UserInfo = userInfo;
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //Not exists user view
        public ActionResult NotExist()
        {
            return View();

        }
        
        //Search action
        public ActionResult SearchUser(string searchTerm)
        {
            
             if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();
                List<User> users;
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    users = null;
                }
                else
                {
                    users = db.Users.Where(x => x.FirstName.StartsWith(searchTerm)).ToList();

                }
                
                return View(users);


            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }


        //Send Friend Request
        
        public ActionResult SendRequest(int friendId)
        {

            if (Session["email"] != null)
            {
                
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();
                var userFriend = db.UserFriends.Where(p => p.UserId == user.Id
                                              && p.FriendId == friendId).FirstOrDefault();
                //if own user

                if (user.Id==friendId || userFriend!=null)
                {
                    return RedirectToAction("UserProfile", "Registration" );


                }
                else
                {
                    UserFriend aUserFriend = new UserFriend();
                    aUserFriend.UserId = user.Id;
                    aUserFriend.FriendId = friendId;
                    aUserFriend.Friendstatus = 1;

                    db.UserFriends.Add(aUserFriend);
                    db.SaveChanges();

                    aUserFriend.UserId =friendId;
                    aUserFriend.FriendId = user.Id;
                    aUserFriend.Friendstatus = 3;

                    db.UserFriends.Add(aUserFriend);
                    db.SaveChanges();
                    return View();
                    
                }
                
            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }


        //Accept friend request

        public ActionResult AcceptRequest(int friendId)
        {
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();
                //1stupdate
                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                var userFriend = db.UserFriends.Where(p => p.UserId == user.Id
                                              && p.FriendId == friendId).FirstOrDefault();

                if (userFriend==null)
                {

                    return RedirectToAction("Home", "Registration");

                }
                else
                {

                    userFriend.Friendstatus = 2;
                    db.UserFriends.Attach(userFriend);
                    db.Entry(userFriend).Property(x => x.Friendstatus).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;

                    db.SaveChanges();
                    //second update

                    var userFriend2 = db.UserFriends.Where(p => p.UserId == friendId
                                                  && p.FriendId == user.Id).FirstOrDefault();


                    userFriend2.Friendstatus = 2;
                    db.UserFriends.Attach(userFriend2);
                    db.Entry(userFriend2).Property(x => x.Friendstatus).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;

                    db.SaveChanges();
                    return View();
                }



            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //Undfriend or cancel
        public ActionResult Unfriend(int friendId)
        {
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();
                //1stupdate
                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();

                var userFriend = db.UserFriends.Where(p => p.UserId == user.Id
                                              && p.FriendId == friendId).FirstOrDefault();


                if (userFriend==null)
                {
                    return RedirectToAction("Home", "Registration");

                }
                else
                {
                    //userFriend.Friendstatus = 2;
                    //db.UserFriends.Attach(userFriend);
                    //db.Entry(userFriend).Property(x => x.Friendstatus).IsModified = true;
                    //db.Configuration.ValidateOnSaveEnabled = false;
                    db.UserFriends.Remove(userFriend);
                    db.SaveChanges();
                    //second update

                    var userFriend2 = db.UserFriends.Where(p => p.UserId == friendId
                                                  && p.FriendId == user.Id).FirstOrDefault();


                    //userFriend2.Friendstatus = 2;
                    //db.UserFriends.Attach(userFriend2);
                    //db.Entry(userFriend2).Property(x => x.Friendstatus).IsModified = true;
                    //db.Configuration.ValidateOnSaveEnabled = false;
                    db.UserFriends.Remove(userFriend2);
                    db.SaveChanges();
                    db.SaveChanges();
                    return View();
                }
                

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //Post a post
        public ActionResult PostContent(HttpPostedFileBase file)
        {
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();
                string postImage = null;
                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();


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

                                string physicalPath = System.IO.Path.Combine(Server.MapPath("~/Images/postImages"), postImage);

                                // save image in folder
                                file.SaveAs(physicalPath);

                                
                            }

                            UserPost aUserPost = new UserPost();

                            aUserPost.PostText = Request.Form["PostText"];
                            aUserPost.UserId = user.Id;
                            aUserPost.ImageUrl = postImage;
                            aUserPost.CurrretTime = DateTime.Now;
                            db.UserPosts.Add(aUserPost);
                            db.SaveChanges();
                            return RedirectToAction("Home", "Registration");

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


        //Remove Post
        public ActionResult RemovePost(int id)
        {
            if (Session["email"] != null)
            {

                BitBookContext db = new BitBookContext();
                string userEmail = "";
                userEmail = Session["email"].ToString();
                //1stupdate
                var user = db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();
                var postState = db.UserPosts.Where(p => p.Id == id && p.UserId == user.Id).FirstOrDefault();
                db.UserPosts.Remove(postState);
                db.SaveChanges();
                return RedirectToAction("Home", "Registration");
            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //like a post

        public ActionResult LikeUserPost(int id)
        {
            if (Session["email"] != null)
            {

                using (BitBookContext db = new BitBookContext())
                {
                    string userEmail = "";
                    userEmail = Session["email"].ToString();

                    var user = db.Users.FirstOrDefault(x => x.Email.Equals(userEmail));
                    var likeStatus = db.LikePosts.FirstOrDefault(a => a.UserId.Equals(user.Id) && a.PostId.Equals(id));
                    if (likeStatus == null)
                    {


                        LikePost aLikePost= new LikePost();
                        aLikePost.PostId = id;
                        aLikePost.UserId = user.Id;
                        aLikePost.CurrretTime=DateTime.Now;

                        db.LikePosts.Add(aLikePost);
                        db.SaveChanges();
                       
                        return RedirectToAction("Home", "Registration");
                    }

                    else
                    {
                        Response.Write("<script> alert('You Already Like It')</script>");
                    }
                }
                return RedirectToAction("Home", "Registration");

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
            
        }
        //
        public ActionResult Comment(int id, string commentText)
        {
            if (Session["email"] != null)
            {

                using (BitBookContext db = new BitBookContext())
                {
                    string userEmail = "";
                    userEmail = Session["email"].ToString();

                    var user = db.Users.FirstOrDefault(x => x.Email.Equals(userEmail));
                    


                        UserComment aUserComment = new UserComment();
                        aUserComment.PostId = id;
                        aUserComment.UserId = user.Id;
                        aUserComment.PostText = commentText;

                        db.UserComments.Add(aUserComment);
                        db.SaveChanges();

                        return RedirectToAction("Home", "Registration");
                    
                }

            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }

        //number of friend request


        //number of friend
        public ActionResult NumberFriend()
        {
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var user = db.Users.FirstOrDefault(x => x.Email.Equals(userEmail));

                var userFriend = db.UserFriends.Where(p => p.UserId == user.Id && p.Friendstatus == 2).ToList();
                ViewBag.Number = userFriend.Count();
                ViewBag.User = userFriend.ToList();
                return View();



            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }


        //Frind Request
        public ActionResult RequestF()
        {
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();

                string userEmail = "";
                userEmail = Session["email"].ToString();

                var user = db.Users.FirstOrDefault(x => x.Email.Equals(userEmail));

                var userFriend = db.UserFriends.Where(p => p.UserId == user.Id && p.Friendstatus == 3).ToList();
                ViewBag.Number = userFriend.Count();
                ViewBag.User = userFriend.ToList();
                return View();



            }
            else
            {
                return RedirectToAction("Login", "Registration");

            }
        }


        //
    }
}