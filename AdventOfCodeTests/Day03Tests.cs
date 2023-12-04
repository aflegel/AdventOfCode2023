using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day03Tests
{
	private readonly string input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day03(input);

		var answer = day.Part1();

		answer.Should().Be("4361");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day03(input);

		var answer = day.Part2();

		answer.Should().Be("467835");
	}
}
