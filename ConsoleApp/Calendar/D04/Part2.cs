namespace ConsoleApp.Calendar.D04
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync() // 1945
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select((x, i) => new { x, i })
                .ToDictionary(x => x.i,
                    x => x.x.Select((y, i) => new { y, i }).ToDictionary(y => y.i, y => y.y));

            var resultList = new List<(int X, int Y)>();

            foreach (var (y, row) in input)
            {
                foreach (var x in row.Where(x => x.Value == 'M').Select(x => x.Key))
                {
                    var aPositions = new[]
                    {
                        (x + 1, y + 1),
                        (x + 1, y - 1),
                        (x - 1, y + 1),
                        (x - 1, y - 1)
                    };

                    foreach (var (ax, ay) in aPositions)
                    {
                        if (!input.TryGetValue(ay, out var aRow) || !aRow.TryGetValue(ax, out var aValue) || aValue != 'A') continue;

                        var sx = ax + (ax - x);
                        var sy = ay + (ay - y);
                        if (!input.TryGetValue(sy, out var sRow) || !sRow.TryGetValue(sx, out var sValue) || sValue != 'S') continue;

                        resultList.Add((ax, ay));
                    }
                }
            }

            return resultList
                .GroupBy(x => x)
                .Count(x => x.Count() == 2)
                .ToString();
        }
    }
}
