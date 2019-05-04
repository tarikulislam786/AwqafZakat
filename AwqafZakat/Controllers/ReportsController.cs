using AwqafZakat.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class ReportsController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        // GET: Reports
        public ActionResult familyRegReport()
        {
            tblFamilyRegistration FamilyRegistrationModel = new tblFamilyRegistration();
            var f = db.tblFamilyRegistrations.ToList();
            return View(f);
        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ReportFamilyRegistration.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("familyRegReport");
            }
            List<tblFamilyRegistration> fr = new List<tblFamilyRegistration>();
           // tblFamilyRegistration familyRegistrationModel = new tblFamilyRegistration();
            fr = db.tblFamilyRegistrations.ToList();
            
            ReportDataSource rd = new ReportDataSource("FamilyRegDataSet", fr);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>" + id + "</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0.5in</MarginTop>" +
                "<MarginLeft>1in</MarginLeft>" +
                "<MarginRight>1in</MarginRight>" +
                "<MarginBottom>0.5in</MarginBottom>" +
                 "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        public ActionResult FamilyRegSingleReport(int id = 0)
        {
            tblFamilyRegistration familyRegistrationModel = new tblFamilyRegistration();

            tblEducationLevel EducationLevelModel = new tblEducationLevel();
            var getEducationLevelList = db.tblEducationLevels.ToList();
            SelectList list = new SelectList(getEducationLevelList, "educationLevelID", "educationLevelName");
            ViewBag.listname = list;

            //  ViewData["FamMemDat"] = FMD;
            return View(familyRegistrationModel);
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
    }
}