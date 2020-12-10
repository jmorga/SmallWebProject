using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Calculate(string firstNumber, string secondNumber, string mathOperation)
        {
            return Json((new Calculator(firstNumber, secondNumber, mathOperation)).result, JsonRequestBehavior.AllowGet);
        }
    }
}