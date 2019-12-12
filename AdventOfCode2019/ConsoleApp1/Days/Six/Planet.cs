using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Days.Six
{
    public class Planet
    {
        public String Name { get; set; }
        public List<Planet> PlanetsDirectlyOrbited { get; }
        public bool HasDirectOrbit { get; private set; }

        public Planet(string name)
        {
            Name = name;
            PlanetsDirectlyOrbited = new List<Planet>();
        }

        public void AddDirectlyOrbitedPlanet(Planet planet)
        {
            PlanetsDirectlyOrbited.Add(planet);
            HasDirectOrbit = true;
        }

        public int CalculateIndirectOrbits()
        {
            int count = 0;
            if(HasDirectOrbit)
            {
                Planet planet = PlanetsDirectlyOrbited.FirstOrDefault();

                while(planet.HasDirectOrbit)
                {
                    planet = planet.PlanetsDirectlyOrbited.FirstOrDefault();
                    count++;
                }
            }
            return count;
        }

        public int CalculateDirectOrbits()
        {
            return PlanetsDirectlyOrbited.Count;
        }
    }
}
