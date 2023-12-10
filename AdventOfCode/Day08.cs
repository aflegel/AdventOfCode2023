using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class Day08 : IAdventDay
{
	private record Node(string Id, string Left, string Right)
	{
		public Node LeftNode { get; set; }
		public Node RightNode { get; set; }
	}
	private string InputInstructions { get; }
	private Node[] InputArray { get; }

	public Day08(string input)
	{
		var lines = input.Split("\n\n");
		InputInstructions = lines[0];

		InputArray = lines[1].Split("\n").Select(s =>
		{
			var split = s.Split(" = ");
			var instructions = split[1].Replace("(", "").Replace(")", "").Split(", ");
			return new Node(split[0], instructions[0], instructions[1]);
		}).ToArray();

		foreach (var node in InputArray)
		{
			node.LeftNode = InputArray.Single(n => n.Id == node.Left);
			node.RightNode = InputArray.Single(n => n.Id == node.Right);
		}
	}

	public string Part1()
	{
		var i = 0;
		var result = 0;
		var currentNode = InputArray.Single(s => s.Id == "AAA");
		while (true)
		{
			if (i >= InputInstructions.Length)
				i = 0;

			currentNode = InputInstructions[i++] == 'L' ? currentNode.LeftNode : currentNode.RightNode;
			result++;

			if (currentNode.Id == "ZZZ")
				return result.ToString();
		}
	}

	public string Part2()
	{
		var startNodes = InputArray.Where(s => s.Id[^1] == 'A').ToArray();

		var routesToEnd = startNodes.Select(FindEnd).ToArray();

		return FindLowestCommonMultiple(routesToEnd).ToString();
	}

	private int FindEnd(Node startNode)
	{
		var i = 0;
		var result = 0;
		var currentNode = startNode;
		while (true)
		{
			if (i >= InputInstructions.Length)
				i = 0;

			currentNode = InputInstructions[i++] == 'L' ? currentNode.LeftNode : currentNode.RightNode;
			result++;

			if (currentNode.Id[^1] == 'Z')
				return result;
		}
	}

	//I googled the LCM function, original sauce is https://www.geeksforgeeks.org/lcm-of-given-array-elements/
	private static long FindLowestCommonMultiple(int[] elements)
	{
		long lcm = 1;
		var divisor = 2;

		while (true)
		{
			var counter = 0;
			var divisible = false;
			for (var i = 0; i < elements.Length; i++)
			{

				// lcm_of_array_elements (n1, n2, ... 0) = 0.
				// For negative number we convert into
				// positive and calculate lcm_of_array_elements.
				if (elements[i] == 0)
					return 0;
				else if (elements[i] < 0)
					elements[i] = elements[i] * -1;
				if (elements[i] == 1)
					counter++;

				// Divide element_array by devisor if complete
				// division i.e. without remainder then replace
				// number with quotient; used for find next factor
				if (elements[i] % divisor == 0)
				{
					divisible = true;
					elements[i] = elements[i] / divisor;
				}
			}

			// If divisor able to completely divide any number
			// from array multiply with lcm_of_array_elements
			// and store into lcm_of_array_elements and continue
			// to same divisor for next factor finding.
			// else increment divisor
			if (divisible)
			{
				lcm *= divisor;
			}
			else
			{
				divisor++;
			}

			// Check if all element_array is 1 indicate 
			// we found all factors and terminate while loop.
			if (counter == elements.Length)
			{
				return lcm;
			}
		}
	}
}
