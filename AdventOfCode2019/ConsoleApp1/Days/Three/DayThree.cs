using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Days.Three
{
    public static class DayThree
    {
        public static int PartOne()
        {
            Grid grid = new Grid(Input.WireOne, Input.WireTwo);
            return grid.ClosestIntersectionDistance;
        }
    }
}
