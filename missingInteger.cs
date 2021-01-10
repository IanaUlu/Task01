///
/// Name: isPolindrome          
/// Date: 08.01.2021         
/// Description: Finding missing integer in the array greater than 0
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] NumArray = { 9, 7, 5, 1, 2, 9, 4, 0, -76, -5 };
            checkNum(NumArray);
        }

        static int checkNum(int[] arr)
        {

            int minNum = 1;

            // Sorting array, select only positive numbers and remove dublicates
            arr = arr.Where(entry => entry > 0).Distinct().OrderBy(ii => ii).ToArray();

            while (arr.Contains(minNum))
            {
                minNum++;
            }
            return minNum;
           

        }
    }

}





