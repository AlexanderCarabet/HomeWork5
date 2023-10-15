using System;
using System.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


class Progr
{
    static void Main()
    {
        Console.WriteLine("Введите строку текста:");
        string input = Console.ReadLine();

        string[] words = input.Split(' ', '.', ',', '!', '?');

        int maxDigitCount = 0;
        string maxDigitWord = "";

        foreach (var word in words)
        {
            int digitCount = word.Count(char.IsDigit);

            if (digitCount > maxDigitCount)
            {
                maxDigitCount = digitCount;
                maxDigitWord = word;
            }
        }

        Console.WriteLine("Слово с максимальным количеством цифр: " + maxDigitWord);
    }
}

