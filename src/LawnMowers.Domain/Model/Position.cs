using System;

namespace LawnMowers.Domain.Model
{
    public struct Position
    {
        public Position(
            int x,
            int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public Position GetAdjacentPosition(
            Direction direction)
        {
            if (direction == null)
            {
                throw new ArgumentNullException(nameof(direction));
            }

            return new Position(X + direction.DeltaX, Y + direction.DeltaY);
        }
    }
}