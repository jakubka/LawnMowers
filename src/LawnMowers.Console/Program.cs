using System;
using System.Collections.Generic;
using System.Linq;
using LawnMowers.Domain.Model;
using LawnMowers.Domain.Services;

namespace LawnMowers.Console
{
    using Console = System.Console;

    public class Program
    {
        public void Main()
        {
            var inputLines = ReadInputLines()
                .TakeWhile(line => line != "");

            try
            {
                var outputLines = PerformSimulations(inputLines)
                    .ToArray();

                foreach (var outputLine in outputLines)
                {
                    Console.WriteLine(outputLine);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error while performing simulation");
                Console.Error.WriteLine($"Error type: {e.GetType().Name}, error message: {e.Message}");
            }
        }

        public IEnumerable<string> PerformSimulations(
            IEnumerable<string> inputLines)
        {
            var inputLinesArray = inputLines.ToArray();

            var inputParser = new InputParser();

            if (!inputLinesArray.Any())
            {
                throw new InvalidInputException("Lawn size missing");
            }

            var lawnSize = inputParser.ParseLawnSize(inputLinesArray[0]);

            if ((inputLinesArray.Length - 1)%2 != 0)
            {
                throw new InvalidInputException("Commands for Lawn Mower missing");
            }

            for (int i = 1; i < inputLinesArray.Length; i = i + 2)
            {
                var initialState = inputParser
                    .ParseLawnMowerState(inputLinesArray[i]);

                var commands = inputParser
                    .ParseLawnMowerCommands(inputLinesArray[i + 1])
                    .ToArray();

                yield return PerformSimulation(initialState, lawnSize, commands);
            }
        }

        private static string PerformSimulation(
            LawnMowerState initialState,
            LawnSize lawnSize,
            LawnMowerCommand[] commands)
        {
            var simulator = new LawnMowerMovementSimulator();

            try
            {
                var lawnMowerSteps =
                    simulator.SimulateMovement(
                        initialState,
                        lawnSize,
                        commands);

                var finalState = lawnMowerSteps
                    .Last();

                return $"{finalState.Position.X} {finalState.Position.Y} {finalState.Direction.Sign}";
            }
            catch (CannotMoveOutsideLawnBoundariesException)
            {
                return "Mower tried to cut rare plants";
            }
        }

        private IEnumerable<string> ReadInputLines()
        {
            while (true)
            {
                yield return Console.ReadLine();
            }
        }
    }
}