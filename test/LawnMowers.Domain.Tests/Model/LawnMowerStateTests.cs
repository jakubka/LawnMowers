using System;
using FluentAssertions;
using LawnMowers.Domain.Model;
using Xunit;

namespace LawnMowers.Domain.Tests.Model
{
    public class LawnMowerStateTests
    {
        [Fact]
        public void Constructor_ShouldThrowWhenDirectionIsNull()
        {
            Action call = () => new LawnMowerState(new Position(2, 4), null);
            call.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetStateAfterRightTurn_ShouldReturnCorrectNewState()
        {
            var originalState = new LawnMowerState(new Position(5, 10), Direction.East);

            var expectedNewState = new LawnMowerState(new Position(5, 10), Direction.South);

            var newState = originalState.GetStateAfterRightTurn();

            newState
                .Should()
                .Be(expectedNewState);
        }

        [Fact]
        public void GetStateAfterLeftTurn_ShouldReturnCorrectNewState()
        {
            var originalState = new LawnMowerState(new Position(5, 10), Direction.East);

            var expectedNewState = new LawnMowerState(new Position(5, 10), Direction.North);

            var newState = originalState.GetStateAfterLeftTurn();

            newState
                .Should()
                .Be(expectedNewState);
        }
        
        [Fact]
        public void GetStateAfterMoveForward_ShouldReturnCorrectNewState()
        {
            var originalState = new LawnMowerState(new Position(5, 10), Direction.East);

            var expectedNewState = new LawnMowerState(new Position(6, 10), Direction.East);

            var newState = originalState.GetStateAfterMoveForward();

            newState
                .Should()
                .Be(expectedNewState);
        }
    }
}
