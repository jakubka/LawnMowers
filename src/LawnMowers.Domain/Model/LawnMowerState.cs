using System;

namespace LawnMowers.Domain.Model
{
    public class LawnMowerState
    {
        public LawnMowerState(
            Position position,
            Direction direction)
        {
            if (direction == null)
            {
                throw new ArgumentNullException(nameof(direction));
            }

            Position = position;
            Direction = direction;
        }

        public Position Position { get; }

        public Direction Direction { get; }

        public LawnMowerState GetStateAfterRightTurn()
        {
            return new LawnMowerState(
                Position,
                Direction.DirectionToTheRight);
        }

        public LawnMowerState GetStateAfterLeftTurn()
        {
            return new LawnMowerState(
                Position,
                Direction.DirectionToTheLeft);
        }

        public LawnMowerState GetStateAfterMoveForward()
        {
            return new LawnMowerState(
                Position.GetAdjacentPosition(Direction),
                Direction);
        }

        protected bool Equals(LawnMowerState other)
        {
            return Position.Equals(other.Position) && Equals(Direction, other.Direction);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LawnMowerState) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Position.GetHashCode()*397) ^ (Direction?.GetHashCode() ?? 0);
            }
        }
    }
}