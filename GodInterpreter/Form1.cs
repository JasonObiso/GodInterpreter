using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GodInterpreter
{
    public partial class Interface : Form
    {
        private void Form1_Load (object sender, EventArgs e) { }

        private Dictionary<string, (string, object)> variables = new Dictionary<string, (string, object)>();

        public Interface()
        {
            InitializeComponent();
        }

        private void ExecuteCode(string code)
        {
            bool insideCodeBlock = false;
            StringBuilder output = new StringBuilder();

            string[] lines = code.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string cleanLine = Regex.Replace(line, @"#.*", "").Trim();
                if (string.IsNullOrWhiteSpace(cleanLine))
                    continue;

                if (cleanLine == "BEGIN CODE")
                {
                    insideCodeBlock = true;
                    continue;
                }

                if (cleanLine == "END CODE")
                    break;

                if (insideCodeBlock)
                {
                    if (cleanLine.StartsWith("DISPLAY:"))
                    {
                        string textToDisplay = cleanLine.Substring("DISPLAY:".Length).Trim();
                        output.AppendLine(ProcessDisplayStatement(textToDisplay));
                    }
                    else if (cleanLine.Contains("="))
                    {
                        ParseVariableDeclaration(cleanLine);
                    }
                    else
                    {
                        output.AppendLine(EvaluateArithmeticExpression(cleanLine).ToString());
                    }
                }
            }

            MessageBox.Show(output.ToString(), "Output");
        }

        private void ParseVariableDeclaration(string line)
        {
            string[] tokens = line.Split(new[] { ' ', '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 3)
            {
                throw new ArgumentException("Invalid variable declaration: " + line);
            }

            string type = tokens[0];
            for (int i = 1; i < tokens.Length; i += 2)
            {
                string name = tokens[i];
                object value = ParseValue(tokens[i + 1], type);
                variables[name] = (type, value);
            }
        }

        private object ParseValue(string value, string type)
        {
            switch (type)
            {
                case "INT":
                    return int.Parse(value);
                case "CHAR":
                    return value.Trim('\'')[0];
                case "BOOL":
                    return value.Trim('"').ToUpper() == "TRUE";
                case "FLOAT":
                    return float.Parse(value);
                default:
                    throw new ArgumentException("Invalid variable type: " + type);
            }
        }

        private object EvaluateArithmeticExpression(string expression)
        {
            string[] tokens = Regex.Split(expression, @"(?<=[-+*/])|(?=[-+*/])");

            List<object> values = new List<object>();
            foreach (var token in tokens)
            {
                if (int.TryParse(token, out int intValue))
                {
                    values.Add(intValue);
                }
                else if (float.TryParse(token, out float floatValue))
                {
                    values.Add(floatValue);
                }
                else
                {
                    throw new FormatException("Invalid number format in expression: " + expression);
                }
            }

            float result = Convert.ToSingle(values[0]);
            for (int i = 1; i < values.Count; i += 2)
            {
                string op = (string)values[i];
                float nextValue = Convert.ToSingle(values[i + 1]);
                switch (op)
                {
                    case "+":
                        result += nextValue;
                        break;
                    case "-":
                        result -= nextValue;
                        break;
                    case "*":
                        result *= nextValue;
                        break;
                    case "/":
                        result /= nextValue;
                        break;
                    default:
                        throw new ArgumentException("Invalid operator: " + op);
                }
            }

            return result;
        }

        private string ProcessDisplayStatement(string statement)
        {
            StringBuilder result = new StringBuilder();
            string[] tokens = statement.Split('&');
            foreach (string token in tokens)
            {
                if (token.Trim().StartsWith("$"))
                {
                    result.AppendLine();
                }
                else if (token.Trim().StartsWith("[") || token.Trim().EndsWith("]"))
                {
                    continue;
                }
                else
                {
                    string trimmedToken = token.Trim();
                    if (variables.ContainsKey(trimmedToken))
                    {
                        var (_, value) = variables[trimmedToken];
                        if (value != null)
                        {
                            result.Append(value.ToString());
                        }
                        else
                        {
                            result.Append("NULL");
                        }
                    }
                    else if (IsArithmeticExpression(trimmedToken))
                    {
                        string[] parts = Regex.Split(trimmedToken, @"(?<=[-+*/])|(?=[-+*/])");
                        if (parts.Length == 3)
                        {
                            string operatorStr = parts[1];
                            object leftValue = GetVariableValue(parts[0]);
                            object rightValue = GetVariableValue(parts[2]);
                            object resultValue = PerformArithmeticOperation(operatorStr, leftValue, rightValue);
                            result.Append(resultValue.ToString());
                        }
                    }
                    else
                    {
                        result.Append(trimmedToken.Replace("\"", ""));
                    }
                }
            }
            return result.ToString();
        }

        private bool IsArithmeticExpression(string token)
        {
            return token.Contains('+') || token.Contains('-') || token.Contains('*') || token.Contains('/');
        }

        private object PerformArithmeticOperation(string operatorStr, object leftValue, object rightValue)
        {
            if (leftValue is int && rightValue is int)
            {
                int left = (int)leftValue;
                int right = (int)rightValue;

                switch (operatorStr)
                {
                    case "+": return left + right;
                    case "-": return left - right;
                    case "*": return left * right;
                    case "/": return left / right;
                    case "%": return left % right;
                    default: throw new ArgumentException("Invalid operator: " + operatorStr);
                }
            }
            else if (leftValue is float && rightValue is float)
            {
                float left = (float)leftValue;
                float right = (float)rightValue;

                switch (operatorStr)
                {
                    case "+": return left + right;
                    case "-": return left - right;
                    case "*": return left * right;
                    case "/": return left / right;
                    case "%": return left % right;
                    default: throw new ArgumentException("Invalid operator: " + operatorStr);
                }
            }
            else if (leftValue is int && rightValue is float)
            {
                int left = (int)leftValue;
                float right = (float)rightValue;

                switch (operatorStr)
                {
                    case "+": return left + right;
                    case "-": return left - right;
                    case "*": return left * right;
                    case "/": return left / right;
                    default: throw new ArgumentException("Invalid operator: " + operatorStr);
                }
            }
            else if (leftValue is float && rightValue is int)
            {
                float left = (float)leftValue;
                int right = (int)rightValue;

                switch (operatorStr)
                {
                    case "+": return left + right;
                    case "-": return left - right;
                    case "*": return left * right;
                    case "/": return left / right;
                    default: throw new ArgumentException("Invalid operator: " + operatorStr);
                }
            }
            else
            {
                throw new ArgumentException("Invalid operand types for arithmetic operation");
            }
        }

        private object GetVariableValue(string variableName)
        {
            variableName = variableName.Trim().Replace("(", "").Replace(")", "").Replace("\"", "").Replace("\'","");
            if (int.TryParse(variableName, out int intValue))
            {
                return intValue;
            }
            else if (variables.ContainsKey(variableName))
            {
                var (_, value) = variables[variableName];
                return value;
            }
            throw new ArgumentException("Variable not found: " + variableName);
        }

        private void RunInterpreter()
        {
            string code = CodeText.Text;
            ExecuteCode(code);
        }

        private void RunButton_MouseClick(object sender, MouseEventArgs e)
        {
            RunInterpreter();
        }
    }
}
