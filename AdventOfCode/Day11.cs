using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode;

public class Day11(string input) : IAdventDay
{
	private record MapPoint(int X, int Y)
	{
		public static int operator -(MapPoint a, MapPoint b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
	}
	private List<string> InputArray { get; } = [.. input.Split("\n")];

	public string Part1() => CalculateExpansion(2).ToString();
	public string Part2() => CalculateExpansion(100).ToString();

	private long CalculateExpansion(int factor)
	{
		var stars = GetStars();
		var (xSet, ySet) = FindExpansion();

		var set = stars.SelectMany(s => stars.Select(s2 => (s, s2))).Where(s => s.s != s.s2).Distinct(new TransitiveComparer()).ToList();
		var linear = set.Sum(s => s.Item1 - s.Item2);
		var expansion = set.Select(s => NumbersBetween(xSet, s.Item1.X, s.Item2.X) + NumbersBetween(ySet, s.Item1.Y, s.Item2.Y)).ToList();

		return linear + expansion.Sum(s => (s * factor) - s);
	}

	private IEnumerable<MapPoint> GetStars()
	{
		for (var i = 0; i < InputArray.Count; i++)
		{
			for (var j = 0; j < InputArray[i].Length; j++)
			{
				if (InputArray[i][j] == '#')
					yield return new MapPoint(i, j);
			}
		}
	}

	private class TransitiveComparer : IEqualityComparer<(MapPoint, MapPoint)>
	{
		public bool Equals((MapPoint, MapPoint) a, (MapPoint, MapPoint) b) =>
			a.Item1 == b.Item1 && a.Item2 == b.Item2 || a.Item1 == b.Item2 && a.Item2 == b.Item1;
		public int GetHashCode([DisallowNull] (MapPoint, MapPoint) obj) =>
			 obj.Item1.GetHashCode() + obj.Item2.GetHashCode();
	}

	public static long NumbersBetween(int[] nums, int a, int b) => (a, b) switch
	{
		_ when a < b => nums.Where(num => a <= num && num <= b).Count(),
		_ when a > b => nums.Where(num => b <= num && num <= a).Count(),
		_ => 0
	};

	private (int[] xSet, int[] ySet) FindExpansion()
	{
		List<int> xSet = [];
		List<int> ySet = [];
		for (var i = 0; i < InputArray.Count; i++)
		{
			if (InputArray[i].All(a => a == '.'))
			{
				xSet.Add(i);
			}
		}

		for (var i = 0; i < InputArray[0].Length; i++)
		{
			var column = string.Join("", InputArray.Select(s => s[i]));
			if (column.All(a => a == '.'))
			{
				ySet.Add(i);
			}
		}

		return (xSet.ToArray(), ySet.ToArray());
	}
}