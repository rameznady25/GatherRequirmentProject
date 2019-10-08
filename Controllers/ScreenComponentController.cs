using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gather_Requirement_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gather_Requirement_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Gather_Requirement_Project.Controllers
{
    [Authorize]
    public class ScreenComponentController : Controller
    {
        Context context;
        public ScreenComponentController(Context _context)
        {
            context = _context;

        }

        public ActionResult Index(int? screenId)
        {
            IQueryable<ScreenComponent> screenComponents = context.ScreenComponent.Include(s => s.Screen).Include(f => f.FieldType).Include(s => s.InputType);
            if (screenId != null)
                screenComponents = screenComponents.Where(s => s.ScreenID == screenId);
            ViewBag.screenId = screenId;

            return View(screenComponents.ToList());
        }

        public ActionResult Details(int id)
        {
            ScreenComponent screencomponentlist = context.ScreenComponent.FirstOrDefault(s => s.ID == id);
            IEnumerable<Screen> ScreenList = context.Screens.ToList();
            IEnumerable<InputType> inputypeList = context.InputTypes.ToList();
            IEnumerable<FieldType> fieldtypeList = context.FieldTypes.ToList();
            ScreenComponent screenComponentDepend = context.ScreenComponent.FirstOrDefault(c => c.ID == screencomponentlist.ScreenComponentID);
            if (screenComponentDepend == null)
                ViewBag.ScreenComponentDepend = "";
            else
                ViewBag.ScreenComponentDepend = screenComponentDepend.FieldNameEnglish;


            return View(screencomponentlist);
        }

        public ActionResult Create(int? screenId)
        {
            IEnumerable<Screen> screens = context.Screens.ToList();
            ViewBag.screenList = screens;

            IEnumerable<InputType> inputTypes = context.InputTypes.ToList();
            ViewBag.inputTypesList = inputTypes;

            IEnumerable<FieldType> fieldTypes = context.FieldTypes.ToList();
            ViewBag.fieldTypesList = fieldTypes;

            ViewBag.screenComponentList1 = "";
            ViewBag.screenComponentList2 = "";
            ViewBag.screenId = screenId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScreenComponent screenComponent)
        {
            if (screenComponent != null)
            {
                IEnumerable<Screen> screens = context.Screens.ToList();
                ViewBag.screenList = screens;

                //IEnumerable<ScreenComponent> screenComponentList1 = context.ScreenComponent.Where(e => e.FieldTypeID == 1);
                //ViewBag.screenComponentList1 = screenComponentList1;

                //IEnumerable<ScreenComponent> screenComponentList2 = context.ScreenComponent.Where(e => e.FieldTypeID == 2 || e.FieldTypeID == 3);
                //ViewBag.screenComponentList2 = screenComponentList2;

                ViewBag.screenComponentList1 = "";
                ViewBag.screenComponentList2 = "";

                IEnumerable<FieldType> fieldTypes = context.FieldTypes.ToList();
                ViewBag.fieldTypesList = fieldTypes;

                // TODO: Add insert logic here
                context.ScreenComponent.Add(screenComponent);
                context.SaveChanges();


                return RedirectToAction(nameof(Index), new { screenId = screenComponent.ScreenID });
            }
            else
            {
                IEnumerable<Screen> screens = context.Screens.ToList();
                ViewBag.screenList = screens;

                IEnumerable<InputType> inputTypes = context.InputTypes.ToList();
                ViewBag.inputTypesList = inputTypes;

                IEnumerable<ScreenComponent> screenComponentList1 = context.ScreenComponent.Where(e => e.FieldTypeID == 1);
                ViewBag.screenComponentList1 = screenComponentList1;

                IEnumerable<ScreenComponent> screenComponentList2 = context.ScreenComponent.Where(e => e.FieldTypeID == 2 || e.FieldTypeID == 3 || e.FieldTypeID == 4 || e.FieldTypeID == 5);
                ViewBag.screenComponentList2 = screenComponentList2;

                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ScreenComponent ScreenComponentEdit = context.ScreenComponent.FirstOrDefault(s => s.ID == id);

            IEnumerable<Screen> screen = context.Screens.ToList();
            ViewBag.screenList = screen;

            IEnumerable<InputType> inputTypes = context.InputTypes.ToList();
            ViewBag.inputTypesList = inputTypes;

            IEnumerable<FieldType> fieldTypes = context.FieldTypes.ToList();
            ViewBag.fieldTypesList = fieldTypes;

            IEnumerable<ScreenComponent> screenComponentList1 = context.ScreenComponent.Where(e => e.FieldTypeID == 1);
            ViewBag.screenComponentList1 = screenComponentList1;

            IEnumerable<ScreenComponent> screenComponentList2 = context.ScreenComponent.Where(e => e.FieldTypeID == 2 || e.FieldTypeID == 3 || e.FieldTypeID == 4 || e.FieldTypeID == 5);
            ViewBag.screenComponentList2 = screenComponentList2;

            return View(ScreenComponentEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ScreenComponent collection)
        {
            // TODO: Add update logic here
            try
            {
                context.Entry(collection).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction(nameof(Index), new { screenId = collection.ScreenID });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            ScreenComponent screencomponent = context.ScreenComponent.FirstOrDefault(s => s.ID == id);

            context.ScreenComponent.Remove(screencomponent);
            context.SaveChanges();
            return RedirectToAction(nameof(Index), new { screenId = screencomponent.ScreenID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        [HttpGet("ScreenComponent/LoadDropDown/{id}")]
        public JsonResult LoadDropDown([FromRoute]int id)
        {
            List<ScreenComponent> scrs = context.ScreenComponent.Where(e => e.FieldTypeID == 1 && e.ScreenID == id).ToList();
            List<string> var = new List<string>();
            var.Clear();
            foreach (var item in scrs)
            {
                var.Add(Convert.ToString(item.ID));
                var.Add(item.FieldNameEnglish);
            }
            return Json(var);
        }

        [HttpGet("ScreenComponent/LoadOther/{id}")]
        public JsonResult LoadOther([FromRoute]int id)
        {
            List<ScreenComponent> scrs = context.ScreenComponent.Where(e => (e.FieldTypeID == 2 && e.ScreenID == id) || (e.FieldTypeID == 3 && e.ScreenID == id) || (e.FieldTypeID == 4 && e.ScreenID == id) || (e.FieldTypeID == 5 && e.ScreenID == id)).ToList();
            List<string> var = new List<string>();
            var.Clear();
            foreach (var item in scrs)
            {
                var.Add(Convert.ToString(item.ID));
                var.Add(item.FieldNameEnglish);
            }
            return Json(var);
        }

    }
}