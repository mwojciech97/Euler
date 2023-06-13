using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;

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
        public static int Problem18and67(string path)
        {
            string[][] test = File.ReadLines(path).Select(x => x.Split(' ')).ToArray();
            int[][] numbers = new int[test.Length][];
            numbers = test.Select(x => x.Select(y => int.Parse(y)).ToArray()).ToArray();
            for (int i = numbers.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < numbers[i].Length; j++)
                {
                    if (numbers[i + 1][j] > numbers[i + 1][j + 1]) numbers[i][j] += numbers[i + 1][j];
                    else numbers[i][j] += numbers[i + 1][j + 1];
                }
            }
            return numbers[0][0];
        }
        public static int Problem19(int dayOfYear, int dayOfWeek, int year, int startYear, int endYear)
        {
            int currentMonth = 1, currentDay = dayOfYear, counter = 0, currentYear = year;
            if (currentDay != 7)
            {
                currentDay += 7 - dayOfWeek;
                dayOfWeek = 7;
            }
            while(currentYear < endYear + 1)
            {
                switch (currentMonth)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                        if (currentDay > 31) 
                        {
                            currentDay -= 31;
                            currentMonth += 1;
                            if(currentDay == 1 && currentYear >= startYear) counter++;
                        }
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        if (currentDay > 30)
                        {
                            currentDay -= 30;
                            currentMonth += 1;
                            if (currentDay == 1 && currentYear >= startYear) counter++;
                        }
                        break;
                    case 2:
                        if ((currentYear % 4 == 0 && currentYear % 100 != 0) || (currentYear % 4 == 0 && currentYear % 400 == 0))
                        {
                            if (currentDay > 29)
                            {
                                currentDay -= 29;
                                currentMonth += 1;
                                if (currentDay == 1 && currentYear >= startYear) counter++;
                                break;
                            }
                            break;
                        }
                        if (currentDay > 28)
                        {
                            currentDay -= 28;
                            currentMonth += 1;
                            if (currentDay == 1 && currentYear >= startYear) counter++;
                        }
                        break;
                    case 12:
                        if (currentDay > 31)
                        {
                            currentDay -= 31;
                            currentMonth = 1;
                            currentYear++;
                            if (currentDay == 1 && currentYear >= startYear && currentYear < endYear + 1) counter++;
                        }
                        break;
                }
                currentDay += 7;
            }
            return counter;
        }
        public static int Problem20(int endNumber)
        {
            BigInteger val = 1;
            int sum = 0;
            for (int i = 2; i < endNumber + 1; i++)
            {
                val *= i;
            }
            char[] digits = val.ToString().ToCharArray();
            digits.ToList().ForEach(i => sum += int.Parse(i.ToString()));
            return sum;
        }
        public static int Problem21(int endNumber)
        {
            int sum = 0;
            List<int> numbers = new List<int>();
            List<int> amicableNumbers = new List<int>();
            numbers = Enumerable.Range(1, endNumber).ToList();
            foreach (int numb in numbers)
            {
                int sumA = 0, sumB = 0;
                if (amicableNumbers.Contains(numb)) continue;
                for (int i = 1; i < numb / 2 + 1; i++)
                {
                    if (numb % i == 0) sumA += i;
                }
                for (int i = 1; i < sumA / 2 + 1; i++)
                {
                    if (sumA % i == 0) sumB += i;
                }
                if(sumB == numb && numb != sumA)
                {
                    amicableNumbers.Add(numb);
                    amicableNumbers.Add(sumA);
                }
            }
            amicableNumbers.ForEach(i => sum += i);
            return sum;
        }
        public static long Problem22(string path)
        {
            string content = "";
            int position = 0;
            long sum = 0;
            List<string> names = new List<string>();
            Dictionary<char, int> values= new Dictionary<char, int>()
            {
                { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 },
                { 'H', 8 }, { 'I', 9 }, { 'J', 10 }, { 'K', 11 }, { 'L', 12 }, { 'M', 13 }, { 'N', 14 },
                { 'O', 15 }, { 'P', 16 }, { 'Q', 17 }, { 'R', 18 }, { 'S', 19 }, { 'T', 20 }, { 'U', 21 },
                { 'V', 22 }, { 'W', 23 }, { 'X', 24 }, { 'Y', 25 }, { 'Z', 26 }
            };
            if (File.Exists(path)) content = File.ReadAllText(path).Replace("\"", "");
            else Console.WriteLine("File does not exist");
            names = content.Split(",").ToList();
            names.Sort();
            foreach (string name in names)
            {
                position++;
                int value = 0;
                char[] chars = name.ToCharArray();
                foreach (char c in chars)
                {
                    if (values.ContainsKey(c))
                    {
                        value += values.First(v => v.Key.Equals(c)).Value;
                    }
                }
                sum += value * position;
            }
            return sum;
        }
        public static int Problem23(int maxNumber)
        {
            int score = 0, cPosition = 0, sum;
            List<int> numbers = new List<int>();
            List<int> abundantNumbers = new List<int>();
            numbers = Enumerable.Range(1, maxNumber).ToList();
            foreach (int numb in numbers)
            {
                sum = 0;
                for (int i = 1; i < numb / 2 + 1; i++)
                {
                    if (numb % i == 0) sum += i;
                }
                if(sum > numb) abundantNumbers.Add(numb);
            }
            foreach (int anumb1 in abundantNumbers)
            {
                for (int i = cPosition; i < abundantNumbers.Count; i++)
                {
                    if (numbers.Contains(anumb1 + abundantNumbers[i])) numbers.Remove(anumb1 + abundantNumbers[i]);
                }
                cPosition++;
            }
            numbers.ForEach(i => score += i);
            return score;
        }
        /// <summary>
        /// Created before I knew any mathematical concepts about permutations
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="placement"></param>
        /// <returns></returns>
        public static string Problem24(int digits, int placement)
        {
            List<string> numbers = new List<string>();
            numbers.Add("0");
            string temp = "";
            int currentNumber = 0;
            while (numbers[currentNumber].Length < digits + 1)
            {
                for (int i = 0; i < numbers[currentNumber].Length + 1; i++)
                {
                    temp = numbers[currentNumber].Insert(i, numbers[currentNumber].Length.ToString());
                    numbers.Add(temp);
                }
                currentNumber++;
            }
            numbers = numbers.FindAll(x => x.Length.Equals(digits));
            numbers.Sort();
            return numbers[placement - 1];
            
        }
        /// <summary>
        /// Created after I learned factoradic representation
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="placement"></param>
        /// <returns></returns>
        public static string Problem24a(int digits, int placement)
        {
            List<int> factorialNumbers = new List<int>();
            placement = placement - 1;
            string firstNumber = "";
            string answer = "";
            FirstNumber();
            int digitFactorial;
            for(int j = digits - 1; j > 0; j--)
            {
                Factorial(j);
                for (int i = 0; i < digits; i++)
                {
                   // if (placement < 0) break;
                    if (digitFactorial * i > placement)
                    {
                        factorialNumbers.Add(i - 1);
                        placement = placement - (digitFactorial * (i - 1));
                        break;
                    }
                }
            }
            for(int i = 0; i < factorialNumbers.Count(); i++)
            {
                answer += firstNumber.ElementAt(factorialNumbers[i]);
                firstNumber = firstNumber.Remove(factorialNumbers[i], 1);
            }
            answer += firstNumber.ElementAt(0);
            return answer;
            void FirstNumber()
            {
                for (int i = 0; i < digits; i++)
                    firstNumber += i.ToString();
            }
            void Factorial(int x)
            {
                int a = 1;
                for(int i = 1; i < x + 1; i++)
                {
                    a *= i;
                }
                digitFactorial = a;
            }
        }
        public static int Problem25(int digits)
        {
            int index = 0, count = 0;
            List<BigInteger> fib = new List<BigInteger>();
            fib.Add(1);
            fib.Add(1);
            while (count < digits)
            {
                index = fib.Count();
                fib.Add(fib[index - 1] + fib[index - 2]);
                count = fib[fib.Count - 1].ToString().ToCharArray().Length;
            }
            return index + 1;
        }
        public static int Problem26(int digits)
        {
            List<int> dividers = new List<int>();
            Dictionary<int,int> answer = new Dictionary<int,int>();
            int restFromDivison;
            for (int i = 2; i < digits + 1; i++)
            {
                restFromDivison = 1 % i;
                dividers.Add(restFromDivison * 10);
                while (true)
                {
                    restFromDivison *= 10;
                    restFromDivison = restFromDivison % i;
                    if (restFromDivison == 0) break;
                    if (!dividers.Contains(restFromDivison * 10))
                    {
                        dividers.Add(restFromDivison * 10);
                        continue;
                    }
                    else
                    {
                        answer.Add(i, dividers.Count());
                        break;
                    }
                }
                dividers.Clear();
            }
            return answer.Where(x => x.Value == answer.Max(y => y.Value)).Select(x => x.Key).First();
        }
        public static string Problem27(int a, int b)
        {
            int coA = a * (-1) - 1, coB = b * (-1) - 1, coN = 0, n, score;
            bool isPrime;
            for(int i = a * (-1); i < a; i++)
            {
                for(int j = b * (-1); j < b + 1; j++)
                {
                    n = 0;
                    while (true)
                    {
                        isPrime = true;
                        score = n * n + i * n + j;
                        if (score < 0) break;
                        for(int d = 2; d < Math.Sqrt(score) + 1; d++)
                        {
                            if (score % d == 0)
                            {
                                isPrime = false;
                                break;
                            }
                        }
                        if(!isPrime)
                            break;
                        n++;
                    }
                    if(n > coN)
                    {
                        coA = i;
                        coB = j;
                        coN = n;
                    }
                }
            }
            return "A: " + coA + ". B: " + coB + ". Number of primes: " + coN + ". Answer: " + coA * coB;
        }
        public static long Problem28(int dim)
        {
            int numb = 1, diagonal = 2, counter = 0;
            long score = 0;
            while (numb <= dim * dim)
            {
                if (counter % 4 == 0 && counter != 0)
                {
                    diagonal += 2;
                    counter = 0;
                }
                score += numb;
                numb += diagonal;
                counter++;
            }
            return score;
        }
        public static int Problem29(double a, double b)
        {
            List<double> distinctNumbers = new List<double>();
            for(double i = 2; i < a + 1; i++)
            {
                for(double j = 2; j < b + 1; j++)
                {
                    double score = Math.Pow(i, j);
                    if (!distinctNumbers.Contains(score)) distinctNumbers.Add(score);
                    else continue;
                }
            }
            return distinctNumbers.Count();
        }
        public static double Problem30(int pow)
        {
            double power = Math.Pow(9, pow);
            double maxNumber = 0;
            int counter = 2;
            double charScore, score = 0;
            char[] chars;
            while (maxNumber.ToString().Length != counter)
            {
                maxNumber = power * counter;
                counter++;
            }
            for (int i = 10; i < maxNumber + 1; i++)
            {
                chars = i.ToString().ToCharArray();
                Array.Sort(chars);
                charScore = 0;
                for(int j = chars.Length - 1; j > -1; j--)
                {
                    if (chars[j] == '0') break;
                    if (charScore > i) break;
                    charScore += Math.Pow(int.Parse(chars[j].ToString()), pow);
                }
                if (charScore == i) score += i;
            }
            return score;
        }
    }
}
