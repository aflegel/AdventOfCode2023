using System.Data;

namespace AdventOfCode;

public class Day10(string input) : IAdventDay
{
	private record MapPoint(int X, int Y, char Value, Direction Direction);
	private char[,] InputArray { get; } = input.Split("\n").To2DArray();
	private enum Direction { Up, Down, Left, Right };

	public string Part1()
	{
		var result = FindLoop();
		return (result.Count / 2).ToString();
	}

	private List<MapPoint> FindLoop()
	{
		var start = FindStart();

		for (var i = 0; i < 4; i++)
		{
			var startingDirection = i switch
			{
				0 => Direction.Up,
				1 => Direction.Right,
				2 => Direction.Down,
				3 => Direction.Left,
				_ => throw new InvalidDataException()
			};

			try
			{
				var plusOne = GetNext(start, startingDirection);
				if (!IsConnected(plusOne))
					continue;

				if (TryFollow(plusOne, out var path))
					return path;
			}
			catch
			{
				continue;
			}
		}

		throw new InvalidDataException();
	}

	private MapPoint FindStart()
	{
		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				if (InputArray[i, j] == 'S')
				{
					return new MapPoint(i, j, 'S', Direction.Up);
				}
			}
		}

		throw new InvalidDataException();
	}

	private static bool IsConnected(MapPoint next) => next.Direction switch
	{
		Direction.Up => "|7F".Contains(next.Value),
		Direction.Down => "|LJ".Contains(next.Value),
		Direction.Left => "-FL".Contains(next.Value),
		Direction.Right => "-J7".Contains(next.Value),
		_ => throw new Exception()
	};

	private static Direction GetNextDirection(MapPoint current) => (current.Value, current.Direction) switch
	{
		('-', Direction.Left) => Direction.Left,
		('-', Direction.Right) => Direction.Right,
		('J', Direction.Right) => Direction.Up,
		('J', Direction.Down) => Direction.Left,
		('7', Direction.Right) => Direction.Down,
		('7', Direction.Up) => Direction.Left,
		('F', Direction.Left) => Direction.Down,
		('F', Direction.Up) => Direction.Right,
		('L', Direction.Left) => Direction.Up,
		('L', Direction.Down) => Direction.Right,
		('|', Direction.Up) => Direction.Up,
		('|', Direction.Down) => Direction.Down,
		_ => throw new Exception()
	};

	private static (int X, int Y) GetNextPosition(MapPoint i, Direction direction) => direction switch
	{
		Direction.Up => (i.X - 1, i.Y),
		Direction.Down => (i.X + 1, i.Y),
		Direction.Left => (i.X, i.Y - 1),
		Direction.Right => (i.X, i.Y + 1),
		_ => throw new Exception()
	};

	private MapPoint GetNext(MapPoint i, Direction? setDirection = null)
	{
		var dir = setDirection ?? GetNextDirection(i);
		var (x, y) = GetNextPosition(i, dir);

		return new MapPoint(x, y, InputArray[x, y], dir);
	}

	private bool TryFollow(MapPoint current, out List<MapPoint> path)
	{
		path = [];
		var next = current;
		do
		{
			path.Add(next);
			next = GetNext(next);

			if (next.Value == 'S')
			{
				path.Add(next);
				return true;
			}
			else if (!IsConnected(next))
				return false;
		}
		while (next != null);

		return false;
	}

	public string Part2()
	{
		var loop = FindLoop();
		var result = new List<MapPoint>();

		FixStartSymbol(loop);

		// do the even-odd rule. reference: https://en.wikipedia.org/wiki/Even%E2%80%93odd_rule
		for (var x = 0; x < InputArray.GetLength(0); x++)
		{
			for (var y = 0; y < InputArray.GetLength(1); y++)
			{
				//escape if the point is in the loop
				if (loop.Any(a => a.X == x && a.Y == y))
					continue;
				var item = new MapPoint(x, y, InputArray[x, y], Direction.Up);
				var dir = GetEscapeDirection(item);
				if (CountDistinctSegments(loop, item, dir) % 2 == 1)
					result.Add(item);
			}
		}

		return result.Count.ToString();
	}

	private Direction GetEscapeDirection(MapPoint point)
	{
		var halfX = InputArray.GetLength(0) / 2;
		var halfY = InputArray.GetLength(1) / 2;

		return (point.X, point.Y) switch
		{
			var (x, y) when x < halfX && y < halfY => Direction.Up,
			var (x, y) when x < halfX && y >= halfY => Direction.Right,
			var (x, y) when x >= halfX && y >= halfY => Direction.Down,
			var (x, y) when x >= halfX && y < halfY => Direction.Left,
			_ => throw new InvalidDataException()
		};
	}

	private bool IsPointInvalid(MapPoint point)
		=> point.X <= 0 || point.X >= InputArray.GetLength(0) - 1 || point.Y <= 0 || point.Y >= InputArray.GetLength(1) - 1;

	private int CountDistinctSegments(List<MapPoint> loop, MapPoint point, Direction direction)
	{
		if (IsPointInvalid(point))
			return 0;

		var count = 0;
		var previous = point;
		Direction? saveCornerDirection = null;
		var next = GetNext(point, direction);
		while (next != null)
		{
			//I think this needs to amend the loop to replace S with its relevant shape

			//current is part of the loop
			if (loop.Any(a => a.X == next.X && a.Y == next.Y))
			{
				//if we are currently traversing a parallel segment
				if (saveCornerDirection is not null)
				{
					if (TryFindCorner(next, direction, out var endCorner))
					{
						if (saveCornerDirection == endCorner)
						{
							//this is not an enclosing parallel
							saveCornerDirection = null;
						}
						else
						{
							//this is an enclosing parallel
							count++;

						}
					}

				}
				
				else if (TryFindCorner(next, direction, out var corner))
				{
					//we are starting a parallel segment
					saveCornerDirection ??= corner;
					//continue;
				}
				else if (Math.Abs(loop.IndexOf(next) - loop.IndexOf(previous)) != 1)
				{
					//throw new Exception();
					saveCornerDirection = null;
					count++;
					//continue;
				}
				else
				{
					//there is no parallel segment
					count++;
				}
			}

			//detects if the next point will throw
			if (IsPointInvalid(next))
				return count;

			previous = next;
			next = GetNext(next, direction);
		}

		return count;
	}

	private static bool TryFindCorner(MapPoint point, Direction direction, out Direction corner)
	{
		corner = (point.Value, direction) switch
		{
			('F', Direction.Left or Direction.Right) => Direction.Down,
			('F', Direction.Up or Direction.Down) => Direction.Right,
			('J', Direction.Left or Direction.Right) => Direction.Up,
			('J', Direction.Up or Direction.Down) => Direction.Left,
			('7', Direction.Left or Direction.Right) => Direction.Down,
			('7', Direction.Up or Direction.Down) => Direction.Left,
			('L', Direction.Left or Direction.Right) => Direction.Up,
			('L', Direction.Up or Direction.Down) => Direction.Right,
			('S', _) => throw new Exception(),
			_ => direction
		};

		return corner != direction;
	}

	private void FixStartSymbol(List<MapPoint> loop)
	{
		var s = loop.Single(a => a.Value == 'S');

		var first = loop[0];
		var last = loop[^2];

		var corectedValue = (first.X - last.X, first.Y - last.Y) switch
		{
			(0, _) => '-',
			(_, 0) => '|',
			(-1, -1) => 'L',
			(-1, 1) => 'F',
			(1, -1) => 'J',
			(1, 1) => '7',
			_ => throw new Exception()
		};

		InputArray[s.X, s.Y] = corectedValue;
	}
}

internal static class EnumerableExtensions
{
	/**
	Source https://gist.github.com/kekyo/2e0c456f506ec31431f33741608d5230
	*/
	public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
	{
		var data = source
			.Select(x => x.ToArray())
			.ToArray();

		var res = new T[data.Length, data.Max(x => x.Length)];
		for (var i = 0; i < data.Length; ++i)
		{
			for (var j = 0; j < data[i].Length; ++j)
			{
				res[i, j] = data[i][j];
			}
		}

		return res;
	}
}