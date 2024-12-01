namespace ConsoleApp.Calendar.D01
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 1506483
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x =>
                {
                    var v = x.Split("   ");
                    return (int.Parse(v[0]), int.Parse(v[1]));
                })
                .OrderBy(x => x.Item1)
                .ToArray();
            var data = input.Select(x => x.Item2)
                .OrderBy(x => x)
                .ToArray();
            return input.Select((x, i) => Math.Abs(data[i] - x.Item1))
                .Sum()
                .ToString();
        }
    }
}