using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Days.Six
{
    public class Planet : IEquatable<Planet>
    {
        public String Name { get; set; }
        public List<Planet> PlanetsDirectlyOrbited { get; }
        public List<Planet> PlanetsIndirectlyOrbited { get; }
        public bool HasDirectOrbit { get; private set; }

        public Planet(string name)
        {
            Name = name;
            PlanetsDirectlyOrbited = new List<Planet>();
            PlanetsIndirectlyOrbited = new List<Planet>();
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
                    PlanetsIndirectlyOrbited.Add(planet);
                    count++;
                }
            }
            return count;
        }

        public int CalculateDirectOrbits()
        {
            return PlanetsDirectlyOrbited.Count;
        }

        public bool Equals(Planet other)
        {
            if(other.Name == this.Name)
            {
                return true;
            }
            return false;
        }
    }
}
