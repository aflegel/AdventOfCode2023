namespace AdventOfCode;

public class Day17(string input) : IAdventDay
{
	private int[,] InputArray { get; } = input.Split('\n').Select(s => s.Select(ss => int.Parse(ss.ToString()))).To2DArray();

	public string Part1()
	{

		var test = ShortestPath(InputArray);

		return test.ToString();
		throw new NotImplementedException();
	}

	/// <summary>
	/// I'm trying to get Copilot to generate this for me.
	/// It's a modified version of Dijkstra's algorithm.
	/// </summary>
	/// <param name="grid"></param>
	/// <returns></returns>
	public static int ShortestPath(int[,] grid)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);
		var dist = new int[rows, cols, 5, 5]; // last dimension for lastDir and consecutive
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				for (int k = 0; k < 5; k++)
					for (int l = 0; l < 5; l++)
						dist[i, j, k, l] = int.MaxValue;
		dist[0, 0, 0, 0] = grid[0, 0]; // start at top left
		var minHeap = new SortedSet<(int dist, int x, int y, int lastDir, int consecutive)>() { (dist[0, 0, 0, 0], 0, 0, 0, 0) };
		while (minHeap.Count > 0)
		{
			var (d, x, y, lastDir, consecutive) = minHeap.Min;
			minHeap.Remove((d, x, y, lastDir, consecutive));
			foreach (var (dx, dy, dir) in new[] { (0, 1, 1), (1, 0, 2), (0, -1, 3), (-1, 0, 4) }) // right, down
			{
				// Skip if the current direction is opposite to the last direction
				if ((lastDir == 1 && dir == 3) || (lastDir == 2 && dir == 4) || (lastDir == 3 && dir == 1) || (lastDir == 4 && dir == 2))
					continue;

				int nx = x + dx, ny = y + dy;
				if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
				{
					int nd = d + grid[nx, ny];
					int nconsecutive = (dir == lastDir) ? consecutive + 1 : 1;
					if (nconsecutive <= 4 && nd < dist[nx, ny, dir, nconsecutive])
					{
						minHeap.Remove((dist[nx, ny, dir, nconsecutive], nx, ny, dir, nconsecutive));
						dist[nx, ny, dir, nconsecutive] = nd;
						minHeap.Add((nd, nx, ny, dir, nconsecutive));
					}
				}
			}
		}
		int minDist = int.MaxValue;
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				minDist = Math.Min(minDist, dist[rows - 1, cols - 1, i, j]); // end at bottom right
		return minDist;
	}

	public string Part2()
	{
		throw new NotImplementedException();
	}
}

