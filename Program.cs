using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFraction
{
	class Program
	{
		static void Main()
		{
			int nodesCount;
			int edgesCount;
			InputGraph(out nodesCount, out edgesCount);
			var edges = InputEdges(edgesCount).ToList();
			var edgesOfNodes = new List<int[]>[nodesCount];

			for (int i = 0; i < edgesOfNodes.Length; i++)
				edgesOfNodes[i] = new List<int[]>();

			foreach (var edge in edges)
			{
				edgesOfNodes[edge[0]].Add(new[] { edge[2], edge[3] });
				if (edge[0] != edge[1])
					edgesOfNodes[edge[1]].Add(new[] { edge[2], edge[3] });
			}

			var fractions = edgesOfNodes.Select(eon => new Fraction(eon.Select(e => e[0]).Sum(), eon.Select(e => e[1]).Sum()))
				.OrderByDescending(f=>f.Value);

			Console.WriteLine(fractions.First());
			Console.ReadKey();
		}

		private static IEnumerable<int[]> InputEdges(int count)
		{
			for (int i = 0; i < count; i++)
				yield return InputEdge();
		}

		private static int[] InputEdge()
		{
			int[] edgeData = new int[4];
			Console.WriteLine("Please input next edge data ('source node, destination node, A, B'):");
			var input = Console.ReadLine();
			while (!TryParseEdge(input, out edgeData))
			{
				Console.WriteLine("Error. Try again (e.g. '0 1 3 4')");
				input = Console.ReadLine();
			}
			return edgeData;
		}

		private static bool TryParseEdge(string input, out int[] edgeData)
		{
			edgeData = null;
			var nums = input.Split(" ,".ToCharArray());
			if (nums.Length != 4)
				return false;
			edgeData = new int[4];
			return int.TryParse(nums[0], out edgeData[0])
				&& int.TryParse(nums[1], out edgeData[1])
				&& int.TryParse(nums[2], out edgeData[2])
				&& int.TryParse(nums[3], out edgeData[3]);
		}

		private static void InputGraph(out int nodesCount, out int edgesCount)
		{
			string[] nums;
			do
			{
				Console.WriteLine("Please input nodes count and edges count (e.g. '2 1'):");
				var input = Console.ReadLine();
				nums = input.Split(" ,".ToCharArray());
			} while (nums.Length != 2 || !int.TryParse(nums[0], out nodesCount) || !int.TryParse(nums[1], out edgesCount));
		}
	}
}
