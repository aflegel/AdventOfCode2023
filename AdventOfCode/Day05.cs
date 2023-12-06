namespace AdventOfCode;

public class Day05 : IAdventDay
{
	private record RangeMap(long TargetIndex, long SourceIndex, int Length)
	{
		public long Difference { get; } = SourceIndex - TargetIndex;
	}
	private record Map(RangeMap[] Ranges);
	private record SeedPlan(long Seed)
	{
		public long Soil { get; set; } = 0;
		public long Fertilizer { get; set; } = 0;
		public long Water { get; set; } = 0;
		public long Light { get; set; } = 0;
		public long Temperature { get; set; } = 0;
		public long Humidity { get; set; } = 0;
		public long Location { get; set; } = 0;
	}

	private record Range(long Start, long End);
	private record SeedPlanRange(Range Seed)
	{
		public Range[] Soil { get; set; } = [];
		public Range[] Fertilizer { get; set; } = [];
		public Range[] Water { get; set; } = [];
		public Range[] Light { get; set; } = [];
		public Range[] Temperature { get; set; } = [];
		public Range[] Humidity { get; set; } = [];
		public Range[] Location { get; set; } = [];
	}

	private SeedPlan[] Seeds { get; }
	private Map[] InputArray { get; }

	public Day05(string input)
	{
		var splits = input.Split("\n\n");

		Seeds = splits[0].Split(": ")[1].Split(" ").Select(s => new SeedPlan(Convert.ToInt64(s))).ToArray();

		InputArray = splits.Skip(1)
						.Select(s => new Map(s.Split("\n").Skip(1).Select(ss =>
							{
								var ranges = ss.Split(" ").ToArray();

								return new RangeMap(Convert.ToInt64(ranges[0]), Convert.ToInt64(ranges[1]), Convert.ToInt32(ranges[2]));
							}).ToArray())).ToArray();
	}

	public string Part1()
	{
		for (var i = 0; i < InputArray.Length; i++)
		{
			foreach (var seed in Seeds)
			{
				switch (i)
				{
					case 0:
						seed.Soil = ProcessMap(seed.Seed, InputArray[i].Ranges);
						break;
					case 1:
						seed.Fertilizer = ProcessMap(seed.Soil, InputArray[i].Ranges);
						break;
					case 2:
						seed.Water = ProcessMap(seed.Fertilizer, InputArray[i].Ranges);
						break;
					case 3:
						seed.Light = ProcessMap(seed.Water, InputArray[i].Ranges);
						break;
					case 4:
						seed.Temperature = ProcessMap(seed.Light, InputArray[i].Ranges);
						break;
					case 5:
						seed.Humidity = ProcessMap(seed.Temperature, InputArray[i].Ranges);
						break;
					case 6:
						seed.Location = ProcessMap(seed.Humidity, InputArray[i].Ranges);
						break;
				}
			}
		}
		return Seeds.Min(m => m.Location).ToString();
	}

	private static long ProcessMap(long current, RangeMap[] ranges)
	{
		if (ranges.FirstOrDefault(f => current >= f.SourceIndex && current < f.SourceIndex + f.Length) is RangeMap map)
			return current - map.Difference;
		else
			return current;
	}

	public string Part2()
	{
		var reseed = Seeds.Select(s => s.Seed).Chunk(2).Select(s => new SeedPlanRange(new(s[0], s[0] + s[1] - 1))).ToArray();

		for (var i = 0; i < InputArray.Length; i++)
		{
			foreach (var seed in reseed)
			{
				switch (i)
				{
					case 0:
						seed.Soil = ProcessMap([seed.Seed], InputArray[i].Ranges);
						break;
					case 1:
						seed.Fertilizer = ProcessMap(seed.Soil, InputArray[i].Ranges);
						break;
					case 2:
						seed.Water = ProcessMap(seed.Fertilizer, InputArray[i].Ranges);
						break;
					case 3:
						seed.Light = ProcessMap(seed.Water, InputArray[i].Ranges);
						break;
					case 4:
						seed.Temperature = ProcessMap(seed.Light, InputArray[i].Ranges);
						break;
					case 5:
						seed.Humidity = ProcessMap(seed.Temperature, InputArray[i].Ranges);
						break;
					case 6:
						seed.Location = ProcessMap(seed.Humidity, InputArray[i].Ranges);
						break;
				}
			}
		}
		var test = reseed.SelectMany(s => s.Location.Select( s => s.Start)).ToArray();

		return test.Min().ToString();
	}

	private static Range Migrate(Range range, long diff) => new(range.Start - diff, range.End - diff);


	private static Range[] ProcessMap(Range[] current, RangeMap[] maps)
	{
		var remapped = new List<Range>();
		var unmapped = new List<Range>(current);

		foreach (var map in maps)
		{
			var i = 0;
			while (i < unmapped.Count)
			{
				var range = unmapped[i++];

				// map is entirely before range
				if (map.SourceIndex + map.Length - 1 < range.Start)
					continue;

				// map is entirely after range
				if (map.SourceIndex > range.End)
					continue;

				//past here there is an intersect and we will split this entry
				//The unmapped entry will be searched by other maps
				i--;
				unmapped.RemoveAt(i);

				//map starts and ends outside of range
				if (map.SourceIndex <= range.Start && map.SourceIndex + map.Length >= range.End)
				{
					remapped.Add(Migrate(range, map.Difference));
					continue;
				}

				if (map.SourceIndex > range.Start)
				{
					// map starts inside range and ends outside range
					if (map.SourceIndex + map.Length > range.End)
					{
						unmapped.Add(new Range(range.Start, map.SourceIndex - 1));
						remapped.Add(Migrate(new Range(map.SourceIndex, range.End), map.Difference));
					}
					// map starts inside range and ends inside range
					else
					{
						//three segments
						unmapped.Add(new Range(range.Start, map.SourceIndex - 1));
						remapped.Add(Migrate(new Range(map.SourceIndex, map.SourceIndex + map.Length), map.Difference));
						unmapped.Add(new Range(map.SourceIndex + map.Length, range.End));
					}
				}
				// map starts outside range and ends inside range
				else
				{
					remapped.Add(Migrate(new Range(range.Start, map.SourceIndex + map.Length - 1), map.Difference));
					unmapped.Add(new Range(map.SourceIndex + map.Length, range.End));
				}
			}
		}

		return unmapped.Union(remapped).ToArray();
	}
}
