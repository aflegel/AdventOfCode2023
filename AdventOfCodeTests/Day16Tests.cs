using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day16Tests
{
	private readonly string input = @".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day16(input);

		var answer = day.Part1();

		answer.Should().Be("46");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day16(input);

		var answer = day.Part2();

		answer.Should().Be("51");
	}
}
