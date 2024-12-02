namespace ConsoleApp.Calendar.D02
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync() // 544
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x => x.Split(' ').Select(int.Parse).ToList());
            var result = input.Count(x =>
            {
                if (TryItOut(x)) return true;
                for (int i = 0; i < x.Count; i++)
                {
                    var copy = x.ToList();
                    copy.RemoveAt(i);
                    if (TryItOut(copy))
                        return true;
                }

                return false;
            });
            return result.ToString();
        }

        private bool TryItOut(List<int> x)
        {
            var direction = 0;
            var prev = x[0];
            for (int i = 1; i < x.Count; i++)
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
        }
    }
}
