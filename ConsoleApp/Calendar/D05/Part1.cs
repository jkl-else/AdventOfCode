namespace ConsoleApp.Calendar.D05
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync() // 5275
        {
            var input = await ReadFileLinesAsync("Input");
            var ordering = input.TakeWhile(x => x != string.Empty)
                .Select(x => x.Split('|').Select(int.Parse).ToArray())
                .ToList();
            var pages = input.Skip(ordering.Count + 1)
                .Select(x => x.Split(',').Select(int.Parse).ToList())
                .ToList();
            return pages
                .Where(page => !UnSorted(page, ordering))
                .Sum(page => page[page.Count / 2])
                .ToString();
        }

        private bool UnSorted(List<int> page, IEnumerable<int[]> ordering) =>
            (from o in ordering let firstIndex = page.IndexOf(o[0])
                where firstIndex != -1
                let secondIndex = page.IndexOf(o[1])
                where secondIndex != -1 && firstIndex >= secondIndex
                select firstIndex).Any();
    }
}
