using System;
using System.Linq;
using FluentAssertions;
using LawnMowers.Domain.Model;
using LawnMowers.Domain.Services;
using Xunit;

namespace LawnMowers.Domain.Tests.Services
{
    public class LawnMowerMovementSimulatorTests
    {
        [Fact]
        public void SimulateMovement_ShouldThrowWhenInitialStateIsNull()
        {
            var simulator = new LawnMowerMovementSimulator();

            Action call = () =>
                simulator.SimulateMovement(
                    null,
                    new LawnSize(5, 10),
                    new[] {LawnMowerCommand.MoveForward,});

            call.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SimulateMovement_ShouldThrowWhenLawnSizeIsNull()
        {
            var simulator = new LawnMowerMovementSimulator();

            Action call = () =>
                simulator.SimulateMovement(
                    new LawnMowerState(new Position(4, 1), Direction.East),
                    null,
                    new[] {LawnMowerCommand.MoveForward,});

            call.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SimulateMovement_ShouldThrowWhenCommandsIsNull()
        {
            var simulator = new LawnMowerMovementSimulator();

            Action call = () =>
                simulator.SimulateMovement(
                    new LawnMowerState(new Position(4, 1), Direction.East),
                    new LawnSize(5, 10),
                    null);

            call.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SimulateMovement_ShouldReturnCorrectResult()
        {
            var simulator = new LawnMowerMovementSimulator();

            var simulationResult =
                simulator.SimulateMovement(
                    new LawnMowerState(new Position(3, 3), Direction.East),
                    new LawnSize(5, 5),
                    new[]
                    {
                        LawnMowerCommand.MoveForward,
                        LawnMowerCommand.TurnRight,
                        LawnMowerCommand.MoveForward,
                        LawnMowerCommand.MoveForward,
                        LawnMowerCommand.TurnLeft,
                        LawnMowerCommand.MoveForward,
                    });

            var expectedSimulationResults = new[]
            {
                new LawnMowerState(new Position(4, 3), Direction.East), // after move east
                new LawnMowerState(new Position(4, 3), Direction.South), // after turn right
                new LawnMowerState(new Position(4, 2), Direction.South), // after move south
                new LawnMowerState(new Position(4, 1), Direction.South), // after move south
                new LawnMowerState(new Position(4, 1), Direction.East), // after turn left
                new LawnMowerState(new Position(5, 1), Direction.East), // after move east
            };

            simulationResult
                .Should()
                .Equal(expectedSimulationResults);
        }

        [Fact]
        public void SimulateMovement_ShouldThrowIfLawnMowerTriesToGoOutsideTheLawnBoundaries()
        {
            var simulator = new LawnMowerMovementSimulator();

            var simulationResult =
                simulator.SimulateMovement(
                    new LawnMowerState(new Position(3, 3), Direction.East),
                    new LawnSize(5, 5),
                    new[]
                    {
                        LawnMowerCommand.MoveForward, // 4, 3
                        LawnMowerCommand.MoveForward, // 5, 3
                        LawnMowerCommand.MoveForward, // 6, 3 -> this is outside
                    });

            Action call = () => simulationResult.ToArray();
            call.ShouldThrow<CannotMoveOutsideLawnBoundariesException>();
        }
    }
}