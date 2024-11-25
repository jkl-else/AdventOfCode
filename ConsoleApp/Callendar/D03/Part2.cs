namespace ConsoleApp.Callendar.D03
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var points = Enumerable.Range('a', 'z' - 'a' + 1)
                .Union(Enumerable.Range('A', 'Z' - 'A' + 1))
                .Select((x, i) => new { Value = (char)x, Score = i + 1 })
                .ToDictionary(x => x.Value, x => x.Score);

            var scores = 0;
            foreach (var group in input.Select((x, i) => new {Value = x, Index = i}).GroupBy(x => x.Index / 3)) // SHould have used: IEnumerable<>.Chunk(3)
            {
                var first = group.First().Value;
                var second = group.Skip(1).First().Value;
                var thrid = group.Last().Value;
                var badge = first.Intersect(second).Intersect(thrid);
                scores += badge.Sum(x => points[x]);
            }
            return scores.ToString();
        }
    }
}
