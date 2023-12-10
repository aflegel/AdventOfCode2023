namespace AdventOfCode;

public class Day09(string input) : IAdventDay
{
	private int[][] InputArray { get; } = input.Split("\n").Select(s => s.Split(" ").Select(int.Parse).ToArray()).ToArray();

	public string Part1()
	{
		var result = InputArray.Select(s => s[^1] + GetDifference(s)).ToArray();
		return result.Sum().ToString();
	}

	private static int GetDifference(int[] series)
	{
		var newSeries = series.Skip(1).Select((item, i) => item - series[i]).ToArray();

		return newSeries.All(a => a == 0) ? 0 : newSeries[^1] + GetDifference(newSeries);
	}

	public string Part2()
	{
		var result = InputArray.Select(s => s[0] - GetPreviousDifference(s)).ToArray();
		return result.Sum().ToString();
	}

	private static int GetPreviousDifference(int[] series)
	{
		var newSeries = series.Skip(1).Select((item, i) => item - series[i]).ToArray();
		return newSeries.All(a => a == 0) ? 0 : newSeries[0] - GetPreviousDifference(newSeries);
	}
}
