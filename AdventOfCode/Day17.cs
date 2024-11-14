namespace AdventOfCode;

public class Day17(string input) : IAdventDay
{
	private int[,] InputArray { get; } = input.Split('\n').Select(s => s.Select(ss => int.Parse(ss.ToString()))).To2DArray();
	private enum Direction { Left, Right, Up, Down }

	public string Part1()
	{
		var test = Part1ModifiedDijkstras(InputArray);

		return test.ToString();
	}

	private static int Part1ModifiedDijkstras(int[,] grid)
	{
		var rows = grid.GetLength(0);
		var cols = grid.GetLength(1);
		var dist = new int[rows, cols];
		for (var i = 0; i < rows; i++)
			for (var j = 0; j < cols; j++)
				dist[i, j] = int.MaxValue;
		dist[0, 0] = grid[0, 0];

		//tuples over record/object because of equality issues
		var minHeap = new SortedSet<(int distance, (int x, int y) coords, Direction dir, int consecutive)>() { new(dist[0, 0], (0, 0), Direction.Right, 0) };
		var history = new HashSet<(int distance, (int x, int y) coords, Direction dir, int consecutive)>() { new(dist[0, 0], (0, 0), Direction.Right, 0) };

		while (minHeap.Count > 0)
		{
			var item = minHeap.Min;
			// Console.WriteLine($"Current: {item.dir}[{item.consecutive}] {item.coords} = {item.distance}");

			minHeap.Remove(item);
			history.Add(item);

			foreach (var (dx, dy, dir) in new[] { (0, 1, Direction.Right), (1, 0, Direction.Down), (0, -1, Direction.Left), (-1, 0, Direction.Up) }) // right, down, left, up
			{
				// Skip if the current direction is opposite to the last direction
				if ((item.dir == Direction.Right && dir == Direction.Left)
					|| (item.dir == Direction.Down && dir == Direction.Up)
					|| (item.dir == Direction.Left && dir == Direction.Right)
					|| (item.dir == Direction.Up && dir == Direction.Down))
					continue;

				var nconsecutive = (dir == item.dir) ? item.consecutive + 1 : 0;
				if (nconsecutive > 2)
					continue;

				int nx = item.coords.x + dx, ny = item.coords.y + dy;
				if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
				{
					if (nx == rows - 1 && ny == cols - 1)
					{
						return item.distance + grid[nx, ny] - grid[0, 0];
					}

					var nd = item.distance + grid[nx, ny];
					// Console.WriteLine($"Checking: {dir}[{nconsecutive}] ({nx}, {ny}) = {nd}");
					if (history.Contains(new(nd, (nx, ny), dir, nconsecutive)))
						continue;

					//aggressively review previous steps
					minHeap.Add(new(nd, (nx, ny), dir, nconsecutive));

					if (nd <= dist[nx, ny])
					{
						dist[nx, ny] = nd;
					}
				}
			}


		}

		return dist[rows - 1, cols - 1] - grid[0, 0];
	}

	private static void PrintDistance(int[,] dist, int[,] grid, (int, int) coords, Direction dir)
	{
		var rows = dist.GetLength(0);
		var cols = dist.GetLength(1);

		for (var i = 0; i < rows; i++)
		{
			for (var j = 0; j < cols; j++)
			{
				Console.Write($"{grid[i, j]}(");
				if (dist[i, j] <= 200)
				{
					var x = (i == coords.Item1 && j == coords.Item2) ? GetDirection(dir) : "";
					Console.Write($"{dist[i, j]}){x}\t");
				}
				else
					Console.Write("?)\t");
			}
			Console.WriteLine();
		}
	}

	private static string GetDirection(Direction dir) => dir switch
	{
		Direction.Left => "<",
		Direction.Right => ">",
		Direction.Up => "^",
		Direction.Down => "v",
		_ => throw new NotImplementedException()
	};

	public string Part2()
	{
		var test = Part2ModifiedDijkstras(InputArray);

		return test.ToString();
	}


	private static int Part2ModifiedDijkstras(int[,] grid)
	{
		var rows = grid.GetLength(0);
		var cols = grid.GetLength(1);
		var dist = new int[rows, cols];
		for (var i = 0; i < rows; i++)
			for (var j = 0; j < cols; j++)
				dist[i, j] = int.MaxValue;
		dist[0, 0] = grid[0, 0];

		//tuples over record/object because of equality issues
		var minHeap = new SortedSet<(int distance, (int x, int y) coords, Direction dir, int consecutive)>() { new(dist[0, 0], (0, 0), Direction.Right, 0) };
		var history = new HashSet<(int distance, (int x, int y) coords, Direction dir, int consecutive)>() { new(dist[0, 0], (0, 0), Direction.Right, 0) };

		while (minHeap.Count > 0)
		{
			var item = minHeap.Min;
			// Console.WriteLine($"Current: {item.dir}[{item.consecutive}] {item.coords} = {item.distance}");

			minHeap.Remove(item);
			history.Add(item);

			foreach (var (dx, dy, dir) in new[] { (0, 1, Direction.Right), (1, 0, Direction.Down), (0, -1, Direction.Left), (-1, 0, Direction.Up) }) // right, down, left, up
			{
				// Skip if the current direction is opposite to the last direction
				if ((item.dir == Direction.Right && dir == Direction.Left)
					|| (item.dir == Direction.Down && dir == Direction.Up)
					|| (item.dir == Direction.Left && dir == Direction.Right)
					|| (item.dir == Direction.Up && dir == Direction.Down))
					continue;

				var nconsecutive = (dir == item.dir) ? item.consecutive + 1 : 0;
				if (nconsecutive < 4)
					continue;

				int nx = item.coords.x + dx, ny = item.coords.y + dy;
				if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
				{
					if (nx == rows - 1 && ny == cols - 1)
					{
						return item.distance + grid[nx, ny] - grid[0, 0];
					}

					var nd = item.distance + grid[nx, ny];
					// Console.WriteLine($"Checking: {dir}[{nconsecutive}] ({nx}, {ny}) = {nd}");
					if (history.Contains(new(nd, (nx, ny), dir, nconsecutive)))
						continue;

					//aggressively review previous steps
					minHeap.Add(new(nd, (nx, ny), dir, nconsecutive));

					if (nd <= dist[nx, ny])
					{
						dist[nx, ny] = nd;
					}
				}
			}


		}

		return dist[rows - 1, cols - 1] - grid[0, 0];
	}

}

