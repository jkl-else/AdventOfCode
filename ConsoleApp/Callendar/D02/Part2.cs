namespace ConsoleApp.Callendar.D02
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = (await ReadFileLinesAsync("Input"))
                .Select(x => x.Split(' '));
            var horizontal = 0;
            var depth = 0;
            var aim = 0;
            foreach (var x in input)
            {
                var value = int.Parse(x[1]);
                switch (x[0])
                {
                    case "forward":
                        horizontal += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return (horizontal * depth).ToString();
        }
    }
}
