namespace ConsoleApp.Callendar.D11
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = (await ReadFileTextAsync("Input"))
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(monkeyString =>
                {
                    var monkeydata = monkeyString.Split(Environment.NewLine);
                    var d = monkeydata[2].Split("= ")[1].Split(' ');

                    return new Monkey
                    {
                        Id = int.Parse(monkeydata[0].Split(' ').Last()[..^1]),
                        Items = new Queue<long>(monkeydata[1][(monkeydata[1].IndexOf(':') + 2)..].Split(", ").Select(long.Parse)),
                        InspectionValue = d[2] == "old" ? null : int.Parse(d[2]),
                        InspectionType = d[1] == "+" ? InspectionType.Addition : InspectionType.Multiplication,
                        TestDivision = int.Parse(monkeydata[3].Split(' ').Last()),
                        DestinationMonkeys = new[]
                        {
                            int.Parse(monkeydata[4].Split(' ').Last()),
                            int.Parse(monkeydata[5].Split(' ').Last())
                        }
                    };
                }).ToDictionary(x => x.Id, x => x);

            var lcm = CalculateLcm(input);
            foreach (var monkey in input)
            {
                if (lcm % monkey.Value.TestDivision != 0)
                    Console.WriteLine("Error: Lcm calculation isn't right!");
            }

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in input)
                {
                    while (monkey.Value.Items.TryDequeue(out var item))
                    {
                        var newItemValue = monkey.Value.Inspect(item, lcm);

                        var destinationMonkeyId = monkey.Value.Test(newItemValue);
                        input[destinationMonkeyId].Items.Enqueue(newItemValue);
                    }
                }
            }
            
            var monkeyBusiness = input.Values.OrderByDescending(x => x.ItemsInspected).Take(2).ToArray();
            return (monkeyBusiness[0].ItemsInspected * monkeyBusiness[1].ItemsInspected).ToString();
        }

        internal int CalculateLcm(Dictionary<int, Monkey> monkeys)
            => monkeys.Values.SelectMany(x => x.Refactor()).Aggregate(1, (current, d) => current * d);

        internal class Monkey
        {
            public int Id { get; set; }
            public Queue<long> Items { get; set; } = null!;
            public int? InspectionValue { get; set; }
            public InspectionType InspectionType { get; set; }
            public long ItemsInspected { get; set; }

            public int TestDivision { get; set; }
            public int[] DestinationMonkeys { get; set; } = null!;

            public int Test(double value) => value / TestDivision % 1 == 0
                ? DestinationMonkeys[0]
                : DestinationMonkeys[1];

            public long Inspect(long value, long lcm)
            {
                ItemsInspected++;
                var newValue = InspectionType == InspectionType.Addition
                    ? Addition(value)
                    : Multiplication(value);
                return newValue % lcm;
            }

            public IEnumerable<int> Refactor()
            {
                for (int i = 1; i <= TestDivision; i++)
                {
                    if (TestDivision % i == 0)
                        yield return i;
                }
            }

            private long Addition(long value) => value + (InspectionValue ?? value);
            private long Multiplication(long value) => value * (InspectionValue ?? value);
        }

        internal enum InspectionType
        {
            Addition,
            Multiplication,
        }
    }
}
