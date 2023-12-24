using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day12Tests
{
	private readonly string input = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1".ReplaceLineEndings("\n");

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day12(input);

		var answer = day.Part1();

		answer.Should().Be("21");
	}

	[Fact]
	public void Part2ShouldMatchExampleCount()
	{
		var day = new Day12(input);

		var answer = day.Part2();

		answer.Should().Be("525152");
	}
}
