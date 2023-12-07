namespace AdventOfCode;

public class Day06 : IAdventDay
{
	private record Race(long Time, long Record);
	private Race[] InputArray { get; }
	private Race InputRace { get; }

	public Day06(string input)
	{
		var splits = input.Split("\n");

		var times = splits[0].Split(":")[1].Split(" ").Where(w => w.Length != 0).Select(s => Convert.ToInt64(s)).ToArray();
		var distances = splits[1].Split(":")[1].Split(" ").Where(w => w.Length != 0).Select(s => Convert.ToInt64(s)).ToArray();

		InputArray = times.Select((item, i) => new Race(item, distances[i])).ToArray();

		//special for part two
		InputRace = new Race(
			Convert.ToInt64(splits[0].Split(":")[1].Replace(" ", "")),
			Convert.ToInt64(splits[1].Split(":")[1].Replace(" ", "")));
	}

	public string Part1()
	{
		var results = InputArray.Select(s => Enumerable.Range(0, (int)s.Time).Select(i => i * (s.Time - i)).Where(w => w > s.Record)).ToArray();

		return results.Aggregate(1, (s, n) => s * n.Count()).ToString();
	}

	public string Part2()
	{
		/*
		 true when:
		 X * (time - X) > Y
		 -X^2 + X*Time > Y
		 -X^2 + X*Time - Y > 0

		 ax^2 + bx + c = 0
		 */

		var a = -1;
		var b = InputRace.Time;
		var c = -InputRace.Record;

		var discriminant = (b * b) - (4 * a * c);

		if (discriminant > 0)
		{
			var root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
			var root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

			var result = Math.Ceiling(root2) - Math.Ceiling(root1);

			return result.ToString();
		}
		else if (discriminant == 0)
		{
			//one root
			throw new NotImplementedException();
		}
		else
		{
			//no roots
			throw new NotImplementedException();
		}
	}
}
