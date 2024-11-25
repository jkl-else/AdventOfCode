using System.Drawing;

namespace ConsoleApp.Callendar.D12
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            Spot end = null!;
            var heights = Enumerable.Range('a', 'z' - 'a' + 1)
                .Select(x => (char)x)
                .Reverse()
                .ToArray();
            var input = (await ReadFileLinesAsync("Input"))
                .Select((y, iy) => y.Select((x, ix) =>
                {
                    var spot = new Spot(ix, iy, Array.IndexOf(heights, x == 'S' ? 'a' : x == 'E' ? 'z' : x));
                    if (x == 'E')
                        end = spot;
                    return spot;
                }).ToArray())
                .ToArray();

            var lowest = int.MaxValue;
            foreach (var start in input.SelectMany(y => y.Where(x => x.Height == heights.Length - 1)))
            {
                lowest = Math.Min(lowest, Trial(input.Select(y => y.Select(x => x with {Steps = 0}).ToArray()).ToArray(), end, start with {Cost = 0}));
            }
            return lowest.ToString();
        }

        internal int Trial(Spot[][] map, Spot endTemplate, Spot start)
        {
            var queue = new PriorityQueue<Spot, int>();
            queue.Enqueue(start, 0);
            Spot current;
            var end = map[endTemplate.Y][endTemplate.X];
            while ((current = queue.Dequeue()) != end)
            {
                foreach (var neighbour in GetDestinations(current, map))
                {
                    if (neighbour.Cost.HasValue && neighbour.Cost.Value <= current.Cost + neighbour.Height)
                        continue;

                    neighbour.Steps = current.Steps + 1;
                    neighbour.Cost = current.Cost + neighbour.Height;
                    queue.Enqueue(neighbour, neighbour.Cost!.Value);
                }

                if (queue.Count == 0)
                    return int.MaxValue; // can't reach end because of the rules
            }

            return end.Steps;
        }

        internal IEnumerable<Spot> GetDestinations(Spot current, Spot[][] map)
        {
            var possibleDestinations = new[]
            {
                new Point(-1, 0),
                new Point(1, 0),
                new Point(0, -1),
                new Point(0, 1)
            };
            foreach (var pd in possibleDestinations)
            {
                var yCheck = current.Y + pd.Y;
                if (map.Length <= yCheck || yCheck < 0)
                    continue;
                var xCheck = current.X + pd.X;
                if (map[0].Length <= xCheck || xCheck < 0)
                    continue;

                var spot = map[yCheck][xCheck];
                if (current.Height <= spot.Height)
                    yield return spot;
                if (current.Height - spot.Height == 1)
                    yield return spot;
            }
        }

        internal record Spot(int X, int Y, int Height)
        {
            public int? Cost { get; set; }
            public int Steps { get; set; }
        }
    }
}
