///
/// Name: isProperly          
/// Date: 08.01.2021       
/// Description: Returns number of way to reach stairs
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int answer = 0;
            string n;

            Console.WriteLine("Number of ways:");
            n = Console.ReadLine();

            if (!string.IsNullOrEmpty(n))
            {
                if (int.Parse(n) <= 1)
                {
                    answer = 1;
                }
                else
                {
                    answer = (steps(int.Parse(n) - 1) + steps(int.Parse(n) - 2));
                }

                Console.WriteLine(answer);
            }
            else
            {
                Console.WriteLine("Wrong value");
            }

            Console.ReadLine();
        }


        static int steps(int n)
        {
            int s1 = 0, s3 = 0;
            int s2 = 1;

            for (int i = 0; i <= n; i++)
            {
                s3 = s1 + s2;
                s1 = s2;
                s2 = s3;
            }
            return s3;
        }
    }

}


