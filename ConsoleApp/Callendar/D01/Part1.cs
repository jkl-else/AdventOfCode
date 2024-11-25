namespace ConsoleApp.Callendar.D01
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            int result = 0;
            (await ReadFileLinesAsync("Input"))
                .Select(int.Parse)
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
