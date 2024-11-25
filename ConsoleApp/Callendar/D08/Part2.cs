namespace ConsoleApp.Callendar.D08
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var edge = new { X = input.Length - 1, Y = input.Length - 1 };
            var grid = input.SelectMany((r, iy) => r.Select((c, ix)
                    => new Three(ix, iy, int.Parse(c.ToString()), ix == 0 || iy == 0 || ix == edge.X || iy == edge.Y)))
                .ToList();

            return grid.Select(x => x.Calculate(grid))
                .OrderByDescending(x => x.Score)
                .First().Score
                .ToString();
        }

        internal record Three(int X, int Y, int Size, bool Edge)
        {
            public int Upp { get; set; }
            public int Down { get; set; }
            public int Left { get; set; }
            public int Right { get; set; }
            public int Score => Upp * Left * Down * Right;

            public Three Calculate(List<Three> grid)
            {
                if (Edge)
                    return this;// always 0

                var row = grid.Where(x => x.X == X).ToList();
                Upp = row.Where(x => x.Y < Y)
                    .OrderByDescending(x => x.Y)
                    .TakeUntil(x => x.Size >= Size)
                    .Count();
                Down = row.Where(x => x.Y > Y)
                    .OrderBy(x => x.Y)
                    .TakeUntil(x => x.Size >= Size)
                    .Count();

                var column = grid.Where(x => x.Y == Y).ToList();
                Left = column.Where(x => x.X < X)
                    .OrderByDescending(x => x.X)
                    .TakeUntil(x => x.Size >= Size)
                    .Count();
                Right = column.Where(x => x.X > X)
                    .OrderBy(x => x.X)
                    .TakeUntil(x => x.Size >= Size)
                    .Count();

                return this;
            }
        }
    }

    internal static class Extension
    {
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list)
            {
                yield return item;
                if (predicate.Invoke(item))
                    yield break;
            }
        }
    }
}
