using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day10Tests
{
	private readonly string input = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ".ReplaceLineEndings("\n");

	private readonly string input2a = @"..........
.S------7.
.|F----7|.
.||OOOO||.
.||OOOO||.
.|L-7F-J|.
.|II||II|.
.L--JL--J.
..........";

	private readonly string input2b = @".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...";

	private readonly string input2c = @"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJIF7FJ-
L---JF-JLJIIIIFJLJJ7
|F|F-JF---7IIIL7L|7|
|FFJF7L7F-JF7IIL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L";

	[Fact]
	public void Part1ShouldMatchExampleCount()
	{
		var day = new Day10(input);

		var answer = day.Part1();

		answer.Should().Be("8");
	}

	[Fact]
	public void Part2ShouldMatchExampleCounta()
	{
		var day = new Day10(input2a);

		var answer = day.Part2();

		answer.Should().Be("4");
	}

	[Fact]
	public void Part2ShouldMatchExampleCountb()
	{
		var day = new Day10(input2b);

		var answer = day.Part2();

		answer.Should().Be("8");
	}

		[Fact]
	public void Part2ShouldMatchExampleCount2c()
	{
		var day = new Day10(input2c);

		var answer = day.Part2();

		answer.Should().Be("10");
	}
}
