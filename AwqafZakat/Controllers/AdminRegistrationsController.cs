using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class AdminRegistrationsController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        // GET: AdminRegistrations

        public ActionResult AdminReg(int id = 0)
        {
            tblAdminRegistration AdminRegistrationModel = new tblAdminRegistration();

            return View(AdminRegistrationModel);
        }

        // POST: AdminRegistrations
        [HttpPost]
        public ActionResult AdminReg(tblAdminRegistration objadminRegistrations)
        {

            // unique checking civil number
            if (db.tblAdminRegistrations.Any(x => x.civilNumber == objadminRegistrations.civilNumber))
            {
                TempData["ErrorMessage"] = "Civil Number already exists";
                return View("AdminReg", objadminRegistrations);
            }
            
                if (objadminRegistrations.adminImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objadminRegistrations.adminImageUpload.FileName);
                    string extension = Path.GetExtension(objadminRegistrations.adminImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    objadminRegistrations.adminIdCard = "~/AppFiles/Images/" + fileName;
                    objadminRegistrations.adminImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                if (objadminRegistrations.assistantImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objadminRegistrations.assistantImageUpload.FileName);
                    string extension = Path.GetExtension(objadminRegistrations.assistantImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    objadminRegistrations.assistantIdCard = "~/AppFiles/Images/" + fileName;
                    objadminRegistrations.assistantImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                if (objadminRegistrations.hardcopyImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objadminRegistrations.hardcopyImageUpload.FileName);
                    string extension = Path.GetExtension(objadminRegistrations.hardcopyImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    objadminRegistrations.hardcopyAttach = "~/AppFiles/Images/" + fileName;
                    objadminRegistrations.hardcopyImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                objadminRegistrations.administratorNumber = Convert.ToInt32("2018") + Convert.ToInt32(objadminRegistrations.civilNumber.ToString());
                db.tblAdminRegistrations.Add(objadminRegistrations);
                db.SaveChanges();
            
            ModelState.Clear();
            // ViewBag.SuccessMessage = "Registration Successful";
            TempData["SuccessMessage"] = "Registration Successful";
            return View("AdminReg", new tblAdminRegistration());
        }
    }
}