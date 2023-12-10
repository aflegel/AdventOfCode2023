using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day08Tests
{
	private readonly string input = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day08(input);

		var answer = day.Part1();

		answer.Should().Be("6");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day08(input);

		var answer = day.Part2();

		answer.Should().Be("6");
	}
}
