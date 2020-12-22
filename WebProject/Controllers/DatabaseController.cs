using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;

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
            string jsonString;

            try
            {
                jsonString = database.getPersonList();
            }
            catch (InvalidOperationException e)
            {
                return Json(new { result = false, jsonStr = $"InvalidOperationException: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException e)
            {
                return Json(new { result = false, jsonStr = $"Argument Exception: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException e)
            {   
                return Json(new { result = false, jsonStr = $"AqlException: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { result = false, jsonStr = $"Exception: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }

            return Json( new {result = true, jsonStr = jsonString }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //It checks if any of the parameters are null. If they're not, a Database and Person object will be created and the Person table from the 
        //people databased will be updated with the new information of a changed person
        public JsonResult ChangePeople(string id, string firstName, string lastName)
        {
            Database database = new Models.Database();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return Json(new { result = false, message = "Error: An input field is null or empty" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                database.changePersonData(new Person(Int32.Parse(id), firstName, lastName));
            }
            catch (ArgumentException e)
            {
                return Json(new { result = false, message = $"Argument Exception: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException e)
            {
                return Json(new { result = false, message = $"AqlException: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { result = false, message = $"Exception: {e.Message}" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true, message = "Data updated successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}