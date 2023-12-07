using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day07Tests
{
	private readonly string input = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day07(input);

		var answer = day.Part1();

		answer.Should().Be("6440");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day07(input);

		var answer = day.Part2();

		answer.Should().Be("5905");
	}
}
