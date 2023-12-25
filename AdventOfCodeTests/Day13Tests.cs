using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day13Tests
{
	private readonly string input = @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day13(input);

		var answer = day.Part1();

		answer.Should().Be("405");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day13(input);

		var answer = day.Part2();

		answer.Should().Be("400");
	}
}
