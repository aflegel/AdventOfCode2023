using AdventOfCode;

Console.WriteLine("Enter the day for the Advent Calendar '1-24'");

if (int.TryParse(Console.ReadLine(), out var day))
{
	using var stream = new StreamReader($"Day{day:D2}.txt");
	var input = await stream.ReadToEndAsync();

	var daySolver = GetDay(day, input);

	Console.WriteLine($"Enter the which part of Day {day}:");
	if (int.TryParse(Console.ReadLine(), out var part))
	{
		Console.WriteLine(GetPart(daySolver, part));
	}
}

IAdventDay GetDay(int day, string input) => day switch
{
	1 => new Day01(input),
	_ => throw new NotImplementedException()
};

string GetPart(IAdventDay day, int part) => part switch
{
	1 => day.Part1(),
	2 => day.Part2(),
	_ => throw new NotImplementedException()
};
