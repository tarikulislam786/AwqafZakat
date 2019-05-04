using AwqafZakat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwqafZakat.Controllers
{
    public class EducationLevelsController : Controller
    {
        AwqafZakatEntities db = new AwqafZakatEntities();
        // GET: EducationLevels
        public ActionResult Index()
        {
            return View(db.tblEducationLevels.ToList());
            
        }
        // GET: EducationLevels/Create
        public ActionResult Add(int id = 0)
        {
            tblEducationLevel EducationLevelModel = new tblEducationLevel();

            return View(EducationLevelModel);
        }

        // POST: EducationLevels/Create
        [HttpPost]
        public ActionResult Add(tblEducationLevel objEducationLevels)
        {
            try
            {
                tblEducationLevel EducationLevelModel = new tblEducationLevel();
                EducationLevelModel.educationLevelName = objEducationLevels.educationLevelName;
                db.tblEducationLevels.Add(EducationLevelModel);
                db.SaveChanges();
                ModelState.Clear();
                // ViewBag.SuccessMessage = "Registration Successful";
                TempData["SuccessMessage"] = "Insert Successful";
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
            tblEducationLevel EducationLevelModel = new tblEducationLevel();
            return View(db.tblEducationLevels.Where(x => x.educationLevelID == id).FirstOrDefault());
        }

        // POST: EducationLevels/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblEducationLevel objEducationLevels)
        {
            try
            {
                // TODO: Add update logic here
                tblEducationLevel educationLevelModel = new tblEducationLevel();
                
                    db.Entry(objEducationLevels).State = EntityState.Modified;
                    db.SaveChanges();
               
                TempData["SuccessMessage"] = "Update Successful";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EducationLevels/Delete/5
        public ActionResult Delete(int id)
        {
            tblEducationLevel educationLevelModel = new tblEducationLevel();
            
                return View(db.tblEducationLevels.Where(x => x.educationLevelID == id).FirstOrDefault());
            
        }
        // POST: EducationLevels/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, tblEducationLevel objEducationLevels)
        {
            try
            {
                // TODO: Add delete logic here
                tblEducationLevel educationLevelModel = new tblEducationLevel();
                {
                    objEducationLevels = db.tblEducationLevels.Where(x => x.educationLevelID == id).FirstOrDefault();
                    db.tblEducationLevels.Remove(objEducationLevels);
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