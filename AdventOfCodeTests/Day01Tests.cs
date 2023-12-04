using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day01Tests
{
	private readonly string input = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

	private readonly string input2 = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day01(input);

		var answer = day.Part1();

		answer.Should().Be("142");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day01(input2);

		var answer = day.Part2();

		answer.Should().Be("281");
	}
}
