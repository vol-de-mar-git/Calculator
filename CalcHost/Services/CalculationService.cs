using System;
using System.Collections.Generic;
using System.Text;


namespace CalcHost
{
    public class CalculationService 
    {
        public List<string> Parsing(string expression)
        {
            List<string> resultExpression = new List<string>();
            StringBuilder number = new StringBuilder();
            foreach (var symbol in expression)
            {
                if (IsOperator(symbol.ToString()) || IsScob(symbol.ToString()))
                {
                    if (number.Length != 0)
                    {
                        resultExpression.Add(number.ToString());
                        number.Clear();
                    }

                    resultExpression.Add(symbol.ToString());
                }
                else
                {
                    if(number.Length < 10)
                        number.Append(symbol);
                    else
                    {
                        throw new ArithmeticException();
                    }
                }
            }
            if(number.Length != 0)
                resultExpression.Add(number.ToString());
            
            return resultExpression;
        }
        
        public Queue<string> ToPostfix(List<string> expression)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>();
            
            foreach (string c in expression)
            {
                if (IsNum(c))
                {
                    queue.Enqueue(c);
                }
                else
                {
                    if (IsOperator(c))
                    {
                        if (!stack.TryPeek(out string a) || a == "(")
                        {
                            stack.Push(c);
                        }
                        else
                        {
                            if (IsMulti(c))
                            {
                                stack.Push(c);
                            }
                            else
                            {
                                StackToQueue(stack,queue);
                                stack.Push(c);
                            }
                        }
                    }
                    switch (c)
                    {
                        case ")":
                        {
                            while(stack.Peek() != "(")
                            {
                                queue.Enqueue(stack.Pop());
                            }
                        
                            if(stack.Count != 0)
                                stack.Pop();
                            break;
                        }
                        case "(":
                            stack.Push(c);
                            break;
                    }
                }
            }
            foreach (var symbol in stack)
            {
                queue.Enqueue(symbol);
            }
            return queue;
        }

        public double Calculate(Queue<string> postfixExpression)
        {
            foreach (var symbol in postfixExpression)
            {
                if ("()".Contains(symbol))
                    throw new ArithmeticException();
            }
            
            Stack<string> stack = new Stack<string>();
            
            foreach (var part in postfixExpression)
            {
                if(IsNum(part))
                    stack.Push(part);
                if(IsOperator(part))
                {
                    double rightOperand = Double.Parse(stack.Pop());
                    double leftOperand = Double.Parse(stack.Pop());
                    double result = part switch
                    {
                        "+" => leftOperand + rightOperand,
                        "-" => leftOperand - rightOperand,
                        "*" => leftOperand * rightOperand,
                        "/" => leftOperand / rightOperand,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    stack.Push(result.ToString());
                }
                    
            }
            return Double.Parse(stack.Pop());
        }
        public bool IsOperator(string s)
        {
            return "+-*/".Contains(s);
        }
        public bool IsScob(string s)
        {
            return "()".Contains(s);
        }
        public bool IsMulti(string s)
        {
            return "*/".Contains(s);
        }
        public bool IsNum(string s)
        {
            return int.TryParse(s, out int result);
        }
        private void StackToQueue(Stack<string> a, Queue<string> b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if(IsMulti(a.Peek()) && a.Peek() != "(")
                    b.Enqueue(a.Pop());
                else
                {
                    return;
                }
            }
        }
    }
}