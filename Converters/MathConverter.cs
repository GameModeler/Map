using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Map.Converters
{
    /// <summary>
    /// Perform a math equation on a bound value
    /// </summary>
    public class MathConverter : IValueConverter
    {
        #region Attributes

        private static readonly char[] AllOperators = new[] {'+', '-', '*', '/', '%', '(', ')'};

        private static readonly List<string> Operators = new List<string> {"+", "-", "*", "/", "%"};

        private static readonly List<string> Grouping = new List<string> { "(", ")" };

        #endregion

        #region Methods

        /// <summary>
        /// Convert the value
        /// </summary>
        /// <param name="value">Value produced by the binding source</param>
        /// <param name="targetType">Type of the binding target property</param>
        /// <param name="parameter">Converter parameter to use</param>
        /// <param name="culture">Cuture to use in the converter</param>
        /// <returns>Converted value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var numbers = new List<double>();
            var mathEquation = parameter as string;
            mathEquation = mathEquation?.Replace(" ", "");
            mathEquation = mathEquation?.Replace("@VALUE", value?.ToString());
            double tmp;

            if (mathEquation == null || value == null)
            {
                return null;
            }

            foreach (var s in mathEquation.Split(AllOperators))
            {
                if (s == string.Empty)
                {
                    continue;
                }

                if (double.TryParse(s, out tmp))
                {
                    numbers.Add(tmp);
                }
                else
                {
                    throw new InvalidCastException("Some non-numeric, operator or grouping character found");
                }
            }

            EvaluateMathString(ref mathEquation, ref numbers, 0);

            return numbers[0];
        }

        /// <summary>
        /// Convert back the value
        /// </summary>
        /// <param name="value">Value produced by the binding source</param>
        /// <param name="targetType">Type of the binding target property</param>
        /// <param name="parameter">Converter parameter to use</param>
        /// <param name="culture">Cuture to use in the converter</param>
        /// <returns>Converted value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Evaluate a mathematical string and keep track of the results in a list of numbers
        /// </summary>
        /// <param name="mathEquation">The math equation</param>
        /// <param name="numbers">List of numbers</param>
        /// <param name="index">Current numbers index</param>
        private void EvaluateMathString(ref string mathEquation, ref List<double> numbers, int index)
        {
            var token = GetNextToken(mathEquation);

            // Loop through each mathematical token
            while (token != string.Empty)
            {
                // Remove the token from the equation
                mathEquation = mathEquation.Remove(0, token.Length);

                // If the token is a grouping character
                if (Grouping.Contains(token))
                {
                    switch (token)
                    {
                        case "(":
                            EvaluateMathString(ref mathEquation, ref numbers, index);
                            break;
                        case ")":
                            return;
                    }
                }

                // If the token is an operator
                if (Operators.Contains(token))
                {
                    var nextToken = GetNextToken(mathEquation);

                    if (nextToken == "(")
                    {
                        EvaluateMathString(ref mathEquation, ref numbers, index + 1);
                    }

                    // Verify that enough numbers exist in the list to complete the operation
                    // and that the next token is the number expected or a grouping character
                    if (numbers.Count > index + 1 &&
                        (double.Parse(nextToken).Equals(numbers[index + 1]) || nextToken == "("))
                    {
                        switch (token)
                        {
                            case "+":
                                numbers[index] = numbers[index] + numbers[index + 1];
                                break;
                            case "-":
                                numbers[index] = numbers[index] - numbers[index + 1];
                                break;
                            case "*":
                                numbers[index] = numbers[index] * numbers[index + 1];
                                break;
                            case "/":
                                numbers[index] = numbers[index] / numbers[index + 1];
                                break;
                            case "%":
                                numbers[index] = numbers[index] % numbers[index + 1];
                                break;
                        }

                        numbers.RemoveAt(index + 1);
                    }
                    else
                    {
                        throw new FormatException("Next token is not the expected number");
                    }
                }

                token = GetNextToken(mathEquation);
            }
        }

        /// <summary>
        /// Get the next mathematical token in the equation
        /// </summary>
        /// <param name="mathEquation">The math equation</param>
        /// <returns>The next mathematical token</returns>
        private static string GetNextToken(string mathEquation)
        {
            // If we're at the end of the equation
            if (mathEquation == string.Empty)
            {
                return string.Empty;
            }

            var tmp = string.Empty;

            // Get the next operator or numeric value in the equation
            foreach (var item in mathEquation)
            {
                if (((IList) AllOperators).Contains(item))
                {
                    return tmp == string.Empty ? item.ToString() : tmp;
                }

                tmp += item;
            }

            return tmp;
        }

        #endregion
    }
}
