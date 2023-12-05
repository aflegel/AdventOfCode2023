using System.Reflection;
using AdventOfCode;

Console.WriteLine("Enter the day for the Advent Calendar '1-24'");

if (int.TryParse(Console.ReadLine(), out var day))
{
	var daySolver = await GetDay(day);

	Console.WriteLine($"Enter the which part of Day {day}:");
	if (int.TryParse(Console.ReadLine(), out var part))
	{
		Console.WriteLine(GetPart(daySolver, part));
	}
}

async Task<IAdventDay> GetDay(int day)
{
	var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == $"Day{day:D2}")
		?? throw new NotImplementedException();

	if(type.GetInterface(nameof(IAdventDay)) is null)
		throw new InvalidCastException($"{type} does not implement interface IAdventDay");

	using var stream = new StreamReader($"Day{day:D2}.txt");
	var input = await stream.ReadToEndAsync();

	var ctor = (type?.GetConstructor([typeof(string)]))
		?? throw new EntryPointNotFoundException("Constructor not found");

	return (IAdventDay)ctor.Invoke(new[] { input.ReplaceLineEndings("\n") });
}

string GetPart(IAdventDay day, int part) => part switch
{
	1 => day.Part1(),
	2 => day.Part2(),
	_ => throw new NotImplementedException()
};
