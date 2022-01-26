using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Models
{
    public class CalculatorModel
    {
        private string result;

        public string FirstOperand { get; set; }

        public string SecondOperand { get; set; }

        public string Operation { get; set; }

        public string Result { get => result; }

        public CalculatorModel()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }

        public CalculatorModel(string firstOperand, string operation) 
        {
            ValidateOperand(firstOperand);            
            ValidateOperation(operation);

            FirstOperand = firstOperand;
            SecondOperand = string.Empty;
            Operation = operation;
            result = string.Empty;
        }

        public CalculatorModel(string firstOperand, string secondOperand, string operation)
        {
            ValidateOperand(firstOperand);
            ValidateOperand(secondOperand);
            ValidateOperation(operation);

            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
            result = string.Empty;
        }

        public void CalculateResult()
        {
            ValidateData();
            try
            {
                switch (Operation)
                {
                    case ("+"):
                        result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("-"):
                        result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("*"):
                        result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("/"):
                        result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("1/x"):
                        result = (1 / (Convert.ToDouble(FirstOperand))).ToString();
                        break;

                    case ("sqr"):
                        result = Math.Pow(Convert.ToDouble(FirstOperand), 2).ToString();
                        break;

                    case ("sqrt"):
                        result = Math.Sqrt(Convert.ToDouble(FirstOperand)).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                result = "ERROR";
                throw;
            }
        }

        private void ValidateOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (Exception)
            {
                result = "ERROR";
                throw;
            }
        }

        private void ValidateOperation(string operation)
        {
            switch (operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                case "1/x":
                case "sqr":
                case "sqrt":
                    break;
                default:
                    result = "ERROR";
                    throw new ArgumentException("Unknown Operation: " + operation, "operation");
            }
        }

        private void ValidateData()
        {
            switch (Operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                    ValidateOperand(FirstOperand);
                    ValidateOperand(SecondOperand);
                    break;
                case "1/x":
                case "sqr":
                case "sqrt":
                    ValidateOperand(FirstOperand);
                    break;
                default:
                    result = "ERROR ";
                    throw new ArgumentException("Unknown Operation: " + Operation, "operation");
            }
        }
    }
}
