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
            Database database = new Models.Database();
            Wrapper list = database.getPersonList();

            return Json( new {result = list.result, jsonStr = list.data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //It checks if any of the parameters are null. If they're not, a Database and Person object will be created and the Person table from the 
        //people databased will be updated with the new information of a changed person
        public JsonResult ChangePeople(string id, string firstName, string lastName)
        {
            Database database = new Models.Database();
            Wrapper update = database.changePersonData(new Person(Int32.Parse(id), firstName, lastName));

            return Json(new { result = update.result, message = update.data }, JsonRequestBehavior.AllowGet);
        }
    }
}