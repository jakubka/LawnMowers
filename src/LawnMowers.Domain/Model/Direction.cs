namespace LawnMowers.Domain.Model
{
    public class Direction
    {
        private Direction(
            char sign,
            int deltaX,
            int deltaY)
        {
            Sign = sign;
            DeltaX = deltaX;
            DeltaY = deltaY;
        }

        public char Sign { get; }

        public int DeltaX { get; }

        public int DeltaY { get; }

        public Direction DirectionToTheRight { get; private set; }

        public Direction DirectionToTheLeft { get; private set; }

        protected bool Equals(Direction other) => DeltaX == other.DeltaX && DeltaY == other.DeltaY;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Direction) obj);
        }

        public override int GetHashCode() => new {DeltaX, DeltaY}.GetHashCode();

        public static Direction North { get; }

        public static Direction East { get; }

        public static Direction South { get; }

        public static Direction West { get; }

        static Direction()
        {
            var north = new Direction('N', 0, 1);
            var east = new Direction('E', 1, 0);
            var south = new Direction('S', 0, -1);
            var west = new Direction('W', -1, 0);

            north.DirectionToTheLeft = west;
            north.DirectionToTheRight = east;

            east.DirectionToTheLeft = north;
            east.DirectionToTheRight = south;

            south.DirectionToTheLeft = east;
            south.DirectionToTheRight = west;

            west.DirectionToTheLeft = south;
            west.DirectionToTheRight = north;

            North = north;
            East = east;
            South = south;
            West = west;
        }

        public static Direction GetBySign(
            char directionSign)
        {
            switch (directionSign)
            {
                case 'N':
                    return North;
                case 'E':
                    return East;
                case 'S':
                    return South;
                case 'W':
                    return West;
                default:
                    throw new InvalidDirectionSignException();
            }
        }
    }
}