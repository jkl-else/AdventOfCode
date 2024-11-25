namespace ConsoleApp.Callendar.D10
{
    internal class Part1 : Part
    {
        internal List<int> Signals {get; } = new List<int>();
        internal int Cycles
        {
            get => _cycles;
            set
            {
                _cycles = value;
                if (_cycles == 20 || (_cycles - 20) % 40 == 0)
                    Signals.Add(_cycles * X);
            } }
        private int _cycles;
        internal int X { get; set; } = 1;

        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            foreach (var data in input)
            {
                if (data == "noop")
                {
                    Cycles++;
                    continue;
                }

                for (int i = 0; i < 2; i++)
                {
                    Cycles++;
                }

                X += int.Parse(data.Split(' ')[1]);
            }

            return Signals.Sum().ToString();
        }
    }
}
