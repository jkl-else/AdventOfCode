namespace ConsoleApp.Calendar.D02
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 502
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x => x.Split(' ').Select(int.Parse).ToArray());
            return input.Count(x =>
            {
                var direction = 0;
                int prev = x[0];
                for (int i = 1; i < x.Length; i++)
                {
                    var val = x[i];
                    var dir = val > prev ? +1 : val < prev ? -1 : 0;
                    if (direction == 0)
                        direction = dir;
                    else if (direction != dir)
                        return false;
                    var diff = Math.Abs(val - prev);
                    if (diff is < 1 or > 3)
                        return false;
                    prev = val;
                }

                return true;
            }).ToString();
        }
    }
}
