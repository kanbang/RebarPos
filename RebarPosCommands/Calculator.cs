using System;
using System.Collections.Generic;
using System.Text;

namespace RebarPosCommands
{
    // Example usage
    // string infix = "( ( ( -1 + 3 ) / 4 ) * -10 )";
    // double value = Calculator.Evaluate(infix);
    // string postfix = Calculator.InfixToPostfix(infix);
    // double value = Calculator.PostfixValue(postfix);
    class Calculator
    {
        public static bool IsValid(string str)
        {
            string ops = "(*/-+";
            string nums = "1234567890.";
            str = str.Trim(ops.ToCharArray());
            str.Trim(nums.ToCharArray());
            return (str.Length == 0);
        }

        private static bool IsGreaterPrecedence(char a, char b)
        {
            string ops = "(*/-+";
            return (ops.IndexOf(a) <= ops.IndexOf(b));
        }

        private static bool IsOperator(char op)
        {
            string ops = "(*/-+";
            return (ops.IndexOf(op) != -1);
        }


        public static double Evaluate(string expression)
        {
            return PostfixValue(InfixToPostfix(expression));
        }

        public static string InfixToPostfix(string infix)
        {
            StringBuilder postfix = new StringBuilder();
            Stack<char> ops = new Stack<char>();
            //loop through all operands and operators
            foreach (char token in infix)
            {
                if (token == ')')
                {
                    // pop operator stack all the way back until we find the corrisponding opening operator
                    char val;
                    while ((val = ops.Peek()) != '(')
                    {
                        postfix.Append(val);
                        postfix.Append(' ');
                        ops.Pop();
                    }
                    ops.Pop(); //pop the opening operator off the stack
                }
                else if (IsOperator(token))
                {
                    // While the operator is of lesser precedence than the one beneath it, pop until it's not
                    while (ops.Count != 0 && IsGreaterPrecedence(ops.Peek(), token) && ops.Peek() != '(')
                    {
                        postfix.Append(ops.Peek());
                        postfix.Append(' ');
                        ops.Pop();
                    }
                    ops.Push(token); //add the operator to the stack
                }
                else
                {
                    //if it's not an operator, then just append it to the output string
                    postfix.Append(token);
                    postfix.Append(' ');
                }
            }

            // append everything else left on the stack
            while (ops.Count != 0)
            {
                postfix.Append(ops.Peek());
                postfix.Append(' ');
                ops.Pop();
            }
            return postfix.ToString();
        }

        public static double PostfixValue(string postfix)
        {
            Stack<double> vals = new Stack<double>();
            foreach (char token in postfix)
            {
                // if it's an operator, then evaluate it with the two most 
                // top operands on the stack and push the result on the stack
                if (IsOperator(token))
                {
                    double b = vals.Pop();
                    double a = vals.Pop();
                    switch (token)
                    {
                        case '*': vals.Push(a * b); break;
                        case '/': vals.Push(a / b); break;
                        case '+': vals.Push(a + b); break;
                        case '-': vals.Push(a - b); break;
                    }
                }
                else
                {
                    // push operand to top of stack
                    vals.Push(double.Parse(token.ToString()));
                }
            }

            // final value is the last value on the stack
            return vals.Peek();
        }
    }
}
