namespace ConsoleApp.Callendar.D03
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            List<(char Key, int Count)> GetData(IEnumerable<string> values, int index) => values
                .Select(row => row[index])
                .GroupBy(x => x)
                .Select(x => (x.Key, x.Count()))
                .ToList();

            var oxygen = input;
            var co2 = input;
            for (int i = 0; i < input[0].Length; i++)
            {
                if (oxygen.Length > 1)
                {
                    var oxygenValues = GetData(oxygen, i);
                    var oxygenFilter = oxygenValues.OrderByDescending(x => x.Count).ThenByDescending(x => x.Key).First().Key;
                    oxygen = oxygen.Where(x => x[i] == oxygenFilter).ToArray();
                }
                if (co2.Length > 1)
                {
                    var co2Values = GetData(co2, i);
                    var co2Filter = co2Values.OrderBy(x => x.Count).ThenBy(x => x.Key).First().Key;
                    co2 = co2.Where(x => x[i] == co2Filter).ToArray();
                }
            }

            return (Convert.ToInt32(new string(oxygen[0]), 2) * Convert.ToInt32(new string(co2[0]), 2)).ToString();
        }
    }
}
