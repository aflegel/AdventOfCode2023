namespace AdventOfCode;

public class Day04(string input) : IAdventDay
{
	private record Card(int[] Winners, int[] Plays)
	{
		public int Match { get; } = Winners.Intersect(Plays).Count();
	}

	private Card[] InputArray { get; } = input.Replace("  ", " ").Split("\n")
		.Select(x =>
		{
			var split = x.Split(": ")[1].Split(" | ");
			return new Card(
				split[0].Split(" ").Select(s => Convert.ToInt32(s)).ToArray(),
				split[1].Split(" ").Select(s => Convert.ToInt32(s)).ToArray());
		}).ToArray();

	public string Part1()
	{
		var totals = InputArray.Where(m => m.Match > 0).Select(m => Math.Pow(2, m.Match - 1)).ToArray();
		return totals.Sum().ToString();
	}

	public string Part2()
	{
		var sums = Enumerable.Repeat(1, InputArray.Length).ToArray();

		for (var i = 0; i < InputArray.Length; i++)
		{
			foreach (var next in Enumerable.Range(i + 1, InputArray[i].Match))
			{
				sums[next] += sums[i];
			}
		}

		return sums.Sum().ToString();
	}
}
