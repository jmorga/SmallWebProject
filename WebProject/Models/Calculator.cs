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
         
            try //if the numbers are not valid, return an empty string
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
                    result = $"{firstValue + secondValue}";
                    break;
                case "-":
                    result = $"{firstValue - secondValue}";
                    break;
                case "*":
                    result = $"{firstValue * secondValue}";
                    break;
                case "/":
                    result = $"{firstValue / secondValue}";
                    break;
                default: result = " ";
                    break;
            }
        }
    }
}