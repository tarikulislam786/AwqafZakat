using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class FamilyDeletionsController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
       tblFamilyRegistration familyRegistrationModel = new tblFamilyRegistration();
        // GET: FamilyDeletions
        public ActionResult FamilyDel(int id = 0)
        {
            tblFamilyDeletion FamilyDeletionModel = new tblFamilyDeletion();
            // ViewBag.familyData = db.tblFamilyRegistrations.ToList();
            return View(FamilyDeletionModel);
        }

        public JsonResult GetSearchingData(string searchBy, string searchValue)
        {
            List<tblFamilyRegistration> FList = new List<tblFamilyRegistration>();
            db.Configuration.ProxyCreationEnabled = false;
            if (searchBy == "registrationNumber")
            {
                try
                {
                    int familyregistrationNumber = Convert.ToInt32(searchValue);
                   // db.Configuration.ProxyCreationEnabled = false;
                    FList = db.tblFamilyRegistrations.Where(x => x.familyRegNumber == familyregistrationNumber || searchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not a ID", searchValue);
                }
                
               // User ma = db.user.First(x => x.u_id == id);
               // return Json(ma, JsonRequestBehavior.AllowGet);

                return Json(FList, JsonRequestBehavior.AllowGet);
            }
            else
            {
               // db.Configuration.ProxyCreationEnabled = false;
                FList = db.tblFamilyRegistrations.Where(x => x.civilNumber.Contains(searchValue) || searchValue == null).ToList();
                return Json(FList, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: FamilyDeletions
        [HttpPost]
        public ActionResult FamilyDel(tblFamilyDeletion objFamilyDeletions)
        {
            
                if (objFamilyDeletions.hardcopyImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objFamilyDeletions.hardcopyImageUpload.FileName);
                    string extension = Path.GetExtension(objFamilyDeletions.hardcopyImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    objFamilyDeletions.hardcopyAttach = "~/AppFiles/Images/" + fileName;
                    objFamilyDeletions.hardcopyImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }

                db.tblFamilyDeletions.Add(objFamilyDeletions);
                db.SaveChanges();
            
            ModelState.Clear();
            // ViewBag.SuccessMessage = "Registration Successful";
            TempData["SuccessMessage"] = "Deletion Successful";
            return View("FamilyDel", new tblFamilyDeletion());

        }
    }
}