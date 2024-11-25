namespace ConsoleApp.Callendar.D02
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x => x.Split(' '));
            var horizontal = 0;
            var depth = 0;
            foreach (var x in input)
            {
                var value = int.Parse(x[1]);
                switch (x[0])
                {
                    case "forward":
                        horizontal += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return (horizontal * depth).ToString();
        }
    }
}
