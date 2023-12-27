namespace AdventOfCode;

public class Day15(string input) : IAdventDay
{
	private string[] InputArray { get; } = [.. input.Split(",")];
	private static readonly char[] separator = ['-', '='];

	public string Part1()
	{
		var result = InputArray.Select(Hash).Sum();
		return result.ToString();
	}

	private static int Hash(string s) => s.Aggregate(0, (a, b) => (a + b) * 17 % 256);

	public string Part2()
	{
		static List<List<string>> MakeBoxes(string[] instructions)
		{
			List<List<string>> boxes = [];
			for (var i = 0; i < 256; i++)
				boxes.Add([]);

			foreach (var instruction in instructions)
			{
				var split = instruction.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				var hash = Hash(split[0]);

				if (instruction.Contains('-') && boxes[hash].SingleOrDefault(a => a.Contains(split[0])) is string toRemove)
					boxes[hash].Remove(toRemove);
				else if (instruction.Contains('='))
				{
					if (boxes[hash].SingleOrDefault(a => a.Contains(split[0])) is string existing)
						boxes[hash][boxes[hash].IndexOf(existing)] = instruction;
					else
						boxes[hash].Add(instruction);
				}
			}
			return boxes;
		}

		var boxes = MakeBoxes(InputArray);

		var result = boxes.SelectMany((box, index) => box.Select((lens, position) =>
		{
			var split = lens.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			return (index + 1) * (position + 1) * int.Parse(split[1]);
		})).Sum();
	
		return result.ToString();
	}
}