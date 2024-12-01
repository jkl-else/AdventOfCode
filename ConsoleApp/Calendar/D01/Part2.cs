namespace ConsoleApp.Calendar.D01
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync() // 23126924
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x =>
                {
                    var v = x.Split("   ");
                    return (int.Parse(v[0]), int.Parse(v[1]));
                })
                .ToList();
            var counter = input.GroupBy(x => x.Item2)
                .ToDictionary(x => x.Key, x => x.Count());
            return input.Sum(x => x.Item1 * counter.GetValueOrDefault(x.Item1, 0))
                .ToString();
        }
    }
}