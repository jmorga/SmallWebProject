using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class DatabaseController : Controller
    {
        public ActionResult Database()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetPeople()
        {
            Database database = new Models.Database();
            List<Person> personList = database.getPersonList();

            return Json(personList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangePeople(string id, string firstName, string lastName)
        {
            if (id != null && firstName != null && lastName != null)
            {
                Database database = new Models.Database();
                database.changePersonData(new Person(Int32.Parse(id), firstName, lastName));
                return Json("Named was changed", JsonRequestBehavior.AllowGet);
            }

            return Json("Nothing changed", JsonRequestBehavior.AllowGet);
        }
    }
}