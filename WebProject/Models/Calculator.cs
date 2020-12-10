using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class Calculator
    {
        public string result { get; }

        public Calculator(string firstNumber, string secondNumber, string symbol)
        {
            switch(symbol)
            {
                case "+":
                    result = $"{Convert.ToDouble(firstNumber) + Convert.ToDouble(secondNumber)}";
                    break;
                case "-":
                    result = $"{Convert.ToDouble(firstNumber) - Convert.ToDouble(secondNumber)}";
                    break;
                case "*":
                    result = $"{Convert.ToDouble(firstNumber) * Convert.ToDouble(secondNumber)}";
                    break;
                case "/":
                    result = $"{Convert.ToDouble(firstNumber) / Convert.ToDouble(secondNumber)}";
                    break;
                default: result = " ";
                    break;
            }
        }
    }
}