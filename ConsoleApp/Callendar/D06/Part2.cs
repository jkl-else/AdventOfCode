namespace ConsoleApp.Callendar.D06
{
    internal class Part2 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileTextAsync("Input");
            var hash = new List<char>();
            var i = 0;
            for (; i < input.Length; i++)
            {
                var c = input[i];
                if (hash.Count >= 14)
                    break;

                var index = hash.IndexOf(c);
                if (index >= 0)
                    hash.RemoveRange(0, index + 1);

                hash.Add(c);
                if (hash.Count == 14)
                    break;
            }

            return (i + 1).ToString();
        }
    }
}
