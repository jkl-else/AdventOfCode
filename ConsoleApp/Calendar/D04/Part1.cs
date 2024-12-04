namespace ConsoleApp.Calendar.D04
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 2458
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select((x, i) => new { x, i })
                .ToDictionary(x => x.i,
                    x => x.x.Select((y, i) => new { y, i }).ToDictionary(y => y.i, y => y.y));

            var result = 0;
            foreach (var (y, row) in input)
            {
                foreach (var x in row.Where(x => x.Value == 'X').Select(x => x.Key))
                {
                    var directions = new[]
                    {
                        (x + 1, y),
                        (x - 1, y),
                        (x, y + 1),
                        (x, y - 1),
                        (x + 1, y + 1),
                        (x + 1, y - 1),
                        (x - 1, y + 1),
                        (x - 1, y - 1)
                    };

                    foreach (var (dx, dy) in directions)
                    {
                        if (!input.TryGetValue(dy, out var mr) || !mr.TryGetValue(dx, out var mv) || mv != 'M') continue;
                        
                        var ax = dx + (dx - x);
                        var ay = dy + (dy - y);
                        if (!input.TryGetValue(ay, out var ar) || !ar.TryGetValue(ax, out var av) || av != 'A') continue;

                        var sx = ax + (ax - dx);
                        var sy = ay + (ay - dy);
                        if (!input.TryGetValue(sy, out var sr) || !sr.TryGetValue(sx, out var sv) || sv != 'S') continue;
                        result++;
                    }
                }
            }

            return result.ToString();
        }
    }
}
