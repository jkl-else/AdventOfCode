using System.Text.RegularExpressions;

namespace ConsoleApp.Calendar.D03
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 184122457
        {
            var regex = new Regex(@"mul[(](?<Value1>\d{1,}),(?<Value2>\d{1,})[)]");
            var input = await ReadFileTextAsync("Input");
            return regex.Matches(input)
                .Sum(match => int.Parse(match.Groups["Value1"].Value) * int.Parse(match.Groups["Value2"].Value))
                .ToString();
        }
    }
}
