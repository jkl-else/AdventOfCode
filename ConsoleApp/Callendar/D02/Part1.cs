namespace ConsoleApp.Callendar.D02
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            return input
                .Select(x => new {Enemy = Array.IndexOf(new[] {'A', 'B', 'C'}, x[0]), Me = Array.IndexOf(new[] {'X', 'Y', 'Z'}, x[2])})
                .Sum(x => x.Me + 1 // choice score
                               + WinPoints(x.Enemy, x.Me))
                .ToString();
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
