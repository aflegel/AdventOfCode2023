namespace AdventOfCode;

public class Day01(string input) : IAdventDay
{
	private string[] InputArray { get; } = [.. input.Split("\n")];

	public string Part1()
		=> InputArray.Select(s => s.Where(item => char.IsDigit(item)).ToArray())
			.Select(w => Convert.ToInt32($"{w.First()}{w.Last()}")).Sum().ToString();


	public string Part2()
		=> InputArray.Select(s =>
		{
			var list = ResolveNumbers(s).Where(s => s.index >= 0);

			var min = list.OrderBy(o => o.index).First().value;
			var max = list.OrderBy(o => o.index).Last().value;

			return Convert.ToInt32($"{min}{max}");
		}).Sum().ToString();

	private static IEnumerable<(int index, int value)> ResolveNumbers(string input)
	{
		static string iToWord(int i) => i switch
		{
			1 => "one",
			2 => "two",
			3 => "three",
			4 => "four",
			5 => "five",
			6 => "six",
			7 => "seven",
			8 => "eight",
			9 => "nine",
			_ => throw new NotImplementedException()
		};

		for (var i = 1; i < 10; i++)
		{
			yield return (input.IndexOf(i.ToString()), i);
			yield return (input.LastIndexOf(i.ToString()), i);
			yield return (input.IndexOf(iToWord(i)), i);
			yield return (input.LastIndexOf(iToWord(i)), i);
		}
	}
}
