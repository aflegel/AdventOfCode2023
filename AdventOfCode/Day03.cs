namespace AdventOfCode;

public class Day03 : IAdventDay
{
	public Day03(string input)
	{
		var rows = input.ReplaceLineEndings("\n").Split("\n");

		InputArray = new char[rows.Length, rows.First().Length];

		for (var i = 0; i < rows.Length; i++)
		{
			for (var j = 0; j < rows[i].Length; j++)
			{
				InputArray[i, j] = rows[i][j];
			}
		}
	}

	private static readonly string SymbolSet = "/$*#%&@+-=";
	private static readonly string NumberSet = "0123456789";

	private char[,] InputArray { get; }

	public string Part1()
	{
		var partSum = 0;

		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				// found start
				if (NumberSet.Contains(InputArray[i, j]))
				{
					// peek to end
					var result = FindEnd(i, j);
					// search
					if (CheckForAdjacentSymbol(i, j, result.Length))
					{
						partSum += Convert.ToInt32(result);
					}

					// update j
					j += result.Length;
				}
			}
		}

		return partSum.ToString();
	}

	private bool CheckForAdjacentSymbol(int x, int y, int length)
	{
		for (var i = x - 1; i < x + 2; i++)
		{
			for (var j = y - 1; j < y + length + 1; j++)
			{
				if (i < 0 || j < 0 || i >= InputArray.GetLength(0) || j >= InputArray.GetLength(1))
				{
					continue;
				}

				if (SymbolSet.Contains(InputArray[i, j]))
				{
					return true;
				}
			}
		}

		return false;
	}

	private string FindEnd(int x, int y)
	{
		var length = "";

		while (true)
		{
			if (NumberSet.Contains(InputArray[x, y]))
				length += InputArray[x, y];
			else
				return length;
			y++;

			if (y >= InputArray.GetLength(1))
				return length;
		}
	}

	public string Part2()
	{
		var partSum = 0;

		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				// found start
				if (InputArray[i, j] == '*')
				{
					var numbers = FindNumbers(i, j);

					if (numbers.Count() == 2)
						partSum += numbers[0] * numbers[1];
				}
			}
		}

		return partSum.ToString();
	}

	private int[] FindNumbers(int x, int y)
	{
		List<int> count = [];
		var previousWasNumber = false;

		for (var i = x - 1; i < x + 2; i++)
		{
			for (var j = y - 1; j < y + 2; j++)
			{
				if (i < 0 || j < 0 || i >= InputArray.GetLength(0) || j >= InputArray.GetLength(1))
				{
					continue;
				}

				if (NumberSet.Contains(InputArray[i, j]))
				{
					if (!previousWasNumber)
						count.Add(FindNumber(i, j));
					previousWasNumber = true;
				}
				else
					previousWasNumber = false;
			}
			previousWasNumber = false;
		}

		return [.. count];
	}

	private int FindNumber(int x, int y)
	{
		while (true)
		{
			if (y < 0)
				return Convert.ToInt32(FindEnd(x, 0));

			if (NumberSet.Contains(InputArray[x, y]))
				y--;
			else
				return Convert.ToInt32(FindEnd(x, y + 1));
		}
	}
}
