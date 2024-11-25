namespace ConsoleApp.Callendar.D11
{
    internal class Part1 : Part
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

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in input)
                {
                    while(monkey.Value.Items.TryDequeue(out var item))
                    {
                        var newItemValue = monkey.Value.Inspect(item);

                        newItemValue /= 3;

                        var destinationMonkeyId = monkey.Value.Test(newItemValue);
                        input[destinationMonkeyId].Items.Enqueue(newItemValue);
                    }
                }
            }

            var monkeyBusiness = input.Values.OrderByDescending(x => x.ItemsInspected).Take(2).ToArray();
            return (monkeyBusiness[0].ItemsInspected * monkeyBusiness[1].ItemsInspected).ToString();
        }

        internal class Monkey
        {
            public int Id {get; set;}
            public Queue<long> Items { get; set; } = null!;
            public int? InspectionValue {get; set;}
            public InspectionType InspectionType {get; set;}
            public int ItemsInspected {get; set;}

            public double TestDivision {get; set;}
            public int[] DestinationMonkeys { get; set; } = null!;

            public int Test(long value) => value / TestDivision % 1 == 0
                ? DestinationMonkeys[0]
                : DestinationMonkeys[1];

            public long Inspect(long value)
            {
                ItemsInspected++;
                return InspectionType == InspectionType.Addition
                    ? Addition(value)
                    : Multiplication(value);
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
