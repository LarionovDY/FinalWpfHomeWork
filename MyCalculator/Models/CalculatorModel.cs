using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Models
{
    public class CalculatorModel
    {
        private string number1;

        private string number2;

        private char operation;

        private double result;

        private string display;

        public string Display
        {
            get { return display; }
            set { display = value; }
        }

        public string Number1
        {
            get { return number1; }
            set { number1 = value; }
        }

        public string Number2
        {
            get { return number2; }
            set { number2 = value; }
        }

        public char Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        public double Result
        {
            get { return result; }
            set { result = value; }
        }

        public CalculatorModel()
        {
            //Clear();
        }
    }
}
