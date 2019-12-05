using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Days.Three
{
    public class Grid
    {
        private List<string> WireOnePoints;
        private List<string> WireTwoPoints;
        private List<Coordinate> WireOneCoordinates = new List<Coordinate>();
        private List<Coordinate> WireTwoCoordinates = new List<Coordinate>();

        public int ClosestIntersectionDistance { get; private set; }
        public int IntersectionWithLeastSteps { get; private set; }
        public Grid(List<string> wireOnePoints, List<string> wireTwoPoints)
        {
            WireOnePoints = wireOnePoints;
            WireTwoPoints = wireTwoPoints;

            Build();
            DetermineClosestIntersectionDistance();
            DetermineIntersectionWithLeastSteps();
         }

        private void Build()
        {
            Coordinate wireOnePosition = new Coordinate(0, 0);
            Coordinate wireTwoPosition = new Coordinate(0, 0);

            foreach (string point in WireOnePoints)
            {
                MoveWirePosition(wireOnePosition, point, true);
            }

            foreach(string point in WireTwoPoints)
            {
                MoveWirePosition(wireTwoPosition, point, false);
            }
        }

        private void MoveWirePosition(Coordinate wirePosition, string wirePoint, bool isWireOne)
        {
            string direction = wirePoint.Substring(0, 1);
            int positionsToMove = Int32.Parse(wirePoint.Substring(1, wirePoint.Length - 1));

            for (int i = 0; i < positionsToMove; i++)
            {
                switch (direction)
                {
                    case "U":
                        wirePosition.Y++;
                        break;
                    case "D":
                        wirePosition.Y--;
                        break;
                    case "R":
                        wirePosition.X++;
                        break;
                    case "L":
                        wirePosition.X--;
                        break;
                    default:
                        throw new Exception("Invalid direction");
                }
                if(isWireOne)
                {
                    WireOneCoordinates.Add(new Coordinate(wirePosition.X, wirePosition.Y));
                }
                else
                {
                    WireTwoCoordinates.Add(new Coordinate(wirePosition.X, wirePosition.Y));
                }
            }
        }

        private void DetermineClosestIntersectionDistance()
        {
            List<Coordinate> intersections = WireOneCoordinates.Intersect(WireTwoCoordinates).ToList();

            List<int> distancesCalculated = new List<int>();

            foreach(Coordinate intersection in intersections)
            {
                int distanceCalculated = Math.Abs(intersection.X) + Math.Abs(intersection.Y);
                distancesCalculated.Add(distanceCalculated);
            }
            ClosestIntersectionDistance = distancesCalculated.Min(x => x);
        }

        private void DetermineIntersectionWithLeastSteps()
        {
            List<Coordinate> intersections = WireOneCoordinates.Intersect(WireTwoCoordinates).ToList();

            List<int> totalSteps = new List<int>();

            foreach (Coordinate intersection in intersections)
            {
                int wireOneSteps = WireOneCoordinates.IndexOf(intersection) + 1;
                int wireTwoSteps = WireTwoCoordinates.IndexOf(intersection) + 1;

                totalSteps.Add(wireOneSteps + wireTwoSteps);
            }
            IntersectionWithLeastSteps = totalSteps.Min(x => x);
        }
    }
}
