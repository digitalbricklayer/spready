using System;

namespace Spready.Calc
{
    public class CellCoordinates : IEquatable<CellCoordinates>
    {
        public CellCoordinates(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; }

        public int Row { get; }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (CellCoordinates)obj;
            return Column == p.Column && Row == p.Row;
        }

        public bool Equals(CellCoordinates other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Column == other.Column && Row == other.Row;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Column * 397) ^ Row;
            }
        }
    }
}
