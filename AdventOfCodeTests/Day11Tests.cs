using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day11Tests
{
	private readonly string input = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day11(input);

		var answer = day.Part1();

		answer.Should().Be("374");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day11(input);

		var answer = day.Part2();

		//factor 2= 374
		//factor 10 = 1030
		//factor  100 = 8410
		answer.Should().Be("8410");
	}
}
