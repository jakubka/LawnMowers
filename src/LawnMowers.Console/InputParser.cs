using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LawnMowers.Domain.Model;
using LawnMowers.Domain.Services;

namespace LawnMowers.Console
{
    public class InputParser
    {
        private static readonly Regex LawnSizeRegex =
            new Regex(@"^(?<TopmostPosition>\d+) (?<RightmostPosition>\d+)$",
                RegexOptions.Compiled | RegexOptions.Singleline);

        private static readonly Regex LawnMowerStateRegex =
            new Regex(@"^(?<X>\d+) (?<Y>\d+) (?<DirectionSign>[NESW])$", RegexOptions.Compiled | RegexOptions.Singleline);

        public LawnSize ParseLawnSize(
            string input)
        {
            var match = LawnSizeRegex.Match(input);

            if (!match.Success)
            {
                throw new InvalidInputException("Lawn size is not in correct format");
            }

            try
            {
                int rightmostPosition = int.Parse(match.Groups["RightmostPosition"].Value);
                int topmostPosition = int.Parse(match.Groups["TopmostPosition"].Value);

                return new LawnSize(
                    rightmostPosition,
                    topmostPosition);
            }
            catch (Exception e)
            {
                throw new InvalidInputException(e.Message);
            }
        }

        public LawnMowerState ParseLawnMowerState(
            string input)
        {
            var match = LawnMowerStateRegex.Match(input);

            if (!match.Success)
            {
                throw new InvalidInputException($"Lawn mower initial state ({input}) is not in correct format");
            }

            try
            {
                int x = int.Parse(match.Groups["X"].Value);
                int y = int.Parse(match.Groups["Y"].Value);
                char directionSign = match.Groups["DirectionSign"].Value[0];

                var position = new Position(x, y);
                var direction = Direction.GetBySign(directionSign);

                return new LawnMowerState(position, direction);
            }
            catch (Exception e)
            {
                throw new InvalidInputException(e.Message);
            }
        }

        public IEnumerable<LawnMowerCommand> ParseLawnMowerCommands(
            string input)
        {
            if (input == "")
            {
                throw new InvalidInputException("Empty commands list");
            }

            foreach (var commandChar in input)
            {
                switch (commandChar)
                {
                    case 'L':
                        yield return LawnMowerCommand.TurnLeft;
                        break;
                    case 'R':
                        yield return LawnMowerCommand.TurnRight;
                        break;
                    case 'M':
                        yield return LawnMowerCommand.MoveForward;
                        break;
                    default:
                        throw new InvalidInputException($"Unknown Lawn Mower command: {commandChar}");
                }
            }
        }
    }
}