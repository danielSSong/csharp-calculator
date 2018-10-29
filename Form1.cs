using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Calculator : Form
    {
        private string buffer; // the buffer for bind the string input

        public Calculator()
        {
            InitializeComponent();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            #region Input the string into the buffer when the button clicked

            if (btn == btnOne)
            {
                buffer += "1";
            }
            else if (btn == btnTwo)
            {
                buffer += "2";
            }
            else if (btn == btnThree)
            {
                buffer += "3";
            }
            else if (btn == btnFour)
            {
                buffer += "4";
            }
            else if (btn == btnFive)
            {
                buffer += "5";
            }
            else if (btn == btnSix)
            {
                buffer += "6";
            }
            else if (btn == btnSeven)
            {
                buffer += "7";
            }
            else if (btn == btnEight)
            {
                buffer += "8";
            }
            else if (btn == btnNine)
            {
                buffer += "9";
            }
            else if (btn == btnZero)
            {
                buffer += "0";
            }
            else if (btn == btnModule)
            {
                buffer += "%";
            }
            else if (btn == btnPower)
            {
                buffer += "^";
            }
            else if (btn == btnPlus)
            {
                buffer += "+";
            }
            else if (btn == btnMinus)
            {
                buffer += "-";
            }
            else if (btn == btnMultiply)
            {
                buffer += "x";
            }
            else if (btn == btnDivide)
            {
                buffer += "/";
            }
            else if (btn == btnPoint)
            {
                buffer += ".";
            }

            #endregion

            textExpression.Text = buffer;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            buffer = String.Empty;
            textExpression.Text = "0";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (buffer.Length > 0)
            {
                buffer = buffer.Substring(0, buffer.Length - 1);
                textExpression.Text = buffer;
            }
            if (buffer.Length < 1)
            {
                textExpression.Text = "0";
                buffer = "0";
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            String temp = textExpression.Text.Replace("-+", "-").Replace("+-", "-").Replace("++", "+").Replace("--", "+");

            if (String.IsNullOrEmpty(temp))
            {
                textExpression.Text = "Invalid Double";
                return;
            }
            this.textExpression.Text = PlusMinusExpression(ref temp).ToString();
            if (textExpression.Text.Equals("NaN"))
            {
                buffer = String.Empty;
                textExpression.Text = "Invalid Double";
                return;
            }
            if (textExpression.Text.Equals("234523452645"))
            {
                buffer = String.Empty;
                textExpression.Text = "Invalid Double";
                return;
            }
            buffer = "";
        }

        public double PlusMinusExpression(ref string str)
        {
            int index = 0; 
            double value = MultipleDIvideExpression(ref str, ref index);
            while (index < str.Length)
            {
                try
                {
                    switch (str[index])
                    {
                        case '+':
                            ++(index);
                            value += MultipleDIvideExpression(ref str, ref index);
                            break;
                        case '-':
                            ++(index);
                            value -= MultipleDIvideExpression(ref str, ref index);
                            break;
                        default:
                            textExpression.Text = "Invalid Double";
                            return 234523452645;
                    }
                }
                catch (Exception)
                {
                    textExpression.Text = "Invalid Double";
                }
            }
            return value;
        }

        public double MultipleDIvideExpression(ref string str, ref int index)
        {
            double value = PowerExpression(ref str, ref index);
            while (index < str.Length)
            {
                if (str[index] == 'x')
                {
                    ++(index);
                    value *= PowerExpression(ref str, ref index);

                }
                else if (str[index] == '/')
                {
                    try
                    {
                        ++(index);
                        value /= PowerExpression(ref str, ref index);
                    }
                    catch (DivideByZeroException)
                    {
                        textExpression.Text = "Invalid Double";
                        break;
                    }
                }
                else if (str[index] == '%')
                {
                    ++(index);
                    value %= PowerExpression(ref str, ref index);
                }
                else
                    break;
            }
            return value;
        }


        public double PowerExpression(ref string str, ref int index)
        {
            double value = GetNumber(ref str, ref index);
            while (index < str.Length)
            {
                if (str[index] == '^')
                {
                    ++(index);
                    if (str[index] == '-')
                    {
                        Console.WriteLine("a");
                        ++(index);
                        if(str[index] == '.')
                        {
                            Console.WriteLine("b");
                            ++(index);
                            if(str[index] == '5')
                            {
                                ++(index);
                                Console.WriteLine("c");
                                value = Math.Sqrt(value);
                                value = 1 / value;
                                break;
                            }
                        }
                        Console.WriteLine("d");
                        value = Math.Pow(value, GetNumber(ref str, ref index));
                        value = 1 / value;
                    }
                    else if (str[index] == '.')
                    {
                        ++(index);
                        if (str[index] == '5')
                        { 
                            ++(index);
                            value = Math.Sqrt(value);
                        }
                    }
                    else
                    {
                        value = Math.Pow(value, GetNumber(ref str, ref index));
                    }
                }
                else
                    break;
            }
            return value;
        }
 
        public double GetNumber(ref string str, ref int index)
        {
            double value = 0.0;

            while ((index < str.Length) && Char.IsDigit(str, index))
            {
                value = 10.0 * value + Char.GetNumericValue(str[(index)]);
                ++(index);
            }
            if ((index == str.Length) || str[index] != '.') return value;
            double factor = 1.0;
            ++(index);

            while ((index < str.Length) && Char.IsDigit(str, index))
            {
                factor *= 0.1;
                value = value + Char.GetNumericValue(str[index]) * factor;
                ++(index);
            }

            return value;
        }
    }
}

