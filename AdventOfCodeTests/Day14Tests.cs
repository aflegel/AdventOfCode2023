using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day14Tests
{
	private readonly string input = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day14(input);

		var answer = day.Part1();

		answer.Should().Be("136");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day14(input);

		var answer = day.Part2();

		answer.Should().Be("64");
	}
}
