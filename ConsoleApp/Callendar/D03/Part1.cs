namespace ConsoleApp.Callendar.D03
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var points = Enumerable.Range('a', 'z' - 'a' + 1)
                .Union(Enumerable.Range('A', 'Z' - 'A' + 1))
                .Select((x, i) => new { Value = (char)x, Score = i + 1 })
                .ToDictionary(x => x.Value, x => x.Score);

            var scores = 0;
            foreach (var ruggsack in input)
            {
                var comp1 = ruggsack.Take(ruggsack.Length / 2).ToList();
                var comp2 = ruggsack.Skip(comp1.Count).ToList();
                var match = comp1.Intersect(comp2).ToList();
                scores += match.Sum(x => points[x]);
            }
            return scores.ToString();
        }
    }
}
