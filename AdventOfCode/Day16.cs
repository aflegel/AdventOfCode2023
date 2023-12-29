namespace AdventOfCode;

public class Day16(string input) : IAdventDay
{
	private enum Direction { Up, Down, Left, Right }
	private record Point(int X, int Y);
	private char[,] InputArray { get; } = input.Split('\n').To2DArray();

	public string Part1()
	{
		var energized = TracePath(new Point(0, 0), Direction.Right);

		return energized.Count.ToString();
	}

	private HashSet<Point> TracePath(Point entry, Direction direction)
	{
		HashSet<Point> energized = [];
		HashSet<(Point, Direction)> uniqueTurns = [];

		bool IsPointInvalid(Point point) => point.X < 0 || point.X >= InputArray.GetLength(0) || point.Y < 0 || point.Y >= InputArray.GetLength(1);

		static Point GetNextPosition(Point i, Direction direction) => direction switch
		{
			Direction.Up => new(i.X - 1, i.Y),
			Direction.Down => new(i.X + 1, i.Y),
			Direction.Left => new(i.X, i.Y - 1),
			Direction.Right => new(i.X, i.Y + 1),
			_ => throw new Exception()
		};

		static Direction GetNextDirection(char c, Direction direction) => (c, direction) switch
		{
			('|', Direction.Left or Direction.Right) => Direction.Down,
			('-', Direction.Up or Direction.Down) => Direction.Right,
			('/', Direction.Up) => Direction.Right,
			('/', Direction.Down) => Direction.Left,
			('/', Direction.Right) => Direction.Up,
			('/', Direction.Left) => Direction.Down,
			('\\', Direction.Up) => Direction.Left,
			('\\', Direction.Down) => Direction.Right,
			('\\', Direction.Right) => Direction.Down,
			('\\', Direction.Left) => Direction.Up,
			_ => direction
		};

		void Traverse(Point entry, Direction direction)
		{
			var current = entry;
			var previousDirection = direction;
			//uses a loop instead of recursion to avoid stack overflow
			while (!IsPointInvalid(current))
			{
				energized.Add(current);
				var nextDirection = GetNextDirection(InputArray[current.X, current.Y], previousDirection);

				if (uniqueTurns.Contains((current, nextDirection)))
					return;

				if (InputArray[current.X, current.Y] == '-' && nextDirection != previousDirection)
				{
					if (!uniqueTurns.Contains((current, Direction.Left)))
					{
						uniqueTurns.Add((current, Direction.Left));
						Traverse(GetNextPosition(current, Direction.Left), Direction.Left);
					}
				}
				else if (InputArray[current.X, current.Y] == '|' && nextDirection != previousDirection)
				{
					if (!uniqueTurns.Contains((current, Direction.Up)))
					{
						uniqueTurns.Add((current, Direction.Up));
						Traverse(GetNextPosition(current, Direction.Up), Direction.Up);
					}
				}
				else if (InputArray[current.X, current.Y] is '/' or '\\')
				{
					uniqueTurns.Add((current, nextDirection));
				}

				current = GetNextPosition(current, nextDirection);
				previousDirection = nextDirection;
			}
		}

		Traverse(entry, direction);
		return energized;
	}

	public string Part2()
	{
		var x = InputArray.GetLength(0) - 1;
		var y = InputArray.GetLength(1) - 1;

		var results = Enumerable.Range(0, x + 1).Select(s => (new Point(s, 0), Direction.Right))
			.Concat(Enumerable.Range(0, y + 1).Select(s => (new Point(0, s), Direction.Down)))
			.Concat(Enumerable.Range(0, x + 1).Select(s => (new Point(s, y), Direction.Left)))
			.Concat(Enumerable.Range(0, y + 1).Select(s => (new Point(x, s), Direction.Up)))
			.Max(s => TracePath(s.Item1, s.Item2).Count);

		return results.ToString();
	}
}