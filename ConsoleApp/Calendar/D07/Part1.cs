namespace ConsoleApp.Calendar.D07
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 42283209483350
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x => x.Split(':'))
                .Select(x =>
                {
                    var value = long.Parse(x[0]);
                    var values = x[1].TrimStart().Split(' ').Select(long.Parse);
                    return new { Result = value, Values = values.ToList() };
                });
            long result = 0;
            const int operatorsToUse = 2;
            foreach (var equation in input)
            {
                var operatorCount = equation.Values.Count - 1; // first value doesn't need operator

                var success = false;
                for (var i = 0; i < Math.Pow(operatorsToUse, operatorCount); i++) // try all scenarios... (v-1) ^n
                {
                    var oIndex = i;
                    var operators = Enumerable.Range(0, operatorCount)
                        .Select(_ =>
                        {
                            var op = oIndex % operatorsToUse;
                            oIndex /= operatorsToUse;
                            return op switch
                            {
                                0 => '+',
                                _ => '*'
                            };
                        }).ToArray();
                    if (Calculate(equation.Values, operators) != equation.Result) continue;
                    success = true;
                    break;
                }

                if (success)
                    result += equation.Result;
            }

            return result.ToString();
        }

        private long Calculate(List<long> values, char[] operators)
        {
            var result = values[0];
            for (var i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case '+':
                        result += values[i + 1];
                        break;
                    case '*':
                        result *= values[i + 1];
                        break;
                }
            }

            return result;
        }
    }
}
