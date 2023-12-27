using System.Text;

namespace AdventOfCode;

public class Day14(string input) : IAdventDay
{
	private enum Direction { North, East, South, West }
	private char[,] InputArray { get; } = input.Split("\n").To2DArray();

	public string Part1()
	{
		var grid = Tilt(InputArray, Direction.North);
		var result = CalculateLoad(grid);

		return result.ToString();
	}

	private static char[,] Tilt(char[,] grid, Direction direction)
	{
		void MoveRock(int x, int y, Direction direction)
		{
			grid[x, y] = '.';

			switch (direction)
			{
				case Direction.North:
					while (x > 0 && grid[x - 1, y] == '.')
						x--;
					break;
				case Direction.South:
					while (x < grid.GetLength(0) - 1 && grid[x + 1, y] == '.')
						x++;
					break;
				case Direction.East:
					while (y < grid.GetLength(1) - 1 && grid[x, y + 1] == '.')
						y++;
					break;
				case Direction.West:
					while (y > 0 && grid[x, y - 1] == '.')
						y--;
					break;
			}

			grid[x, y] = 'O';
		}

		if (direction is Direction.North or Direction.West)
		{
			for (var x = 0; x < grid.GetLength(0); x++)
			{
				for (var y = 0; y < grid.GetLength(1); y++)
				{
					if (grid[x, y] == 'O')
					{
						MoveRock(x, y, direction);
					}
				}
			}
		}
		else
		{
			for (var x = grid.GetLength(0) - 1; x >= 0; x--)
			{
				for (var y = grid.GetLength(1) - 1; y >= 0; y--)
				{
					if (grid[x, y] == 'O')
					{
						MoveRock(x, y, direction);
					}
				}
			}
		}

		return grid;
	}

	private static int CalculateLoad(char[,] grid)
	{
		var load = 0;

		for (var x = 0; x < grid.GetLength(0); x++)
		{
			for (var y = 0; y < grid.GetLength(1); y++)
			{
				if (grid[x, y] == 'O')
					load += grid.GetLength(0) - x;
			}
		}

		return load;
	}

	public string Part2()
	{
		static int GetPatternMath(char[,] grid)
		{
			List<string> pattern = [];
			string? current;
			while (true)
			{
				grid = Cycle(grid);
				current = grid.MakeString();
				if (pattern.Contains(current))
					break;
				else
					pattern.Add(current);
			}

			var index = pattern.IndexOf(current);
			return ((1000000000 - index) % (pattern.Count - index)) - 1;
		}

		var grid = InputArray;
		var math = GetPatternMath(grid);
		for (var i = 0; i < math; i++)
		{
			grid = Cycle(grid);
		}

		var result = CalculateLoad(grid);
		return result.ToString();
	}

	private static char[,] Cycle(char[,] grid)
	{
		Tilt(grid, Direction.North);
		Tilt(grid, Direction.West);
		Tilt(grid, Direction.South);
		Tilt(grid, Direction.East);
		return grid;
	}
}

public static class PrintExtensions
{
	public static void PrintToConsole(this char[,] array)
	{
		for (var i = 0; i < array.GetLength(0); i++)
		{
			for (var j = 0; j < array.GetLength(1); j++)
			{
				Console.Write(array[i, j]);
			}
			Console.WriteLine();
		}
	}

	public static string MakeString(this char[,] array)
	{
		var result = new StringBuilder();
		for (var i = 0; i < array.GetLength(0); i++)
		{
			for (var j = 0; j < array.GetLength(1); j++)
			{
				result.Append(array[i, j]);
			}
			result.Append('\n');
		}

		return result.ToString();
	}
}