using System.Drawing;

namespace ConsoleApp.Callendar.D09
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var moveMents = input.Select(x =>
            {
                var d = x.Split(' ');
                var value = int.Parse(d[1]);
                switch (d[0])
                {
                    case "R":
                        return (new Point(1, 0), value);
                    case "L":
                        return (new Point(-1, 0), value);
                    case "U":
                        return (new Point(0, 1), value);
                    default: // D
                        return (new Point(0, -1), value);
                }
            });

            var tail = new Point(0, 0);
            var head = new Point(0, 0);
            var visitations = new List<Point>{ tail };
            foreach (var movement in moveMents)
            {
                for (int i = 0; i < movement.value; i++)
                {
                    head.X += movement.Item1.X;
                    head.Y += movement.Item1.Y;

                    var xDiff = head.X - tail.X;
                    var yDiff = head.Y - tail.Y;

                    var absXdiff = Math.Abs(xDiff);
                    var absYdiff = Math.Abs(yDiff);
                    if (absXdiff <= 1 && absYdiff <= 1)
                        continue; // close enought

                    if (absXdiff > 0)
                        tail.X += xDiff < 0 ? -1 : 1;
                    if (absYdiff > 0)
                        tail.Y += yDiff < 0 ? -1 : 1;
                    visitations.Add(tail);
                }
            }

            return visitations.Distinct().Count().ToString();
        }
    }
}
