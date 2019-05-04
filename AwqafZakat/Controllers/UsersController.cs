using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class UsersController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        // GET: Users
        public ActionResult Registration(int id = 0)
        {
            tblUser userModel = new tblUser();
            return View(userModel);
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Registration(tblUser objUsers)
        {
            try
            {
                if (db.tblUsers.Any(x => x.userName == objUsers.userName))
                {
                   TempData["ErrorMessage"] = "Username already exists";
                   return View("Registration", objUsers);
                }
                if (db.tblUsers.Any(x => x.email == objUsers.email))
                {
                   TempData["ErrorMessage"] = "Email already exists";
                   return View("Registration", objUsers);
                }
                db.tblUsers.Add(objUsers);
                db.SaveChanges();

                ModelState.Clear();
                // ViewBag.SuccessMessage = "Registration Successful";
                TempData["SuccessMessage"] = "Registration Successful";
                return View("Registration", new tblUser());

            }
            catch
            {
                return View();
            }
        }

        // GET: Users login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(tblUser objUsers)
        {
            //using (UserModels db = new UserModels())
            //{
                var userDetails = db.tblUsers.Where(x => x.userName == objUsers.userName && x.password == objUsers.password).FirstOrDefault();
                if (userDetails == null)
                {
                    objUsers.LoginErrorMessage = "Wrong username or password.";
                    return View("Login", objUsers);
                }
                else
                {
                    Session["userID"] = userDetails.userID;
                    Session["userName"] = userDetails.userName;
                    return RedirectToAction("Index", "Home");
                }
            //}
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Login", "Users");
        }
    }
}