namespace ConsoleApp.Calendar.D06
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync() // 1784
        {
            Dictionary<(int X, int Y), (bool Obstacle, bool Start)> input = (await ReadFileLinesAsync("Input"))
                .Select((row, yi) => row.Select((c, xi) => new { X = xi, Y = yi, Obstacle = c == '#', Guard = c == '^' }))
                .SelectMany(x => x)
                .ToDictionary(x => (x.X, x.Y), x => (x.Obstacle, x.Guard));

            var current = input.First(x => x.Value.Start);

            var map = input.Keys.ToHashSet();
            var obstacles = input.Where(x => x.Value.Obstacle).Select(x => x.Key).ToHashSet();
            return Patrol(current.Key, map, obstacles, null)
                .Path
                .Count(x => x != current.Key && Patrol(current.Key, map, obstacles, x).Loop)
                .ToString();
        }

        private static (bool Loop, HashSet<(int X, int Y)> Path) Patrol((int X, int Y) current, ICollection<(int X, int Y)> map, ICollection<(int X, int Y)> obstacles, (int X, int Y)? extraObstacle)
        {
            (int X, int Y) direction = new(0, -1);
            var hits = new Dictionary<(int X, int Y), HashSet<(int X, int Y)>>();
            HashSet<(int X, int Y)> path = [];
            var loop = false;
            while (true)
            {
                (int X, int Y) nextStep = (current.X + direction.X, current.Y + direction.Y);
                if (!map.Contains(nextStep))
                    break; // outside area

                if (!obstacles.Contains(nextStep) && (!extraObstacle.HasValue || nextStep != extraObstacle.Value))
                {
                    // no obstacle
                    current = nextStep; // move
                    path.Add(current);
                    continue;
                }

                if (!hits.ContainsKey(nextStep))
                    hits.Add(nextStep, [direction]); // first hit on this obstacle
                else if (!hits[nextStep].Contains(direction))
                    hits[nextStep].Add(direction); // second hit but from new direction
                else
                {
                    // already hit this obstacle from this direction
                    loop = true;
                    break; // loop
                }
                // obstacle hit, change direction
                direction = direction switch
                {
                    (0, 1) => (-1, 0), // down -> left
                    (1, 0) => (0, 1), // right -> down
                    (0, -1) => (1, 0), // up -> right
                    (-1, 0) => (0, -1), // left -> up
                    _ => throw new NotImplementedException()
                };
            }

            return (loop, path); // true = success, false = stuck in loop
        }
    }
}
