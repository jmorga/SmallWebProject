﻿using System;
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
        Database database;

        public DatabaseController()
        {
            this.database = new Models.Database();
        }
        public ActionResult Database()
        {
            return View();
        }

        [HttpPost]
        //Creates a database object, calls the getPersonList method to get all the data from the Person table in the people database,
        //the method returns a list of person objects
        public JsonResult GetPeople()
        {
            string jsonString;
            try
            {
                jsonString = this.database.getPersonList();
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
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return Json(new { result = false, message = "Error: An input field is null or empty" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                this.database.changePersonData(new Person(Int32.Parse(id), firstName, lastName));
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

        [HttpPost]
        public JsonResult GetUpdate(string list)
        {
            Person temp = null;

            List<Person> newData = JsonConvert.DeserializeObject<List<Person>>(this.database.getPersonList());
            List<Person> currentList = JsonConvert.DeserializeObject<List<Person>>(list);
            List<Person> toUpdate = new List<Person>();
 
            foreach(Person person in newData)
            {
                temp = currentList.Find(x => x.id == person.id);

                if (temp != null)
                {
                    if(!temp.Equals(person))
                        toUpdate.Add(person);
                }
                else
                {
                    toUpdate.Add(person);
                }
            }

            return Json(JsonConvert.SerializeObject(toUpdate), JsonRequestBehavior.AllowGet);
        }
    }
}