namespace ConsoleApp.Callendar.D05
{
    internal class Part2 : Part
    {
        //----------2024----------------------------------------------------------------------------------------------------
        public override async Task<string> GetResultAsync()
        {
            var input = (await ReadFileTextAsync("Input2")).Split(Environment.NewLine + Environment.NewLine);
            var seeds = input[0]["seeds: ".Length..]
                .Split(' ')
                .Select((v, i) => new { Value = long.Parse(v), Index = i })
                .GroupBy(x => x.Index / 2)
                .Select(x => new Seed(x.First().Value, x.Last().Value - 1))
                .ToList();
            var transformers = input.Skip(1)
                .Select(x => x.Split(Environment.NewLine).Skip(1) // description row
                    .Select(s =>
                    {
                        var data = s.Split(' ').Select(long.Parse).ToArray();
                        return new Transformer(data[1], data[2], data[0]);
                    }).ToHashSet());

            foreach (var transformer in transformers)
            {
                var transformedSeeds = new List<Seed>();
                foreach (var seed in seeds)
                {
                    var possibleTransformers = transformer
                        .Where(x => x.CanTransform(seed))
                        .OrderBy(x => x.Start)
                        .ToList();
                    if (possibleTransformers.Count == 0)
                    {
                        transformedSeeds.Add(seed);
                        continue;
                    }

                    transformedSeeds.AddRange(possibleTransformers
                        .SelectMany((t, i) => t.Transform(seed, i == 0, i == possibleTransformers.Count - 1)));
                }

                seeds = transformedSeeds;
            }

            return seeds.Min(x => x.Start).ToString();
        }

        internal class Seed(long start, long count)
        {
            public long Start { get; init; } = start;
            public long Count { get; init; } = count;
            public long End => Start + Count;
        }

        internal class Transformer(long start, long count, long transformedStart)
        {
            public long Start { get; init; } = start;
            public long End { get; init; } = start + count;
            public long Diff { get; init; } = transformedStart - start;
            public bool CanTransform(Seed value) => value.End >= Start && value.Start <= End;
            public IEnumerable<Seed> Transform(Seed value, bool first, bool last)
            {
                var count = value.Count;
                var missingStartRange = Start - value.Start;
                if (missingStartRange > 0)
                {
                    count -= missingStartRange;
                    if (first)
                        yield return new Seed(value.Start, missingStartRange);
                }

                var missingEndRange = value.End - End;
                if (missingEndRange > 0)
                {
                    count -= missingEndRange;
                    if (last)
                        yield return new Seed(End, missingEndRange);
                }

                var start = (missingStartRange > 0 ? Start : value.Start) + Diff;
                yield return new Seed(start, count);
            }
        }

        //----------2023----------------------------------------------------------------------------------------------------
        //public override async Task<string> GetResultAsync()
        //{
        //    var input = await ReadFileTextAsync("Input2");//Result = 137516820 - Result in: 00:00:00.0306106
        //    var seeds = new Regex("^seeds: (?<seeds>[0-9 ]*)").Match(input).Groups[1].Value
        //        .Split(' ')
        //        .Select((x, i) => new { Value = x, Index = i })
        //        .GroupBy(x => x.Index / 2)
        //        .Select(x => new SeedCollection(long.Parse(x.First().Value), long.Parse(x.Last().Value)))
        //        .ToList();
        //    List<List<Map>> mapgroups = new Regex(@"(?<id>[a-z-]*) map:\r\n(?<map>[0-9 \r\n]*)\r\n")
        //        .Matches(input)
        //        .Select(x => x.Groups["map"].Value
        //            .Split(Environment.NewLine)
        //            .Where(y => y.Length > 0)
        //            .Select(y =>
        //            {
        //                var values = y.Split(' ').Select(long.Parse).ToArray();
        //                return new Map(values[0], values[1], values[2]);
        //            }).ToList()
        //        ).ToList();
        //    foreach (var seed in seeds)
        //    {
        //        List<ThroughtPut> throughtPuts = new()
        //        {
        //            new(seed.Id, seed.Range)
        //        };
        //        foreach (var mapGroup in mapgroups)
        //        {
        //            List<ThroughtPut> newThroughputs = new();
        //            foreach (var throughtPut in throughtPuts)
        //            {
        //                foreach (var map in mapGroup.Where(x => x.ContainsSource(throughtPut)))
        //                {
        //                    var (result, start, end) = map.Evaluate(throughtPut);
        //                    throughtPut.Evaluations.Add((start, end));
        //                    newThroughputs.Add(result);
        //                }
        //                newThroughputs.AddRange(throughtPut.GetUnevaliated());
        //            }
        //            throughtPuts = newThroughputs;
        //        }
        //        seed.Location = throughtPuts.Count == 0
        //            ? long.MaxValue
        //            : throughtPuts.Min(x => x.Start);
        //    }

        //    return seeds.Min(x => x.Location).ToString(); //511468327 Too High
        //}

        //internal record Map(long Start, long SourceStart, long Range)
        //{
        //    public bool ContainsSource(ThroughtPut throughtPut)
        //        => (throughtPut.Start <= SourceEnd && throughtPut.Start >= SourceStart)
        //        || (throughtPut.End >= SourceStart && throughtPut.End <= SourceEnd);

        //    public long SourceEnd => SourceStart + Range;
        //    public long End => Start + Range;
        //    public (ThroughtPut Result, long Start, long End) Evaluate(ThroughtPut value)
        //    {
        //        // get value to start with since all values might not be in this map
        //        var valueStartInThisRange = value.Start < SourceStart ? SourceStart : value.Start;
        //        // get value to end with since all values might not be in this map
        //        var valueEndInThisRange = value.End > SourceEnd ? SourceEnd : value.End;
        //        // convert to start + range calulcated by source values
        //        var start = valueStartInThisRange + (Start - SourceStart);
        //        // convert to end 
        //        var end = valueEndInThisRange + (Start - SourceStart);
        //        // return new result and which values that this map started and ended with
        //        return (new(start, end - start), valueStartInThisRange, valueEndInThisRange);
        //    }
        //}
        //internal record SeedCollection(long Id, long Range)
        //{
        //    public long Location { get; set; }
        //}
        //public record ThroughtPut(long Start, long Range)
        //{
        //    public long End => Start + Range;
        //    public List<(long Start, long End)> Evaluations { get; } = new();
        //    public IEnumerable<ThroughtPut> GetUnevaliated()
        //    {
        //        if (Evaluations.Count == 0)
        //        {
        //            yield return this; // this hasn't been evaluated yet
        //            yield break;
        //        }
        //        var currentStart = Start;
        //        foreach (var evaluation in Evaluations.OrderBy(x => x.Start))
        //        {
        //            if (currentStart < evaluation.Start)
        //                yield return new(currentStart, evaluation.Start - currentStart); // found values that hasn't been evaluated yet
        //            currentStart = evaluation.End;
        //        }
        //    }
        //}
    }
}
