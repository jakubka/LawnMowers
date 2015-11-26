using System;
using FluentAssertions;
using LawnMowers.Domain.Model;
using Xunit;

namespace LawnMowers.Domain.Tests.Model
{
    public class PositionTests
    {
        [Fact]
        public void GetAdjacentPosition_ShouldReturnCorrectPosition()
        {
            var position = new Position(2, 3);

            var adjacentPosition =
                position.GetAdjacentPosition(Direction.North);

            adjacentPosition.X.Should().Be(2);
            adjacentPosition.Y.Should().Be(4);
        }

        [Fact]
        public void GetAdjacentPosition_ShouldThrowWhenNullIsPassedIn()
        {
            var position = new Position(2, 3);

            Action call = () => position.GetAdjacentPosition(null);
            call.ShouldThrow<ArgumentNullException>();
        }
    }
}
