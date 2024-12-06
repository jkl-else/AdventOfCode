namespace ConsoleApp.Calendar.D05
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync() // 6191
        {
            var input = await ReadFileLinesAsync("Input");
            var ordering = input.TakeWhile(x => x != string.Empty)
                .Select(x => x.Split('|').Select(int.Parse).ToArray())
                .ToList();
            var pages = input.Skip(ordering.Count + 1)
                .Select(x => x.Split(',').Select(int.Parse).ToList())
                .ToList();
            int result = 0;
            foreach (var page in pages)
            {
                if (!UnSorted(page, ordering))
                    continue;

                var ordered = OrderPages(page, ordering);
                var midValue = ordered[ordered.Count / 2];
                result += midValue;
            }
            return result.ToString();
        }

        private bool UnSorted(List<int> page, IEnumerable<int[]> ordering) =>
            (from o in ordering
                let firstIndex = page.IndexOf(o[0])
                where firstIndex != -1
                let secondIndex = page.IndexOf(o[1])
                where secondIndex != -1 && firstIndex >= secondIndex
                select firstIndex).Any();

        private List<int> OrderPages(List<int> pages, List<int[]> ordering) // Kahn's algorithm
        {
            var dependencyGraph = pages.ToDictionary(x => x, _ => new List<int>());
            var inDegree = pages.ToDictionary(x => x, _ => 0);
            // Build the graph and update in-degrees based on ordering
            foreach (var order in ordering)
            {
                var first = order[0];
                var second = order[1];

                if (!dependencyGraph.ContainsKey(first) || !dependencyGraph.ContainsKey(second)) continue;

                dependencyGraph[first].Add(second);
                inDegree[second]++;
            }

            // Topological sort using Kahn's algorithm
            var result = new List<int>();
            var queue = new Queue<int>(inDegree.Where(x => x.Value == 0).Select(x => x.Key));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in dependencyGraph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                        queue.Enqueue(neighbor);
                }
            }

            return result;
        }
    }
}
