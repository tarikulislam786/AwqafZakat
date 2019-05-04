using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class FamilyTransfersController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        tblFamilyRegistration familyRegistrationModel = new tblFamilyRegistration();
        // GET: FamilyTransfers
        public ActionResult Transfer(int id = 0)
        {
            tblFamilyTransfer FamilyTransferModel = new tblFamilyTransfer();
            return View(FamilyTransferModel);
        }

        // search by registration id or civil number in ajax
        /* public JsonResult GetSearchingData(string searchBy, string searchValue)
         {
             List<tblFamilyRegistration> FList = new List<tblFamilyRegistration>();
             if (searchBy == "registrationNumber")
             {
                 try
                 {
                     int familyregistrationNumber = Convert.ToInt16(searchValue);
                     FList = db.tblFamilyRegistrations.Where(x => x.familyRegNumber == familyregistrationNumber || searchValue == null).ToList();
                 }
                 catch (FormatException)
                 {
                     Console.WriteLine("{0} is not a ID", searchValue);
                 }
                 return Json(FList, JsonRequestBehavior.AllowGet);
             }
             else
             {
                 FList = db.tblFamilyRegistrations.Where(x => x.civilNumber.Contains(searchValue) || searchValue == null).ToList();
                 return Json(FList, JsonRequestBehavior.AllowGet);
             }
         }*/

        // POST: FamilyTransfers
        [HttpPost]
        public ActionResult Transfer(tblFamilyTransfer objFamilyTransfers)
        {
            
                if (objFamilyTransfers.hardcopyImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objFamilyTransfers.hardcopyImageUpload.FileName);
                    string extension = Path.GetExtension(objFamilyTransfers.hardcopyImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    objFamilyTransfers.hardcopyAttach = "~/AppFiles/Images/" + fileName;
                    objFamilyTransfers.hardcopyImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }

                if (objFamilyTransfers.electricityImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objFamilyTransfers.electricityImageUpload.FileName);
                    string extension = Path.GetExtension(objFamilyTransfers.electricityImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    objFamilyTransfers.electricityAttach = "~/AppFiles/Images/" + fileName;
                    objFamilyTransfers.electricityImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }

                db.tblFamilyTransfers.Add(objFamilyTransfers);
                db.SaveChanges();
            
            ModelState.Clear();
            // ViewBag.SuccessMessage = "Registration Successful";
            TempData["SuccessMessage"] = "Transfer Successful";
            return View("Transfer", new tblFamilyTransfer());
        }
    }
}