using System.Collections.Generic;

namespace ConsoleApp1.Days.One
{
    public static class DayOne
    {
        public static int PartOne()
        {
            List<int> testInput = Input.TestInput;

            int totalFuelRequired = 0;

            foreach(int input in testInput)
            {
                int fuelRequired = input / 3 - 2;
                totalFuelRequired += fuelRequired;
            }
            return totalFuelRequired;
        }

        public static int PartTwo()
        {
            List<int> testInput = Input.TestInput;

            int totalFuelRequired = 0;

            foreach(int input in testInput)
            {
                int dividedByThree = input / 3 - 2;
                int remainder = 0;
                
                while(dividedByThree >= 0)
                {
                    remainder += dividedByThree;
                    dividedByThree = dividedByThree / 3 - 2;
                }
                totalFuelRequired += remainder;
            }
            return totalFuelRequired;
        }
    }
}
