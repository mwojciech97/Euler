using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler
{
    static class Tasks
    {
        public static int Problem1(int firstNumber, int secondNumber, int topNumber)
        {
            int score = 0;
            for (int i = firstNumber; i < topNumber; i++)
            {
                if (i % firstNumber == 0 || i % secondNumber == 0)
                    score += i;
            }
            return score;
        }
        public static long Problem2(int scope)
        {
            int a = 0, b = 1;
            long sum = 0;
            while (scope > b)
            {
                int tmp = a + b;
                a = b;
                b = tmp;
                if (tmp == 2) sum += 2;
                else if (tmp % 2 == 0) sum += tmp;
            }
            return sum;
        }
        public static long Problem3(long number)
        {
            List<int> dividers = new List<int>();
            for(int i = 2; i < number + 1; i++)
            {
                int count = 0;
                for(int j = 2; j < i + 1; j++)
                {
                    if (i % j == 0) count++;
                }
                if (count == 1)
                {
                    if(number % i == 0)
                    {
                        number /= i;
                        dividers.Add(i);
                    }
                }
            }
            dividers.Sort((x1, x2) => x2.CompareTo(x1));
            return dividers.First();
        }
        
    }
}
