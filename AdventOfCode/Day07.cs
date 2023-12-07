using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class Day07(string input) : IAdventDay
{
	private static readonly string cardOrder = "AKQJT98765432";
	private static readonly string cardOrderWild = "AKQT98765432J";
	private record Hand
	{
		public string Cards { get; }
		public int Wager { get; }
		public string CardsWild { get; }

		public Hand(string cards, int wager)
		{
			Cards = cards;
			Wager = wager;
			SortingDetails = CalculateSort(Cards);
			CardsWild = CalculateWild();
			SortingDetailsWild = CalculateSort(CardsWild);
		}

		private static (char card, int count)[] CalculateSort(string cards)
			=> cards.GroupBy(g => g).OrderByDescending(o => o.Count()).Select(s => (s.Key, s.Count())).ToArray();

		private string CalculateWild()
		{
			var jokers = Cards.Where(c => c == 'J').Count();
			var newOrder = string.Join("", SortingDetails
				.Where(w => w.card != 'J')
				.OrderByDescending(o => o.count)
				.ThenBy(t => cardOrderWild.IndexOf(t.card))
				.Select(s => new string(s.card, s.count)));
			return newOrder.Length == 0 ? new string('J', jokers) : new string(newOrder[0], jokers) + newOrder;
		}

		public (char card, int count)[] SortingDetails { get; }
		public (char card, int count)[] SortingDetailsWild { get; }
	}
	private Hand[] InputArray { get; } = input.Split("\n").Select(s =>
	{
		var split = s.Split(" ");
		return new Hand(split[0], int.Parse(split[1]));
	}).ToArray();

	private class HandTypeComparer : IComparer<Hand>
	{
		public int Compare(Hand? a, Hand? b)
		{
			if (a == null || b == null)
				return 0;

			if (a.SortingDetails[0].count == b.SortingDetails[0].count)
			{
				if (a.SortingDetails[1].count == b.SortingDetails[1].count)
				{
					for (var i = 0; i < a.Cards.Length; i++)
					{
						if (a.Cards[i] == b.Cards[i])
							continue;
						return cardOrder.IndexOf(a.Cards[i]) < cardOrder.IndexOf(b.Cards[i]) ? -1 : 1;
					}

					return 0;
				}

				return a.SortingDetails[1].count > b.SortingDetails[1].count ? -1 : 1;
			}

			return a.SortingDetails[0].count > b.SortingDetails[0].count ? -1 : 1;
		}
	}

	public string Part1()
	{
		var results = InputArray.OrderByDescending(t => t, new HandTypeComparer()).ToArray();

		return results.Select((item, i) => item.Wager * (i + 1)).Sum().ToString();
	}

	private class HandTypeComparerWild : IComparer<Hand>
	{
		public int Compare(Hand? a, Hand? b)
		{
			if (a == null || b == null)
				return 0;

			if (a.SortingDetailsWild[0].count == b.SortingDetailsWild[0].count)
			{
				if (a.SortingDetailsWild[0].count == 5 || a.SortingDetailsWild[1].count == b.SortingDetailsWild[1].count)
				{
					for (var i = 0; i < a.Cards.Length; i++)
					{
						if (a.Cards[i] == b.Cards[i])
							continue;
						return cardOrderWild.IndexOf(a.Cards[i]) < cardOrderWild.IndexOf(b.Cards[i]) ? -1 : 1;
					}

					return 0;
				}

				return a.SortingDetailsWild[1].count > b.SortingDetailsWild[1].count ? -1 : 1;
			}

			return a.SortingDetailsWild[0].count > b.SortingDetailsWild[0].count ? -1 : 1;
		}
	}

	public string Part2()
	{
		var results = InputArray.OrderByDescending(t => t, new HandTypeComparerWild()).ToArray();

		return results.Select((item, i) => item.Wager * (i + 1)).Sum().ToString();
	}
}
