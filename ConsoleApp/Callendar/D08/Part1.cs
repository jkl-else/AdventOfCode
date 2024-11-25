using System.Drawing;

namespace ConsoleApp.Callendar.D08
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            var input = await ReadFileLinesAsync("Input");
            var grid = input.Select((r, iy) => r.Select((c, ix) => new Three(ix, iy, int.Parse(c.ToString())))
                    .ToArray()
                ).ToArray();
            var visible = 0;
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    var three = grid[y][x];
                    if (y == 0 || x == 0)
                        visible++;
                    else if (y == grid.Length || x == grid[y].Length)
                        visible++;
                    else if (grid.Take(y).All(col => col[x].Size < three.Size)) // Upp
                        visible++;
                    else if (grid[y].Where(row => row.X < three.X).All(row => row.Size < three.Size)) // Left
                        visible++;
                    else if (grid.Skip(y + 1).All(col => col[x].Size < three.Size)) // down
                        visible++;
                    else if (grid[y].Where(row => row.X > three.X).All(row => row.Size < three.Size)) // Right
                        visible++;
                }
            }

            return visible.ToString();
        }

        internal record Three(int X, int Y, int Size);
    }
}
