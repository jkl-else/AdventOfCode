namespace ConsoleApp.Callendar.D02
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            return input
                .Select(x => new {Enemy = Array.IndexOf(new[] {'A', 'B', 'C'}, x[0]), RequiredResult = Array.IndexOf(new[] {'X', 'Y', 'Z'}, x[2])})
                .Select(x => new {x.Enemy, Me = MyChoice(x.Enemy, x.RequiredResult)})
                .Sum(x => x.Me + 1 // choice score
                               + WinPoints(x.Enemy, x.Me))
                .ToString();
        }

        private int MyChoice(int e, int r)
        {
            switch (r)
            {
                case 0: // loose
                    return e - 1 < 0 ? 2 : e - 1; // return one index below e, min 0
                case 1: // draw
                    return e;
                default: // win
                    return e + 1 > 2 ? 0 : e + 1; // return one index above e, max 2
            }
        }

        private int WinPoints(int e, int m)
        {
            if (e == m)
                return 3; // draw
            switch (e)
            {
                case 0 when m == 1:// Rock Paper
                case 1 when m == 2: // Paper Scissor
                case 2 when m == 0: // Scissor Rock
                    return 6; // win
                default:
                    return 0; // loss
            }
        }
    }
}
