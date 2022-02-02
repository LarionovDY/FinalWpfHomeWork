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

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public void CalculateResult()
        {
            ValidateOperation();
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
                        if (Convert.ToDouble(FirstOperand) != 0)
                            result = (1 / (Convert.ToDouble(FirstOperand))).ToString();
                        else
                            throw new Exception();
                        break;

                    case ("sqr"):
                        result = Math.Pow(Convert.ToDouble(FirstOperand), 2).ToString();
                        break;

                    case ("sqrt"):
                        if (Convert.ToDouble(FirstOperand) >= 0)
                            result = Math.Sqrt(Convert.ToDouble(FirstOperand)).ToString();
                        else
                            throw new Exception();
                        break;

                    case ("cos"):
                        result = Math.Cos(DegreeToRadian(Convert.ToDouble(FirstOperand))).ToString();
                        break;

                    case ("sin"):
                        result = Math.Sin(DegreeToRadian(Convert.ToDouble(FirstOperand))).ToString();
                        break;

                    case ("tan"):
                        result = Math.Tan(DegreeToRadian(Convert.ToDouble(FirstOperand))).ToString();
                        break;

                    case ("log"):
                        result = Math.Log(Convert.ToDouble(FirstOperand)).ToString();
                        break;
                }
            }
            catch (Exception)
            {
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
                throw;
            }
        }

        private void ValidateOperation()
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
                case "cos":
                case "sin":
                case "tan":
                case "log":
                    ValidateOperand(FirstOperand);
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
