namespace ConsoleApp.Calendar.D06
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 5131
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select((row, yi) => row.Select((c, xi) => new Coordinate(xi, yi, c == '#', c == '^')))
                .SelectMany(x => x)
                .ToHashSet();
            var current = input.First(x => x.Visited);
            (int X, int Y) direction = new(0, -1);
            while (true)
            {
                var nextStep = new { X = current.X + direction.X, Y = current.Y + direction.Y };
                var nextPos = input.FirstOrDefault(x => x.X == nextStep.X && x.Y == nextStep.Y);
                if (nextPos == null) break;
                if (nextPos.Obstacle)
                {
                    direction = direction switch
                    {
                        (0, 1) => (-1, 0),
                        (1, 0) => (0, 1),
                        (0, -1) => (1, 0),
                        (-1, 0) => (0, -1),
                        _ => throw new NotImplementedException()
                    };
                }
                else
                {
                    nextPos.Visited = true;
                    current = nextPos;
                }
            }
            return input.Count(x => x.Visited).ToString();
        }
        internal class Coordinate(int x, int y, bool obstacle, bool visited)
        {
            public int X { get; } = x;
            public int Y { get; } = y;
            public bool Obstacle { get; } = obstacle;
            public bool Visited { get; set; } = visited;
        }
    }
}
