namespace ConsoleApp.Callendar.D05
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var startup = input.TakeWhile(x => x.Length > 0).ToList();
            startup.Reverse();
            var noOfStacks = startup[0].Split(' ').Count(x => x.Length > 0);
            var stacks = Enumerable.Range(0, noOfStacks).Select(_ => new List<string>()).ToArray();
            for (int i = 1; i < startup.Count; i++)
            {
                for (int j = 0; j < noOfStacks; j++)
                {
                    var s = new String(startup[i].Skip(j * 3 + j + 1).Take(1).ToArray()).Trim();
                    if (s.Length > 0)
                        stacks[j].Add(s);
                }
            }

            var moveMents = input.Skip(startup.Count + 1);
            foreach (var movement in moveMents)
            {
                var data = movement.Split(' ');
                var fromIndex = int.Parse(data[3]) - 1;
                var toIndex = int.Parse(data[5]) - 1;
                for (int i = 0; i < int.Parse(data[1]); i++)
                {
                    var box = stacks[fromIndex].Last();
                    stacks[fromIndex].RemoveAt(stacks[fromIndex].Count - 1); // should have used: System.Collections.Stack
                    stacks[toIndex].Add(box);
                }
            }
            return String.Join("", stacks.Select(s => s.Last()));
        }
    }
}
