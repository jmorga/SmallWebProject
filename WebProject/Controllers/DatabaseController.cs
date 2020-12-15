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
        //Creates a database object, calls the getPersonList method to get all the data from the Person table in the people database,
        //the method returns a list of person objects
        public JsonResult GetPeople()
        {
            Database database = new Models.Database();

            return Json(database.getPersonList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //It checks if any of the parameters are null. If they're not, a Database and Person object will be created and the Person table from the 
        //people databased will be updated with the new information of a changed person
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