namespace ConsoleApp.Callendar.D01
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(int.Parse)
                .ToList();
            int result = 0;
            input.Select((x, i) => i > input.Count - 3 ? 0 : x + input[i + 1] + input[i + 2])
                .Aggregate((i, ni) =>
                {
                    if (ni > i)
                        result++;
                    return ni;
                });
            return result.ToString();
        }
    }
}
