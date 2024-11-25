using System.Drawing;

namespace ConsoleApp.Callendar.D09
{
    internal class Part2 : Part
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

            var tail = Enumerable.Range(0, 9).Select(_ => new Point(0, 0)).ToArray();
            var head = new Point(0, 0);
            var visitations = new List<Point> { tail[^1] };
            foreach (var movement in moveMents)
            {
                for (int i = 0; i < movement.value; i++)
                {
                    head.X += movement.Item1.X;
                    head.Y += movement.Item1.Y;

                    var previus = head;
                    for (int k = 0; k < 9; k++)
                    {
                        if (!MoveKnot(tail[k], previus, out var newPos))
                            break;

                        tail[k] = newPos;
                        if (k == 8) // tail
                            visitations.Add(tail[k]);
                        else
                            previus = newPos;
                    }
                }
            }

            return visitations.Distinct().Count().ToString();
        }

        private bool MoveKnot(Point currentPosition, Point headOrKnot, out Point newPosition)
        {
            newPosition = new Point(currentPosition.X, currentPosition.Y);

            var xDiff = headOrKnot.X - currentPosition.X;
            var yDiff = headOrKnot.Y - currentPosition.Y;

            var absXdiff = Math.Abs(xDiff);
            var absYdiff = Math.Abs(yDiff);
            if (absXdiff > 1 || absYdiff > 1)// not close enought
            {
                if (absXdiff > 0)
                    newPosition.X += xDiff < 0 ? -1 : 1;
                if (absYdiff > 0)
                    newPosition.Y += yDiff < 0 ? -1 : 1;
            }

            return newPosition.X != currentPosition.X || newPosition.Y != currentPosition.Y;
        }
    }
}
