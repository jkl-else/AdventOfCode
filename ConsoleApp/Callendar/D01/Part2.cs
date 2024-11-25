namespace ConsoleApp.Callendar.D01
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            return (await ReadFileTextAsync("Input"))
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(x => x.Split(Environment.NewLine).Select(int.Parse).Sum())
                .OrderByDescending(x => x)
                .Take(3)
                .Sum()
                .ToString();
        }
    }
}
