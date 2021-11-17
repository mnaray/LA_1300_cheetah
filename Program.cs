using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace voci_bot
{
    class Program
    {
        static int countCycles = 0;
        static int countCyclesLearnBlock = 0;
        static int hearts = 3;
        static int wordRight = 0;
        static int wordWrong = 0;
        static string[] german = CsvToArray().Take(CsvToArray().Length / 2).ToArray();
        static string[] foreignLanguage = CsvToArray().Skip(CsvToArray().Length / 2).ToArray();
        static void Main(string[] args)
        {
            while (true)
            {
                if (!(hearts == 0))
                {
                    CreateHighScore();
                }
                else
                {
                    Console.WriteLine("Sie haben keine Herzen mehr.");
                    Continue();
                    hearts = 3;
                }
            }
        }
        public static void CheckWords(string[] correctWord)
        {
            Console.WriteLine("Geben Sie das Wort für " + german[countCycles] + " ein.");
            string input = Console.ReadLine();
            if (input == correctWord[countCycles])
            {
                countCycles++;
                countCyclesLearnBlock++;
                wordRight++;
            }
            else
            {
                Console.WriteLine("Falsche Eingabe");
                countCyclesLearnBlock++;
                wordWrong++;
                hearts--;
            }
        }
        public static int CreateHighScore()
        {
            int highscore = 0;
            try
            {
                LearnBlockUtils();
                CheckWords(foreignLanguage);
            }
            catch
            {
                if (highscore > 0)
                {
                    highscore--;
                }
            }
            return highscore;
        }
        public static string[] CsvToArray()
        {
            string inPath = @"..\..\..\example.csv";
            string text = File.ReadAllText(inPath);

            string[] lines = text.Split("\r\n");
            int words = lines.Length;
            string[] german = new string[words];
            string[] foreignLanguage = new string[words];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] items = lines[i].Split(", ");
                german[i] = items[0];
                foreignLanguage[i] = items[1];
            }
            List<string> joinedList = new List<string>();
            joinedList.AddRange(german);
            joinedList.AddRange(foreignLanguage);
            string[] z = joinedList.ToArray();
            return z;
        }
        public static void LearnBlockUtils()
        {
            if (countCyclesLearnBlock == 10)
            {
                countCyclesLearnBlock = 0;
                double subtraction = 10 - wordRight;
                double result = subtraction * 100 / 10;
                Console.WriteLine("Sie haben " + wordRight + " Wörter richtig und " + wordWrong + " falsch gelernt.");
                Console.WriteLine("Sie haben " + result + "% des Lernblocks falsch ");
                Console.WriteLine("--------------------------------------------------");
                Continue();
            }
        }
        public static void Continue()
        {
            Console.WriteLine("Möchten Sie weiterlernen? y/n");
            if (Console.ReadLine() == "n")
            {
                Environment.Exit(0);
            }
        }
    }
}


