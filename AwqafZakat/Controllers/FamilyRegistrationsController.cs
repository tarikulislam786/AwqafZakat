using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class FamilyRegistrationsController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        // GET: FamilyRegistrations
        public ActionResult FamilyReg(int id = 0)
        {
            tblFamilyRegistration familyRegistrationModel = new tblFamilyRegistration();

            tblEducationLevel EducationLevelModel = new tblEducationLevel();
            var getEducationLevelList = db.tblEducationLevels.ToList();
            SelectList list = new SelectList(getEducationLevelList, "educationLevelID", "educationLevelName");
            ViewBag.listname = list;

            //  ViewData["FamMemDat"] = FMD;
            return View(familyRegistrationModel);
        }

        // POST: FamilyRegistrations
        [HttpPost]
        public ActionResult FamilyReg(tblFamilyRegistration objfamilyRegistrations)
        {
            tblEducationLevel EducationLevelModel = new tblEducationLevel();
            var getEducationLevelList = db.tblEducationLevels.ToList();
            SelectList list = new SelectList(getEducationLevelList, "educationLevelID", "educationLevelName");
            ViewBag.listname = list;
            // unique checking civil number
            if (db.tblFamilyRegistrations.Any(x => x.civilNumber == objfamilyRegistrations.civilNumber))
            {
                TempData["ErrorMessage"] = "Civil Number already exists";
                return View("FamilyReg", objfamilyRegistrations);
            }
            if (objfamilyRegistrations.ImageUpload != null)
            {
                foreach (var item in objfamilyRegistrations.ImageUpload)
                { // iterate in each file
                    if (item != null)
                    { // check file is null or not
                        if (item.ContentLength > 0)
                        { // check length of bytes are greater than zero or not
                            string fileName = Path.GetFileNameWithoutExtension(item.FileName);
                            string extension = Path.GetExtension(item.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            objfamilyRegistrations.document = "~/AppFiles/Images/" + fileName;
                            item.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                        }
                    }
                }
            }
            objfamilyRegistrations.familyRegNumber = Convert.ToInt32("2018") + Convert.ToInt32(objfamilyRegistrations.civilNumber.ToString());
            objfamilyRegistrations.status = "A";
            db.tblFamilyRegistrations.Add(objfamilyRegistrations);
            
            db.SaveChanges();
            // lastInsertId of FamilyRegistration
            int lastInsertFamilyRegID = objfamilyRegistrations.familyRegID;

            // member details adding
           // tblFamilymemberDetail familyMemberDetailModel = new tblFamilymemberDetail();

            string[] familymembernames = Request.Form.GetValues("familymembername[]");
            string[] memberCivilNumbers = Request.Form.GetValues("memberCivilNumber[]");
            string[] relativerelations = Request.Form.GetValues("relativerelation[]");
            string[] @cases = Request.Form.GetValues("case[]");
            string[] educationLevelIDs = Request.Form.GetValues("educationLevelID[]");
            string[] educationalInstitutes = Request.Form.GetValues("educationalInstitute[]");
            string[] memberNotes = Request.Form.GetValues("memberNotes[]");
            // count posted all array object membernames including empty array obj
            //int memberAmount = familymembernames.Count(s => s != null);
            // count filtered array obj that means posted all not empty array obj
            int memberAmount = (from s in familymembernames where !string.IsNullOrEmpty(s) select s).Count();
            for (int i = 0; i < memberAmount; i++)
              {
                 tblFamilymemberDetail objfamilymemberDetails = new tblFamilymemberDetail();
                 objfamilymemberDetails.familyRegID = lastInsertFamilyRegID;
                 objfamilymemberDetails.educationLevelID = Convert.ToInt16(educationLevelIDs[i]);
                 objfamilymemberDetails.familymembername = familymembernames[i];
                 objfamilymemberDetails.memberCivilNumber = memberCivilNumbers[i];
                 objfamilymemberDetails.@case = @cases[i];
                 objfamilymemberDetails.relativerelation = relativerelations[i];
                 objfamilymemberDetails.educationalInstitute = educationalInstitutes[i];
                 objfamilymemberDetails.memberNotes = memberNotes[i];
                 db.tblFamilymemberDetails.Add(objfamilymemberDetails);
                 db.SaveChanges();
              }
            // end member details adding

            // family Income expense adding
           // tblFamilyIncomeExpense familyIncomeExpenseModel = new tblFamilyIncomeExpense();
            string[] householdincomes = Request.Form.GetValues("householdincome[]");
            string[] incomenumbers = Request.Form.GetValues("incomenumber[]");
            string[] incomeamounts = Request.Form.GetValues("incomeamount[]");
            string[] householdexpenses = Request.Form.GetValues("householdexpense[]");
            string[] expensenumbers = Request.Form.GetValues("expensenumber[]");
            string[] expenseamounts = Request.Form.GetValues("expenseamount[]");
           // string[] memberNotes = Request.Form.GetValues("memberNotes[]");
           
            // family Income adding
           // int incomeAmount = householdincomes.Count(s => s != null);
           int incomeAmount = (from s in householdincomes where !string.IsNullOrEmpty(s) select s).Count();
           for (int i = 0; i < incomeAmount; i++)
              {
                  tblFamilyIncomeExpense objIncomeExpenseDetails = new tblFamilyIncomeExpense();
                  objIncomeExpenseDetails.familyRegID = lastInsertFamilyRegID;
                  objIncomeExpenseDetails.type = "i";
                  objIncomeExpenseDetails.particular = householdincomes[i];
                  objIncomeExpenseDetails.quantity = Convert.ToInt16(incomenumbers[i]);
                  objIncomeExpenseDetails.amount = Convert.ToDecimal(incomeamounts[i]);
                  db.tblFamilyIncomeExpenses.Add(objIncomeExpenseDetails);
                  db.SaveChanges();
                }
                // end family income adding

                // add family expense adding
                // int expenseAmount = householdexpenses.Count(s => s != null);
                int expenseAmount = (from s in householdexpenses where !string.IsNullOrEmpty(s) select s).Count();
                for (int i = 0; i < expenseAmount; i++)
                {
                    tblFamilyIncomeExpense objIncomeExpenseDetails = new tblFamilyIncomeExpense();
                    objIncomeExpenseDetails.familyRegID = lastInsertFamilyRegID;
                    objIncomeExpenseDetails.type = "e";
                    objIncomeExpenseDetails.particular = householdexpenses[i];
                    objIncomeExpenseDetails.quantity = Convert.ToInt16(expensenumbers[i]);
                    objIncomeExpenseDetails.amount = Convert.ToDecimal(expenseamounts[i]);
                    db.tblFamilyIncomeExpenses.Add(objIncomeExpenseDetails);
                    db.SaveChanges();
                }
            // end Familyexpepense adding
            // property details adding
           // tblFamilyPropertyDetail familyPropertyDetailModel = new tblFamilyPropertyDetail();
            
            string[] familyproperties = Request.Form.GetValues("familyproperty[]");
            string[] familypropertynumbers = Request.Form.GetValues("familypropertynumber[]");

            // int propertiesAmount = familyproperties.Count(s => s != null);
            int propertiesAmount = (from s in familyproperties where !string.IsNullOrEmpty(s) select s).Count();
            for (int i = 0; i < propertiesAmount; i++)
              {
                 tblFamilyPropertyDetail objfamilypropertyDetails = new tblFamilyPropertyDetail();
                 objfamilypropertyDetails.familyRegID = lastInsertFamilyRegID;
                 objfamilypropertyDetails.familyProperty = familyproperties[i];
                 objfamilypropertyDetails.numOfProperty = Convert.ToInt16(familypropertynumbers[i]);
                 db.tblFamilyPropertyDetails.Add(objfamilypropertyDetails);
                 db.SaveChanges();
               }
             // end FamilyPropertyDetailModels adding

            ModelState.Clear();
            // ViewBag.SuccessMessage = "Registration Successful";
            TempData["SuccessMessage"] = "Registration Successful";
            return View("FamilyReg", new tblFamilyRegistration());
        }

    }
}