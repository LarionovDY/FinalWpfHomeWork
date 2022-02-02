using MyCalculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCalculator.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string expression;
        private string display = "0";

        private CalculatorModel calculator = new CalculatorModel();

        public string FirstOperand
        {
            get => calculator.FirstOperand;
            set => calculator.FirstOperand = value;
        }

        public string SecondOperand
        {
            get => calculator.SecondOperand;
            set => calculator.SecondOperand = value;
        }

        public string Operation
        {
            get => calculator.Operation;
            set => calculator.Operation = value;
        }

        public string LastOperation
        {
            get => lastOperation;
            set => lastOperation = value;
        }

        public string Result
        {
            get => calculator.Result;
        }

        public string Display
        {
            get
            {
                if (display.Length > 11)
                    return display.Substring(0, 11);
                else
                    return display;
            }
            set
            {
                display = value;
                OnPropertyChanged();
            }
        }

        public string Expression
        {
            get => expression;
            set
            {
                expression = value;
                OnPropertyChanged();
            }
        }       

        public ICommand DigitButtonPressCommand { get; }

        public void OnDigitButtonPressCommandExecute(object p)
        {
            string button = (string)p;
            switch (button)
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    Expression = string.Empty;
                    break;
                case "Del":
                    if (Display.Length > 1)
                        Display = Display.Substring(0, Display.Length - 1);
                    else 
                        Display = "0";
                    break;
                case "+/-":
                    if (Display.Contains("-"))
                        Display = Display.Remove(Display.IndexOf("-"), 1);
                    else 
                        Display = "-" + Display;
                    break;
                case ",":
                    if (newDisplayRequired)
                        Display = "0,";                    
                    else if (!Display.Contains(","))
                        Display += ",";                   
                    break;
                default:
                    if (Display == "0" || newDisplayRequired)
                        Display = button;
                    else
                        Display += button;
                    break;
            }
            newDisplayRequired = false;
        }

        public static bool CanDigitButtonPressCommandExecuted(object p)
        {
            return true;
        }

        public ICommand SingleOperationButtonPressCommand { get; }

        public void OnSingleOperationButtonPressCommand(object p)
        {
            string operation = (string)p;
            try
            {
                FirstOperand = Display;
                Operation = operation;
                calculator.CalculateResult();
                Expression = Operation + "(" + Math.Round(Convert.ToDouble(FirstOperand), 5) + ") = ";
                LastOperation = "=";
                Display = Result;
                FirstOperand = Display;
                newDisplayRequired = true;
            }
            catch (Exception)
            {
                Display = "ERROR";
            }
        }

        public bool CanSingleOperationButtonPressCommand(object p)
        {
            return true;
        }

        public ICommand DoubleOperationButtonPressCommand { get; }

        public void OnDoubleOperationButtonPressCommand(object p)
        {
            string operation = (string)p;
            try
            {
                if (FirstOperand == string.Empty || LastOperation == "=")
                {
                    FirstOperand = Display;
                    LastOperation = operation;
                }
                else
                {
                    SecondOperand = Display;
                    Operation = LastOperation;
                    calculator.CalculateResult();
                    Expression = Math.Round(Convert.ToDouble(FirstOperand), 5) + " "
                        + Operation + " " + Math.Round(Convert.ToDouble(SecondOperand), 5) + " = ";
                    LastOperation = operation;
                    Display = Result;
                    FirstOperand = Display;
                }
                newDisplayRequired = true;
            }
            catch (Exception)
            {
                Display = "ERROR";
            }
        }

        public bool CanDoubleOperationButtonPressCommand(object p)
        {
            return true;
        }

        public MainWindowViewModel()
        {
            DigitButtonPressCommand = new RelayCommand(OnDigitButtonPressCommandExecute, CanDigitButtonPressCommandExecuted);
            SingleOperationButtonPressCommand = new RelayCommand(OnSingleOperationButtonPressCommand, CanSingleOperationButtonPressCommand);
            DoubleOperationButtonPressCommand = new RelayCommand(OnDoubleOperationButtonPressCommand, CanDoubleOperationButtonPressCommand);
        }
    }
}
