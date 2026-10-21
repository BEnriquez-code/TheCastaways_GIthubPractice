using System;
using System.Collections.Generic;
using System.Text;

namespace Finance_Tracker
{
    public class CIO
    {
        public bool PromptForBool(string prompt, string trueString, string falseString) {
            if (string.IsNullOrWhiteSpace(prompt) || string.IsNullOrWhiteSpace(trueString) || string.IsNullOrWhiteSpace(falseString)) throw new ArgumentException("prompt, trueString, or falseString cannot be null or just whitespace");
            string? input = null;
            bool isInvalid = true;

            do
            {
                Console.WriteLine(prompt);

                input = Console.ReadLine();

                isInvalid = input == null ||
                    !string.Equals(input, trueString, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(input, falseString, StringComparison.OrdinalIgnoreCase);

                if (isInvalid)
                {
                    Console.WriteLine("Invalid Input. Please try again.\n");
                }

            } while (isInvalid);

            return string.Equals(input, trueString, StringComparison.OrdinalIgnoreCase);
        }

        public string PromptForString(string prompt, bool allowEmpty) {
            if (string.IsNullOrWhiteSpace(prompt)) throw new ArgumentException("Prompt cannot be null or just whitespace");
            string? input = null;
            bool isInvalid = true;

            do
            {
                Console.WriteLine(prompt);

                input = Console.ReadLine();

                isInvalid = input == null || (string.IsNullOrWhiteSpace(input) && !allowEmpty);

                if (isInvalid)
                {
                    Console.WriteLine("Invalid Input. Please try again.\n");
                }

            } while (isInvalid);

            return input!;
        }

        public static int PromptForInt(string prompt, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(prompt) || min > max)
            {
                throw new ArgumentException("The prompt can't be null, empty, or just whitespace. Min can't be greater than Max");
            }

            string? input = null;
            bool isInvalid = true;
            int number = 0;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                isInvalid = !int.TryParse(input, out number) ||
                    number < min ||
                    number > max;

                if (isInvalid)
                {
                    Console.WriteLine("Invalid Input. Please try again.\n");
                }

            } while (isInvalid);

            return number;
        }

        public float PromptForFloat(string prompt, float min, float max) {
            if (string.IsNullOrWhiteSpace(prompt) || min > max) throw new ArgumentException("The prompt can't be null, empty, or just whitespace. Min can't be greater than Max");

            string? input = null;
            bool isInvalid = true;
            float number = 0;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                isInvalid = !float.TryParse(input, out number) ||
                    number < min ||
                    number > max;

                if (isInvalid)
                {
                    Console.WriteLine("Invalid Input. Please try again.\n");
                }

            } while (isInvalid);

            return number;
        }

        public static int PromptForMenuSelection(IEnumerable<string> options, bool withQuit = false)
        {
            if (options == null || !options.Any() && !withQuit)
            {
                throw new ArgumentException("Options can't be null or options can't be empty and withQuit is false");
            }

            int max = options.Count();
            int min = 1;
            string? optionsString = "";
            int i = 1;

            foreach (string option in options)
            {
                optionsString += $"{i++}. {option}\n";
            }

            if (withQuit)
            {
                optionsString += "\n0. Quit\n";
                min = 0;
            }

            return PromptForInt(optionsString, min, max);
        }
    }
}
