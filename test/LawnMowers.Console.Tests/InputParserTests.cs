using System;
using System.Linq;
using FluentAssertions;
using LawnMowers.Domain.Model;
using LawnMowers.Domain.Services;
using Xunit;

namespace LawnMowers.Console.Tests
{
    public class InputParserTests
    {
        [Theory]
        [InlineData("0 0", 0, 0)]
        [InlineData("5 5", 5, 5)]
        [InlineData("1234 500", 1234, 500)]
        [InlineData("999999 999999", 999999, 999999)]
        public void ParseLawnSize_ShouldReturnCorrectLawnSizeWhenInputIsValid(
            string input,
            int expectedTopmostCoordinate,
            int expectedRightmostCoordinate)
        {
            var inputParser = new InputParser();

            var lawnSize = inputParser.ParseLawnSize(input);

            var expectedLawnSize = new LawnSize(
                expectedRightmostCoordinate,
                expectedTopmostCoordinate);

            lawnSize
                .Should()
                .Be(expectedLawnSize);
        }

        [Theory]
        [InlineData("abcd")]
        [InlineData("5 x")]
        [InlineData("1234")]
        [InlineData("3 5 ")]
        [InlineData("dasd 4")]
        [InlineData("")]
        [InlineData("6546546464123 5")]
        [InlineData("-1 2")]
        [InlineData("12312312312312 123123123123")] // int32 is max allowed 
        public void ParseLawnSize_ShouldThrowWhenInputIsInvalid(string input)
        {
            var inputParser = new InputParser();

            Action call = () => inputParser.ParseLawnSize(input);
            call.ShouldThrow<InvalidInputException>();
        }

        [Theory]
        [InlineData("3 5 N", 3, 5, 'N')]
        [InlineData("0 0 E", 0, 0, 'E')]
        [InlineData("35222 5555 S", 35222, 5555, 'S')]
        [InlineData("999999 999999 W", 999999, 999999, 'W')]
        public void ParseLawnMowerState_ShouldReturnCorrectStateWhenInputIsValid(
            string input,
            int expectedX,
            int expectedY,
            char expectedDirectionSign)
        {
            var inputParser = new InputParser();

            var parsedState = inputParser.ParseLawnMowerState(input);

            var expectedState = new LawnMowerState(
                new Position(expectedX, expectedY),
                Direction.GetBySign(expectedDirectionSign));

            parsedState
                .Should()
                .Be(expectedState);
        }

        [Theory]
        [InlineData("3 b N")]
        [InlineData("")]
        [InlineData("xxx N")]
        [InlineData("3 5 n")]
        [InlineData("3 5 N ")]
        [InlineData(" 3 5 N ")]
        [InlineData("3 565465465465464 N")]
        [InlineData("3 -1 N")]
        [InlineData("3 5 X")]
        [InlineData("blabla")]
        public void ParseLawnMowerState_ShouldThrowWhenInputIsInvalid(
            string input)
        {
            var inputParser = new InputParser();

            Action call = () => inputParser.ParseLawnMowerState(input);
            call.ShouldThrow<InvalidInputException>();
        }

        [Fact]
        public void ParseLawnMowerCommands_ShouldReturnCorrectCommandsWhenInputIsValid()
        {
            var inputParser = new InputParser();

            var parsedCommands = inputParser.ParseLawnMowerCommands("MLRRRLLM");

            var expectedCommands = new[] {
                LawnMowerCommand.MoveForward,
                LawnMowerCommand.TurnLeft,
                LawnMowerCommand.TurnRight,
                LawnMowerCommand.TurnRight,
                LawnMowerCommand.TurnRight,
                LawnMowerCommand.TurnLeft,
                LawnMowerCommand.TurnLeft,
                LawnMowerCommand.MoveForward,
            };

            parsedCommands
                .Should()
                .Equal(expectedCommands);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("MMMMxx")]
        [InlineData(" M")]
        [InlineData("")]
        [InlineData("MMMr")]
        [InlineData("M ")]
        public void ParseLawnMowerCommands_ShouldThrowWhenInputIsInvalid(
            string input)
        {
            var inputParser = new InputParser();

            Action call = () => inputParser.ParseLawnMowerCommands(input).ToArray();
            call.ShouldThrow<InvalidInputException>();
        }
    }
}
