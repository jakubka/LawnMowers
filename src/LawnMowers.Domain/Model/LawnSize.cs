using System;

namespace LawnMowers.Domain.Model
{
    public class LawnSize
    {
        public LawnSize(
            int rightmostCoordinate,
            int topmostCoordinate)
        {
            if (rightmostCoordinate < 0)
            {
                throw new ArgumentNullException(nameof(rightmostCoordinate), "Rightmost coordinate cannot be negative");
            }
            if (topmostCoordinate < 0)
            {
                throw new ArgumentNullException(nameof(topmostCoordinate), "Topmost coordinate cannot be negative");
            }

            RightmostCoordinate = rightmostCoordinate;
            TopmostCoordinate = topmostCoordinate;
        }

        public int RightmostCoordinate { get; }

        public int TopmostCoordinate { get; }

        public bool IsPositionWithinLawn(
            Position position)
        {
            return position.X >= 0
                && position.X <= RightmostCoordinate
                && position.Y >= 0
                && position.Y <= TopmostCoordinate;
        }

        protected bool Equals(LawnSize other)
        {
            return RightmostCoordinate == other.RightmostCoordinate && TopmostCoordinate == other.TopmostCoordinate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LawnSize) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RightmostCoordinate*397) ^ TopmostCoordinate;
            }
        }
    }
}