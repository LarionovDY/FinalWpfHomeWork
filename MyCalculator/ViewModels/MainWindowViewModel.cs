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
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string fullExpression;
        private string display;

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
            get => display;
            set
            {
                display = value;
                OnPropertyChanged();
            }
        }

        public string FullExpression
        {
            get => fullExpression;
            set
            {
                fullExpression = value;
                OnPropertyChanged();
            }
        }

        public ICommand DigitButtonPressCommand { get; }

        public void OnDigitButtonPressCommandExecute(string button)
        {
            switch (button) //рефакторить
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    FullExpression = string.Empty;
                    break;
                case "Del":
                    if (display.Length > 1)
                        Display = display.Substring(0, display.Length - 1);
                    else Display = "0";
                    break;
                case "+/-":
                    if (display.Contains("-") || display == "0")
                    {
                        Display = display.Remove(display.IndexOf("-"), 1);
                    }
                    else Display = "-" + display;
                    break;
                case ".":
                    if (newDisplayRequired)
                    {
                        Display = "0.";
                    }
                    else
                    {
                        if (!display.Contains("."))
                        {
                            Display = display + ".";
                        }
                    }
                    break;
                default:
                    if (display == "0" || newDisplayRequired)
                        Display = button;
                    else
                        Display = display + button;
                    break;
            }
            newDisplayRequired = false;
        }

        private static bool CanDigitButtonPressCommandExecuted(string button)
        {
            return true;
        }

    }
}
