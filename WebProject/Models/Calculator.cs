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
            double firstValue, secondValue;
         
            try
            {
                firstValue = Convert.ToDouble(firstNumber);
                secondValue = Convert.ToDouble(secondNumber);
            }
            catch(FormatException e)
            {
                result = " ";
                return;
            }


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