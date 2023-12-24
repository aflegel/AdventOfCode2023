using System.Text.RegularExpressions;

namespace AdventOfCode;

public partial class Day12(string input) : IAdventDay
{
	private partial record SpringGroup(string Group, int[] Pattern)
	{
		public bool IsValid()
		{
			var test = SplitWhenCharactersChange(Group).Where(s => s.Contains('#')).ToArray();
			if (test.Length != Pattern.Length)
				return false;

			for (var i = 0; i < test.Length; i++)
			{
				if (test[i].Length != Pattern[i])
					return false;
			}
			return true;
		}

		public bool IsPartiallyValid()
		{
			var test = SplitWhenCharactersChange(Group).ToArray();
			var patternIndex = 0;

			for (var i = 0; i < test.Length; i++)
			{
				if (test[i].Contains('?'))
					return true;

				if (test[i].Contains('#') && patternIndex < Pattern.Length && test[i].Length != Pattern[patternIndex])
				{
					//if the next group is unknown it may contain valid input
					if (i < test.Length - 1 && test[i + 1].Contains('?'))
						return true;
					//Console.WriteLine($"{Group} - {string.Join(',', Pattern)}");
					return false;
				}
				else if (test[i].Contains('#'))
					patternIndex++;
			}
			return true;
		}

		private static string[] SplitWhenCharactersChange(string input)
			=> CharacterSplit().Matches(input)
				.Cast<Match>()
				.Select(m => m.Value).ToArray();

		[GeneratedRegex(@"(.)\1*")]
		private static partial Regex CharacterSplit();

	}
	private List<SpringGroup> InputArray { get; } = input.Split("\n").Select(s =>
	{
		var split = s.Split(" ");
		return new SpringGroup(split[0], split[1].Split(",").Select(int.Parse).ToArray());
	}).ToList();

	public string Part1()
	{
		var groups = InputArray.SelectMany(CompleteGroups).ToList();
		var valid = groups.Where(g => g.IsValid()).ToList();
		return valid.Count.ToString();
	}

	private IEnumerable<SpringGroup> CompleteGroups(SpringGroup group)
	{
		static string ReplaceFirst(string text, string search, string replace)
		{
			var pos = text.IndexOf(search);
			return pos < 0 ? text : text[..pos] + replace + text[(pos + search.Length)..];
		}

		if (!group.Group.Contains('?', StringComparison.CurrentCulture))
			yield return group;
		else
		{
			var a = new SpringGroup(ReplaceFirst(group.Group, "?", "."), group.Pattern);
			var b = new SpringGroup(ReplaceFirst(group.Group, "?", "#"), group.Pattern);

			if (a.IsPartiallyValid())
				foreach (var g in CompleteGroups(a))
					yield return g;

			if (b.IsPartiallyValid())
				foreach (var g in CompleteGroups(b))
					yield return g;
		}
	}

	public string Part2()
	{
		var data = GetUnfoldedResults();

		var groups = data.SelectMany(CompleteGroups).ToList();
		var valid = groups.Where(g => g.IsValid()).ToList();
		return valid.Count.ToString();
	}

	private List<SpringGroup> GetUnfoldedResults()
	{
		var groups = InputArray.Select(s => new SpringGroup(string.Join("?", Enumerable.Repeat(s.Group, 5)), Enumerable.Repeat(s.Pattern, 5).SelectMany(s => s).ToArray())).ToList();

		return groups;
	}

}