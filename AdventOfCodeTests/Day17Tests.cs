using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day17Tests
{
	private readonly string input = @"2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day17(input);

		var answer = day.Part1();

		answer.Should().Be("102");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day17(input);

		var answer = day.Part2();

		answer.Should().Be("51");
	}
}
