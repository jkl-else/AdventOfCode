namespace ConsoleApp.Callendar.D01
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            return (await ReadFileTextAsync("Input"))
                .Split(Environment.NewLine + Environment.NewLine)
                .Max(x => x.Split(Environment.NewLine).Select(int.Parse).Sum())
                .ToString();
        }
    }
}
