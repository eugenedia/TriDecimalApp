using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TriDecimal
{ // итоговая формула будет = (количество чисел с суммой цифр 1 * количество чисел с суммой цифр 1)*13 + (количество чисел с суммой цифр 2 * количество чисел с суммой цифр 2)*13 и т.д. до 12*6=72
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> numbersDictionary;

            numbersDictionary = GenerateSixDigitNumberDictionary();

            long countBeautifulNumbers =  CountBeautifulNumbers(numbersDictionary);
            Console.WriteLine(countBeautifulNumbers);
        }


        // В массиве храним числа от 0 до 12
        // Массив из шести позиций
        private static Dictionary<int, int> GenerateSixDigitNumberDictionary()
        {
            int[] numbers = new int[6];

            int number = 0;

            //В словаре храним - key сумма цифр, value - количество таких чисел
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            int maxNumber = MaxNumberInTriDecimalSystem(6);


            while (number <= maxNumber)
            {
                Array.Clear(numbers, 0, numbers.Length);

                ConvertToTriDecimalSystem(number, numbers);

                //Сумма цифр
                int sumOfDigits = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    sumOfDigits += numbers[i];
                }

                if (dictionary.ContainsKey(sumOfDigits))
                    dictionary[sumOfDigits]++;
                else
                    dictionary.Add(sumOfDigits, 1);

                number++;
            }

            return dictionary;

        }

        private static long CountBeautifulNumbers(Dictionary<int, int> numbersDictionary) 
        {
            long result = 0;
            
            foreach(var elem in numbersDictionary)
            {
                result = result + (long)elem.Value * (long)elem.Value * 13;
            }

            return result;
        }

        /// <summary>
        /// Перевод числа в тринадцатиричную систему
        /// </summary>
        private static void ConvertToTriDecimalSystem(int number, int[] numbers)
        {
            int divisible = number;
            int i = 5;
            int reminder = 0;

            while (divisible >= 13 && i > 0)
            {
                reminder = divisible % 13;
                numbers[i--] = reminder;
                divisible = divisible / 13;
            }
            numbers[i] = divisible;
        }


        private static int IntPow(int number, int pow)
        {
            int result = 1;
            while(pow > 0)
            {
                result*= number;
                pow--;
            }
            return result;
        }


        //Максимальное число в тринадцатиричной системе счисления в зависимости от количества позиций.
        //Формула подсчета максимального числа 12*13^5 + 12*13^4 + 12*13^3 + 12*13^2 + 12*13^1 + 12*13^0 = 4826808
        static int MaxNumberInTriDecimalSystem(int positions)
        {
            int result = 0;
            positions--;

            while (positions >= 0)
            {
                //checked для проверки переполнения
                result = checked(result + 12 * IntPow(13, positions--));
            }

            return result;
        }

    }
}
