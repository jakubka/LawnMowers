using System;
using System.Linq;
using FluentAssertions;
using LawnMowers.Domain.Model;
using LawnMowers.Domain.Services;
using Xunit;

namespace LawnMowers.Console.Tests
{
    public class ProgramTests
    {
        [Theory]
        [InlineData("5 5|1 2 N|LMLMLMLMM|3 3 E|MMRMMRMRRM", "1 3 N|5 1 E")]
        [InlineData("5 5|1 2 N|LMLMLMLMM|1 2 N|LMLMLMMMMMMMMMMLMM|3 3 E|MMRMMRMRRM", "1 3 N|Mower tried to cut rare plants|5 1 E")]
        public void PerformSimulations_ShouldReturnExpectedResult(
            string input,
            string expectedOutput)
        {
            var program = new Program();

            var inputLines = input.Split('|');
            var expectedOutputLines = expectedOutput.Split('|');

            var outputLines = program
                .PerformSimulations(inputLines)
                .ToArray();

            outputLines
                .Should()
                .Equal(expectedOutputLines);
        }
    }
}
