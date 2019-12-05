using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Days.Four
{
    public static class DayFour
    {
        public static int PartOne()
        {
            List<int> validNumbers = new List<int>();

            for(int i = Input.MinRange; i <= Input.MaxRange; i++)
            {
                if(TwoAdjacentDigitsAreTheSame(i) && DigitsNeverDecrease(i))
                {
                    validNumbers.Add(i);
                }
            }
            return validNumbers.Count;
        }

        public static int PartTwo()
        {
            List<int> validNumbers = new List<int>();

            for (int i = Input.MinRange; i <= Input.MaxRange; i++)
            {
                if (TwoAdjacentDigitsAreTheSame(i) && DigitsNeverDecrease(i) && MatchingAdjacentNumbersHasAtLeastOnePair(i))
                {
                    validNumbers.Add(i);
                }
            }
            return validNumbers.Count;
        }

        private static bool TwoAdjacentDigitsAreTheSame(int input)
        {
            string inputStr = input.ToString();

            for(int i = 0; i < inputStr.Length - 1; i++)
            {
                if(inputStr[i] == inputStr[i+1])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DigitsNeverDecrease(int input)
        {
            string inputStr = input.ToString();

            for(int i = 0; i < inputStr.Length - 1; i++)
            {
                if(Int32.Parse(inputStr[i].ToString()) > Int32.Parse(inputStr[i+1].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool MatchingAdjacentNumbersHasAtLeastOnePair(int input)
        {
            var matches = Regex.Matches(input.ToString(), @"(.)\1+");

            foreach(var match in matches)
            {
                if((double)match.ToString().Length / (double)2 == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
