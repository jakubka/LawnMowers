using System;
using FluentAssertions;
using LawnMowers.Domain.Model;
using Xunit;

namespace LawnMowers.Domain.Tests.Model
{
    public class LawnSizeTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        [InlineData(5, 10)]
        public void IsPositionWithinLawn_ShouldReturnTrueWhenPositionIsWithin(
            int positionX,
            int positionY)
        {
            var lawnSize = new LawnSize(5, 10);
            var position = new Position(positionX, positionY);

            lawnSize
                .IsPositionWithinLawn(position)
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData(-100, -100)]
        [InlineData(-1, 2)]
        [InlineData(6, 5)]
        [InlineData(5, 11)]
        [InlineData(6, 11)]
        [InlineData(100, 100)]
        public void IsPositionWithinLawn_ShouldReturnFalseWhenPositionIsNotWithin(
            int positionX,
            int positionY)
        {
            var lawnSize = new LawnSize(5, 10);
            var position = new Position(positionX, positionY);

            lawnSize
                .IsPositionWithinLawn(position)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData(-1, 10)]
        [InlineData(10, -1)]
        public void Constructor_ShouldThrowWhenCoordinateIsNegative(
            int rightmostCoordinate,
            int topmostCoordinate)
        {
            Action call = () => new LawnSize(rightmostCoordinate, topmostCoordinate);
            call.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TwoTheSameLawnSizesShouldBeEqual()
        {
            var lawnSize1 = new LawnSize(5, 10);
            var lawnSize2 = new LawnSize(5, 10);

            (lawnSize1.Equals(lawnSize2))
                .Should()
                .BeTrue();
        }

        [Fact]
        public void TwoDifferentLawnSizesShouldNotBeEqual()
        {
            var lawnSize1 = new LawnSize(5, 10);
            var lawnSize2 = new LawnSize(10, 10);

            (lawnSize1.Equals(lawnSize2))
                .Should()
                .BeFalse();
        }
    }
}
