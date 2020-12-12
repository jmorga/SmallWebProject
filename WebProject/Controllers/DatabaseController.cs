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
        // GET: Database
        Database database;
        public ActionResult Database()
        {
            database = new Database();

            //Load data from database here
            //List<Person> people = database.getPersonList();

            return View();
        }

       
    }
}