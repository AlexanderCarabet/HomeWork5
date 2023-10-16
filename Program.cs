
using System;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Collections.Generic;
// See https://aka.ms/new-console-template for more information


Console.WriteLine("Выберите, что вы хотите сделать с текстом из следующих вариантов: ");

  /*  bool keepRunning = true;

    while (keepRunning)
    {
       

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                // Код для первой задачи
                break;
            case "2":
                // Код для второй задачи
                break;
            case "3":
                // Код для третьей задачи
                break;
            case "4":
                // Код для четвертой задачи
                break;
            case "5":
                // Код для пятой задачи
                break;
            case "6":
                // Код для шестой задачи
                break;
            case "0":
                keepRunning = false; // Выход из программы
                break;
            default:
                Console.WriteLine("Неизвестная команда, пожалуйста, попробуйте снова.");
                break;
        }

        if (keepRunning)
        {
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey();
            Console.Clear(); // Очищает консольное окно
        }
    }

    Console.WriteLine("Программа завершила работу.");

*/

bool keepRunning = true;

while (keepRunning)
{

    Console.WriteLine("Меню:");
    Console.WriteLine("1 - если вы хотите найти слово с максимальным количеством цифр.");
    Console.WriteLine("2 - если вы хотите найти самое длинное слово и узнать, сколько раз это слово встречается в тексте.");
    Console.WriteLine("3 - если вы хотите заменить цифры от 0 до 9 на слова 'Ноль', 'один' и так далее.");
    Console.WriteLine("4 - показать предложения с восклицательным знаком! а затем с вопросительным знаком?");
    Console.WriteLine("5 - показать предложения, которые не содержат запятых.");
    Console.WriteLine("6 - Найти слова, которые начинаются и заканчиваются на одну и ту же букву.");
    Console.WriteLine("0 - выход из программы.");

    string input = Console.ReadLine();

         string text = "";
         string pdfPath = @"/Users/alexandrucarabetobodovski/Projects/Homework 5/filTest.pdf"; // Убедитесь, что путь указан правильно

         try
         {
             text = ExtractTextFromPdf(pdfPath);
         }
         catch (Exception ex)
         {
             Console.WriteLine($"Произошла ошибка: {ex.Message}");
             return; // avsluta om det finns ett fel under textextraktion
         }

         switch (input)
         {
             case "1":
                 FindWordWithMostDigits(text);
                 break;

             case "2":
                 FindAndCountLongestWord(text);
                 break;

             case "3":
                 string resultText = ReplaceDigitsWithWords(text);
                 Console.WriteLine("Текст после замены цифр словами:");
                 Console.WriteLine(resultText);
                 break;

             case "4":
                 DisplaySentencesWithSpecificMarks(text);
                 break;

             case "5":
                 DisplaySentencesWithoutCommas(text);
                 break;

             case "6":
                 DisplayWordsWithSameStartAndEnd(text);
                 break;


     default:
                 Console.WriteLine("Неверный ввод.");
                 break;
    }
            if (keepRunning)
                {
                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                    Console.ReadKey();
                    Console.Clear(); // Очищает консольное окно
                }
    }
    

    static string ExtractTextFromPdf(string pdfPath)
    {
        using (PdfReader reader = new PdfReader(pdfPath))
        {
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                string text = "";
                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); ++page)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string pageContent = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    text += pageContent;
                }
                return text;
            }
        }
    }

    static void FindWordWithMostDigits(string text)
    {
        // Найти слово с наибольшим количеством цифр
        string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t', ',', '.', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        string wordWithMostDigits = "";
        int maxDigitCount = 0;

        foreach (var word in words)
        {
            int digitCount = word.Count(char.IsDigit); // подсчитываем количество цифр в каждом слове

            if (digitCount > maxDigitCount)
            {
                maxDigitCount = digitCount;
                wordWithMostDigits = word;
            }
        }

        if (maxDigitCount > 0) // Если найдено слово с цифрами
        {
            Console.WriteLine($"Слово с наибольшим количеством цифр: {wordWithMostDigits}");
        }
        else
        {
            Console.WriteLine("В тексте не найдено слов с цифрами.");
        }
    }

    static void FindAndCountLongestWord(string text)
    {
        // Найти самое длинное слово и подсчитать, сколько раз оно встречается в тексте
        string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t', ',', '.', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        string longestWord = "";
        int maxLength = 0;

        foreach (var word in words)
        {
            if (word.Length > maxLength)
            {
                maxLength = word.Length;
                longestWord = word;
            }
        }

        int wordCount = words.Count(w => w == longestWord);

        Console.WriteLine($"Самое длинное слово: {longestWord}, количество раз в тексте: {wordCount}");
    }

    static string ReplaceDigitsWithWords(string text)
    {
        // Массив слов для замены цифр
        string[] russianNumbers = new string[] { "Ноль", "Один", "Два", "Три", "Четыре", "Пять", "Шесть", "Семь", "Восемь", "Девять" };

        // Заменяем каждую цифру её словесным представлением
        for (int i = 0; i < russianNumbers.Length; i++)
        {
            text = text.Replace(i.ToString(), russianNumbers[i]);
        }

        return text;
    }

static void DisplaySentencesWithSpecificMarks(string text)
{
    // Dela upp texten i meningar
    string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

    List<string> exclamationSentences = new List<string>();
    List<string> questionSentences = new List<string>();

    // Gå igenom varje mening och kontrollera om den slutar med ett utropstecken eller frågetecken
    foreach (var sentence in sentences)
    {
        string trimmedSentence = sentence.Trim();
        if (trimmedSentence.EndsWith("!"))
        {
            exclamationSentences.Add(trimmedSentence);
        }
        else if (trimmedSentence.EndsWith("?"))
        {
            questionSentences.Add(trimmedSentence);
        }
    }

    // Skriv ut meningar med utropstecken
    if (exclamationSentences.Count > 0)
    {
        Console.WriteLine("Предложения с восклицательным знаком:");
        foreach (var sentence in exclamationSentences)
        {
            Console.WriteLine(sentence + "!");
        }
    }
    else
    {
        Console.WriteLine("В тексте нет предложений с восклицательным знаком.");
    }

    // Skriv ut meningar med frågetecken
    if (questionSentences.Count > 0)
    {
        Console.WriteLine("\nПредложения с вопросительным знаком:");
        foreach (var sentence in questionSentences)
        {
            Console.WriteLine(sentence + "?");
        }
    }
    else
    {
        Console.WriteLine("В тексте нет предложений с вопросительным знаком.");
    }
}

static void DisplaySentencesWithoutCommas(string text)
{
    // Разделяем текст на предложения
    string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

    List<string> sentencesWithoutCommas = new List<string>();

    // Проверяем каждое предложение на отсутствие запятых
    foreach (var sentence in sentences)
    {
        if (!sentence.Contains(","))
        {
            sentencesWithoutCommas.Add(sentence.Trim()); // Добавляем предложения без запятых в список
        }
    }

    // Выводим предложения без запятых
    if (sentencesWithoutCommas.Count > 0)
    {
        Console.WriteLine("Предложения, которые не содержат запятых:");
        foreach (var sentence in sentencesWithoutCommas)
        {
            Console.WriteLine(sentence);
        }
    }
    else
    {
        Console.WriteLine("Все предложения содержат запятые.");
    }
    
}



static void DisplayWordsWithSameStartAndEnd(string text)
{
    // Удаляем знаки препинания и разделяем текст на слова
    string[] words = text.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

    List<string> matchingWords = new List<string>();

    // Проверяем каждое слово, чтобы увидеть, начинается и заканчивается ли оно на одну и ту же букву
    foreach (var word in words)
    {
        // Переводим слово в нижний регистр для последующего сравнения
        var lowerCaseWord = word.ToLower();
        if (lowerCaseWord[0] == lowerCaseWord[lowerCaseWord.Length - 1])
        {
            matchingWords.Add(word); // Добавляем слова, которые начинаются и заканчиваются на одну и ту же букву, в список
        }
    }

    // Выводим слова, которые начинаются и заканчиваются на одну и ту же букву
    if (matchingWords.Any())
    {
        Console.WriteLine("Слова, которые начинаются и заканчиваются на одну и ту же букву:");
        foreach (var matchingWord in matchingWords.Distinct()) // Используем Distinct(), чтобы исключить повторяющиеся слова
        {
            Console.WriteLine(matchingWord);
        }
    }
    else
    {
        Console.WriteLine("В тексте нет слов, которые начинаются и заканчиваются на одну и ту же букву.");
    }
}
