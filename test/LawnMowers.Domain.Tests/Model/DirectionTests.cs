using System;
using System.Collections.Generic;
using FluentAssertions;
using LawnMowers.Domain.Model;
using Xunit;

namespace LawnMowers.Domain.Tests.Model
{
    public class DirectionTests
    {
        [Fact]
        public void DirectionShouldBeEqualToTheSameDirection()
        {
            (Direction.North == Direction.North).Should().BeTrue();
        }

        [Fact]
        public void TwoDifferentDirectionsShouldNotBeEqual()
        {
            (Direction.North == Direction.East).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(AllDirections))]
        public void TurningFourTimesRightShouldReturnToStartingDirection(Direction direction)
        {
            direction
                .DirectionToTheRight
                .DirectionToTheRight
                .DirectionToTheRight
                .DirectionToTheRight
                .Should()
                .Be(direction);
        }

        [Theory]
        [MemberData(nameof(AllDirections))]
        public void TurningFourTimesLeftShouldReturnToStartingDirection(Direction direction)
        {
            direction
                .DirectionToTheLeft
                .DirectionToTheLeft
                .DirectionToTheLeft
                .DirectionToTheLeft
                .Should()
                .Be(direction);
        }

        [Theory]
        [MemberData(nameof(AllDirections))]
        public void TurningRightAndThenLeftShouldBeStartingDirection(Direction direction)
        {
            direction
                .DirectionToTheRight
                .DirectionToTheLeft
                .Should()
                .Be(direction);
        }

        [Theory]
        [MemberData(nameof(AllDirections))]
        public void TurningLeftAndThenRightShouldBeStartingDirection(Direction direction)
        {
            direction
                .DirectionToTheLeft
                .DirectionToTheRight
                .Should()
                .Be(direction);
        }

        [Theory]
        [MemberData(nameof(AllDirections))]
        public void GetBySign_ShouldReturnDirectionIfSignIsValid(Direction validDirection)
        {
            var returnedDirection =
                Direction.GetBySign(validDirection.Sign);

            returnedDirection
                .Should()
                .Be(validDirection);
        }

        [Theory]
        [InlineData('a')]
        [InlineData('B')]
        [InlineData('n')] // lowercase sign is invalid
        public void GetBySign_ShouldThrowIfSignIsInvalid(char directionSign)
        {
            Action call = () => Direction.GetBySign(directionSign);
            call.ShouldThrow<InvalidDirectionSignException>();
        }

        public static IEnumerable<object[]> AllDirections
        {
            get
            {
                yield return new[] { Direction.North };
                yield return new[] { Direction.East };
                yield return new[] { Direction.South };
                yield return new[] { Direction.West };
            }
        }
    }
}
