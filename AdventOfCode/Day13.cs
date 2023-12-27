namespace AdventOfCode;

public class Day13(string input) : IAdventDay
{
	private string[][] InputArray { get; } = input.Split("\n\n")
		.Select(s => s.Split("\n").ToArray()).ToArray();

	public string Part1()
	{
		var result = InputArray.Sum(
			grid => 
				TryFindMirror(grid) * 100 
				?? TryFindMirror(Transpose(grid)) 
				?? 0);

		return result.ToString();
	}

	private static int? TryFindMirror(string[] grid)
	{
		static bool IsReflectionFrom(string[] grid, int startIndex)
		{
			for ((var i, var j) = (startIndex, startIndex + 1); i >= 0 && j < grid.Length; i--, j++)
			{
				if (grid[i] != grid[j])
					return false;
			}
			return true;
		}

		for (var i = 0; i < grid.Length - 1; i++)
		{
			if (IsReflectionFrom(grid, i))
				return i + 1;
		}

		return null;
	}

	private static string[] Transpose(string[] grid)
	{
		List<string> result = [];
		for (var i = 0; i < grid[0].Length; i++)
		{
			result.Add(string.Join("", grid.Select(s => s[i])));
		}
		return [.. result];
	}

	public string Part2()
	{
		var result = InputArray.Sum(
			grid => 
				TryFindSmudgeMirror(grid) * 100 
				?? TryFindSmudgeMirror(Transpose(grid)) 
				?? 0);

		return result.ToString();
	}

	private static int? TryFindSmudgeMirror(string[] grid)
	{
		static bool IsReflectionFrom(string[] grid, int startIndex)
		{
			var smudge = false;
			for ((var i, var j) = (startIndex, startIndex + 1); i >= 0 && j < grid.Length; i--, j++)
			{
				if (grid[i] != grid[j])
				{
					if (smudge)
						return false;
					else if (Difference(grid[i], grid[j]) == 1)
						smudge = true;
					else
						return false;
				}
			}
			return smudge;
		}

		for (var i = 0; i < grid.Length - 1; i++)
		{
			if (IsReflectionFrom(grid, i))
				return i + 1;
		}
		return null;
	}

	private static int Difference(string a, string b)
	{
		var result = 0;
		for (var i = 0; i < a.Length; i++)
		{
			if (a[i] != b[i])
				result++;
		}
		return result;
	}
}