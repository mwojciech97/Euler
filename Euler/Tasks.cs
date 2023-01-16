﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;

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
        public static string Problem4(int one, int two)
        {
            int sum, first = 0, second = 0, max = 0;
            char[] charSum;
            string reversedSum;
            for (int i = two; i > one - 1; i--)
            {
                for (int j = two; j > one - 1; j--)
                {
                    sum = i * j;
                    charSum = sum.ToString().ToCharArray();
                    Array.Reverse(charSum);
                    reversedSum = new string(charSum);
                    if (sum < max) continue;
                    if (!sum.ToString().Equals(reversedSum)) continue;
                    if (sum > max)
                    {
                        max = sum;
                        first = i;
                        second = j;
                    }
                }

            }
            return "Palindrome:" + max + " given from numbers: " + first + "*" + second;
        }
        public static int Problem5(int one, int two)
        {
            int number = 0, sum = 0;
            while(sum != (two - one))
            {
                number+=two;
                sum = 0;
                for (int i = one; i < two; i++)
                {
                    if (!(number % i == 0)) break;
                    sum++;
                }
            }
            return number;
        }
        public static int Problem6(int one, int two)
        {
            int sum = 0, squaredSum = 0;
            for (int i = one; i < two + 1; i++)
            {
                sum += i;
                squaredSum += i*i;
            }
            return sum*sum - squaredSum;
        }
        public static int Problem7(int number)
        {
            int val = 5, check;
            List<int> primes = new List<int>();
            primes.Add(2);
            primes.Add(3);
            while (primes.Count() < number)
            {
                check = 0;
                foreach (int prime in primes)
                {
                    if (val % prime == 0) break;
                    check++;
                }
                if(check == primes.Count()) primes.Add(val);
                val+=2;
            }
            return primes.Last();
        }
        public static long Problem8(int digits)
        {
            string text = File.ReadAllText("D:\\Nauka\\1000DigitNumber.txt");
            char[] chars = text.ToCharArray();
            int[] numbers = chars.Select(i => Int32.Parse(i.ToString())).ToArray();
            long sum, temp = 0;
            for (int i = 0; i < numbers.Length - digits + 1; i++)
            {
                sum = 1;
                for (int j = i; j < digits + i; j++)
                {
                    sum *= numbers[j];
                    if (sum > temp) temp = sum;
                }
            }
            return temp;
        }
        public static string Problem9(int sum)
        {
            int a = 0, b = 0, c = 0, val;
            for (int i = 1; i < sum/2; i++)
            {
                for (int j = 1; j < sum/2; j++)
                {
                    if (i + j + Math.Sqrt(i * i + j * j) == sum)
                    {
                        a = i;
                        b = j;
                        c = sum - i - j;
                        break;
                    }
                }
                if (a == i) break;
            }
            val = a * b * c;
            return $"A: {a}, B: {b}, C: {c}, val: {val}";
        }
        public static long Problem10(int number)
        {
            List<int> primes = new List<int>();
            long sum = 0;
            int check;
            primes.Add(2);
            primes.Add(3);
            for (int i = 5; i < number; i+=2)
            {
                check = 0;
                for (int j = 0; j < primes.Count(); j++)
                {
                    if (i % primes[j] == 0)
                    {
                        check++;
                        break;
                    }
                    
                }
                if(check == 0) primes.Add(i);
            }
            primes.ForEach(prime => sum += prime);
            return sum;
        }
        public static int Problem11(int number)
        {
            string grid = File.ReadAllText("D:\\Nauka\\20x20gridProblem11.txt");
            string[] grids = grid.Split(' ');
            int[] numbers = grids.Select(x => Int32.Parse(x)).ToArray();
            int left = 1, down = 1, diagonalDown = 1, diagonalUp = 1;
            CountRight();
            CountDown();
            CountDiagonalDown();
            CountDiagonalUp();
            int[] max = new int[] { left, down, diagonalDown, diagonalUp };
            return max.Max();
            void CountRight()
            {
                int first = 0, tempR;
                for (int i = 0; i < numbers.Count() - number + 1; i++)
                {
                    if (i < number) left *= numbers[i];
                    if (i % 20 == 0)
                    {
                        i += 3;
                        tempR = numbers[i] * numbers[i - 1] * numbers[i - 2] * numbers[i - 3];
                        if (tempR > left) left = tempR;
                        first += 4;
                        continue;
                    }
                    if(numbers[first] == 0)
                    {
                        first++;
                        continue;
                    }
                    tempR = left / numbers[first] * numbers[i];
                    if (tempR > left) left = tempR;
                    first++;
                }
            }
            void CountDown()
            {
                int tempD;
                for (int i = 0; i < numbers.Count() - 60; i++)
                {
                    tempD = numbers[i] * numbers[i + 20] * numbers[i + 40] * numbers[i + 60];
                    if(tempD > down) down = tempD;
                }
            }
            void CountDiagonalDown()
            {
                int tempDD;
                for (int i = 0; i < numbers.Count() - 63; i++)
                {
                    if (i % 17 == 0) i+=3;
                    tempDD = numbers[i] * numbers[i + 21] * numbers[i + 42] * numbers[i + 63];
                    if(tempDD > diagonalDown) diagonalDown = tempDD;
                }
            }
            void CountDiagonalUp()
            {
                int tempDU;
                for (int i = 3; i < numbers.Count() - 60; i++)
                {
                    if (i % 20 == 0) i += 3;
                    tempDU = numbers[i] * numbers[i + 19] * numbers[i + 38] * numbers[i + 57];
                    if(tempDU > diagonalUp) diagonalUp = tempDU;
                }
            }
            
        }
        public static int Problem12(int numberOfDividers)
        {
            int dividersCount, number = 0, count = 0;
            do
            {
                dividersCount = 2;
                count++;
                number += count;
                for (int i = 2; i < number / 2 + 1; i++)
                {
                    if (number % i == 0) dividersCount++;
                }
            } while (dividersCount <= numberOfDividers);
            return number;
        }
        public static string Problem13(int digits)
        {
            BigInteger sum = 0;
            string[] text = File.ReadAllLines("D:\\Nauka\\Problem13.txt");
            BigInteger[] numbers = text.Select(t => BigInteger.Parse(t)).ToArray();
            foreach (BigInteger num in numbers)
            {
                sum += num;
            }
            char[] charNumbers = sum.ToString().ToCharArray();
            char[] first = charNumbers.Take(digits).ToArray();
            string firstDigits = new string(first);
            return $"Sum of those values is {sum}. First {digits} digits are {firstDigits}";
        }
        public static string Problem14(int max)
        {
            int longestChain = 0, chain, number = 0;
            for (int i = 2; i < max; i++)
            {
                chain = 1;
                longestChain = Counting(i);
                if (longestChain == chain) number = i;
            }
            return $"The longest chain is {longestChain} for number {number}";
            int Counting(long val)
            {
                if (val == 1)
                {
                    if (chain > longestChain) return chain;
                    return longestChain;
                }
                else if (val % 2 == 0)
                {
                    chain++;
                    val /= 2;
                    return Counting(val);
                }
                else
                {
                    chain++;
                    val = val * 3 + 1;
                    return Counting(val);
                }
            }
        }
        public static long Problem15(int grid)
        {
            long sumU = 1, sumD = 1, score = 0;
            for (long i = 1; i < grid + 1; i++)
            {
                sumD *= i;
            }
            for (long i = grid + 1; i < (2 * grid) + 1; i++)
            {
                if (sumD % i == 0) sumD /= i;
                else sumU *= i;
            }
            if(sumD != 0) score = sumU / sumD;
            return score;
        }
        public static BigInteger Problem16(int pow)
        {
            BigInteger sum = 2, digitsSum = 0;
            for (int i = 2; i < pow + 1; i++)
            {
                sum *= 2;
            }
            char[] digits = sum.ToString().ToCharArray();
            int[] ints = digits.Select(i => Int32.Parse(i.ToString())).ToArray();
            foreach (int i in ints)
            {
                digitsSum += i;
            }
            return digitsSum;
        }
        public static int Problem17(int val)
        {
            int sum = 0;
            Dictionary<int, int> numbers = new Dictionary<int, int>() {
                { 1, 3 }, { 2, 3 }, { 3, 5 }, { 4, 4 }, { 5, 4 }, { 6, 3 }, { 7, 5 }, { 8, 5 }, { 9, 4 },
                { 10, 3 }, { 11, 6 }, { 12, 6 }, { 13, 8 }, { 14, 8 }, { 15, 7 },
                { 16, 7 }, { 17, 9 }, { 18, 8 }, { 19, 8}, { 20, 6 }, { 30, 6 }, { 40, 5 },
                { 50, 5 }, { 60, 5 }, { 70, 7 }, { 80, 6 }, { 90, 6 }
            };
            for (int i = 1; i < val + 1; i++)
            {
                if (numbers.ContainsKey(i)) sum += numbers.First(x => x.Key.Equals(i)).Value;
                else if (i < 100)
                {
                    AddToSum(RoundTens(i), i - RoundTens(i));
                }
                else if (i < 1000)
                {
                    int temp1 = i / 100;
                    sum += numbers.First(x => x.Key.Equals(temp1)).Value + 7;
                    if (i % (temp1 * 100) != 0)
                    {
                        int temp2 = i - (temp1 * 100);
                        if (!numbers.ContainsKey(temp2))
                        {
                            AddToSum(RoundTens(temp2), temp2 - RoundTens(temp2));
                            sum += 3;
                            continue;
                        }
                        sum += numbers.First(x => x.Key.Equals(temp2)).Value + 3;
                        continue;
                    }
                    else continue;
                }
                else if (i == 1000)
                {
                    sum += 11;
                    break;
                }
            }
            return sum;
            void AddToSum(int number1, int number2)
            {
                sum += numbers.First(x => x.Key.Equals(number1)).Value;
                sum += numbers.First(x => x.Key.Equals(number2)).Value;
            }
            int RoundTens(int number) { return number / 10 * 10; }
        }
    }
}
