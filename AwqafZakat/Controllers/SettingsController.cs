using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class SettingsController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        // GET: Settings
        public ActionResult Index()
        {
            return View(db.tblSettings.ToList());
        }

        // GET: Settings/Create
        public ActionResult Add(int id = 0)
        {
            tblSetting settingModel = new tblSetting();

            return View(settingModel);
        }

        // POST: Settings/Create
        [HttpPost]
        public ActionResult Add(tblSetting objSettings)
        {
            try
            {
                tblSetting settingModel = new tblSetting();
                db.tblSettings.Add(objSettings);
                db.SaveChanges();
               
                ModelState.Clear();
                // ViewBag.SuccessMessage = "Registration Successful";
                TempData["SuccessMessage"] = "Insert Successful";
                //return View("AddOrEdit", new tbl_adminRegistrations());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EducationLevels/Edit/5
        public ActionResult Edit(int id)
        {
            tblSetting settingModel = new tblSetting();
            return View(db.tblSettings.Where(x => x.settingsID == id).FirstOrDefault());
        }

        // POST: Settings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblSetting objSettings)
        {
            try
            {
                // TODO: Add update logic here
                tblSetting settingModel = new tblSetting();

                db.Entry(objSettings).State = EntityState.Modified;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Update Successful";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Settings/Delete/5
        public ActionResult Delete(int id)
        {
            tblSetting settingModel = new tblSetting();

            return View(db.tblSettings.Where(x => x.settingsID
            == id).FirstOrDefault());
        }

        // POST: Settings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, tblSetting objSettings)
        {
            try
            {
                // TODO: Add delete logic here
                tblSetting settingModel = new tblSetting();
                {
                    objSettings = db.tblSettings.Where(x => x.settingsID == id).FirstOrDefault();
                    db.tblSettings.Remove(objSettings);
                    db.SaveChanges();
                }
                TempData["SuccessMessage"] = "Deleted Successful";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}