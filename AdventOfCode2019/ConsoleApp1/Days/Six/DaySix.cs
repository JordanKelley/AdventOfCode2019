using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1.Days.Six
{
    public static class DaySix
    {
        public static int PartOne()
        {
            String[] testInput = Input.TestInput;
            List<Planet> planets = LoadPlanets(testInput);

            int totalOrbits = 0;
            foreach(Planet planet in planets)
            {
                int directOrbits = planet.CalculateDirectOrbits();
                int indirectOrbits = planet.CalculateIndirectOrbits();
                totalOrbits = directOrbits + indirectOrbits + totalOrbits;
            }
            return totalOrbits;
        }

        public static List<Planet> LoadPlanets(String[] testInput)
        {
            List<Planet> planets = new List<Planet>();

            foreach(string input in testInput)
            {
                var split = input.Split(")");
                string planetOneName = split[0];
                string planetTwoName = split[1];
                Planet one;
                Planet two; 

                if(planets.Any(x => x.Name == planetOneName))
                {
                    one = planets.First(x => x.Name == planetOneName);
                }
                else
                {
                    one = new Planet(planetOneName);
                    planets.Add(one);
                }

                if (planets.Any(x => x.Name == planetTwoName))
                {
                    two = planets.First(x => x.Name == planetTwoName);
                }
                else
                {
                    two = new Planet(planetTwoName);
                    planets.Add(two);
                }

                two.AddDirectlyOrbitedPlanet(one);
            }

            return planets;
        }
    }
}
