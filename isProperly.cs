///
/// Name: isProperly          
/// Date: 08.01.2021         
/// Description: Validate brackets in string
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "()(())(())(())(";

            bool answer = isProperly(str);

            if (answer) Console.WriteLine("String is Valid");
            else Console.WriteLine("String is Not Valid");

        }

        public static bool isProperly(String str)
        {
            var stack = new Stack<char>();
            foreach (var bracket in str)
            {
                switch (bracket)
                {
                    case '(':
                        stack.Push(bracket);
                        break;
                    case ')':
                        if ((stack.Count == 0) || (stack.Pop() != '('))
                            return false;
                        break;
                }

            }

            return stack.Count == 0;
        }

    }
}

