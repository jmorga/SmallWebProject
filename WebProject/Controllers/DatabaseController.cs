using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
using Newtonsoft.Json;

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
            string exception = "Exeption, Error";

            Database database = new Models.Database();
            string jsonString = database.getPersonList();

            if (jsonString.Contains(exception))
            {
                return Json(new { error = true, jsonStr = jsonString}, JsonRequestBehavior.AllowGet);
            }

            return Json( new {error = false, jsonStr = jsonString }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //It checks if any of the parameters are null. If they're not, a Database and Person object will be created and the Person table from the 
        //people databased will be updated with the new information of a changed person
        public JsonResult ChangePeople(string id, string firstName, string lastName)
        {
            string success;

            if (id == null || firstName == null || lastName == null || id == "" || firstName == "" || lastName == "")
            {
                return Json(new { error = true, message = "Error: An input field is null or empty" }, JsonRequestBehavior.AllowGet);
            }

            Database database = new Models.Database();
            success = database.changePersonData(new Person(Int32.Parse(id), firstName, lastName));

            if (!success.Equals("true"))
            {
                return Json(new { error = true, message = success }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { error = false, message = success }, JsonRequestBehavior.AllowGet);
        }
    }
}