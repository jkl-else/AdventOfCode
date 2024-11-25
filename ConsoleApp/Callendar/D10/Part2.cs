namespace ConsoleApp.Callendar.D10
{
    internal class Part2 : Part
    {
        internal int Cycles
        {
            get => _cycles;
            set
            {
                if (_cycles == 40)
                {
                    Console.WriteLine();
                    _cycles = 1;
                    Console.Write(Math.Abs(X - _cycles) <= 1 ? "#" : ".");
                }
                else
                {
                    Console.Write(Math.Abs(X - _cycles) <= 1 ? "#" : ".");
                    _cycles = value;
                }
            }
        }
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

            return "";
        }
    }
}
