using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day15Tests
{
	private readonly string input = @"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day15(input);

		var answer = day.Part1();

		answer.Should().Be("1320");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day15(input);

		var answer = day.Part2();

		answer.Should().Be("145");
	}
}
