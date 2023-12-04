namespace AdventOfCode;

public class Day02(string input) : IAdventDay
{
	private record Draw(int Red, int Green, int Blue);
	private record Game(int Id, Draw[] Draws);

	private Game[] InputArray { get; } = input.Split("\n").Select(s =>
	{
		static string ParseColour(string[] s, string colour) => s.FirstOrDefault(w => w.Contains(colour))?.Replace(colour, "") ?? "0";

		var split = s.Split(":");

		var id = Convert.ToInt32(split[0].Replace("Game", ""));

		var list = split[1].Split(";").Select(s =>
		{
			var split2 = s.Split(",");

			var r = ParseColour(split2, "red");
			var b = ParseColour(split2, "blue");
			var g = ParseColour(split2, "green");

			return new Draw(Convert.ToInt32(r), Convert.ToInt32(g), Convert.ToInt32(b));
		}).ToArray();

		return new Game(id, list);
	}).ToArray();

	public string Part1()
	{
		var yes = InputArray.Where(w => !w.Draws.Any(a => a.Red > 12 || a.Blue > 14 || a.Green > 13)).Select(s => s.Id).ToArray();

		return yes.Sum().ToString();
	}

	public string Part2()
	{
		var powers = InputArray.Select(s => s.Draws.Max(s => s.Red) * s.Draws.Max(s => s.Blue) * s.Draws.Max(s => s.Green)).ToArray();

		return powers.Sum().ToString();
	}
}
