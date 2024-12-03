using System.Text.RegularExpressions;

namespace ConsoleApp.Calendar.D03
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync() // 107862689
        {
            var regex = new Regex(@"(mul[(](?<Value1>\d{1,}),(?<Value2>\d{1,})[)])|(don't[(][)])|(do[(][)])");
            var input = await ReadFileTextAsync("Input");
            var save = true;
            return regex.Matches(input).Sum(match =>
            {
                switch (match.Value)
                {
                    case "don't()":
                        save = false;
                        return 0;
                    case "do()":
                        save = true;
                        return 0;
                }

                if (!save) return 0;
                return int.Parse(match.Groups["Value1"].Value) * int.Parse(match.Groups["Value2"].Value);
            }).ToString();
        }
    }
}
