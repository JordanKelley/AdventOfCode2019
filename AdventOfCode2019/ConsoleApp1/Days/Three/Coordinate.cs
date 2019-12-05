using System;

namespace ConsoleApp1.Days.Three
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate other)
        {
            if (this.X == other.X && this.Y == other.Y)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object obj) => Equals(obj as Coordinate);
        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}
