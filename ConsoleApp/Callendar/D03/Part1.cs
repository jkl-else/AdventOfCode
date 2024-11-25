namespace ConsoleApp.Callendar.D03
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var data = input.SelectMany(row => row.Select((val, index) => (val, index)))
                .GroupBy(obj => obj.index)
                .Select(x => x.GroupBy(obj => obj.val).OrderByDescending(y => y.Count()).Select(y => y.Key).ToList())
                .ToList();
            var epsilon = Convert.ToInt32(string.Join("", data.Select(x => x[0])), 2);
            var gamma = Convert.ToInt32(string.Join("", data.Select(x => x[1])), 2);
            return (epsilon * gamma).ToString();
        }
    }
}
