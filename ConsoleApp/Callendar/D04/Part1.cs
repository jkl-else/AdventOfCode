namespace ConsoleApp.Callendar.D04
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var overLaps = 0;
            foreach (var pair in input.Select(x
                         => x.Split(',')
                             .Select(y
                                 => y.Split('-').Select(int.Parse).ToArray())
                             .Select(y => new { Start = y[0], End = y[1] })
                             .ToArray()))
            {
                if (pair[0].Start <= pair[1].Start && pair[0].End >= pair[1].End)
                    overLaps++;
                else if (pair[1].Start <= pair[0].Start && pair[1].End >= pair[0].End)
                    overLaps++;
            }

            return overLaps.ToString();
        }
    }
}
