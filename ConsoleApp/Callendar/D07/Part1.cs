namespace ConsoleApp.Callendar.D07
{
    internal class Part1 : Part
    {
        public override async Task<string> GetResultAsync()
        {
            const string root = "#root";
            var input = await ReadFileLinesAsync("Input");
            var directorySizes = new Dictionary<string, long>
            {
                [root] = 0
            };
            var currentPath = String.Empty;
            foreach (var cmd in input)
            {
                if (cmd == "$ ls" || cmd.StartsWith("dir "))
                    continue; // nothing to do
                if (cmd.StartsWith("$ cd "))
                {
                    var path = cmd[5..];
                    switch (path)
                    {
                        case "/":
                            currentPath = root;
                            break;
                        case "..":
                        {
                            var index = currentPath.LastIndexOf('\\');
                            currentPath = index < 0 ? root : currentPath[..index];
                            break;
                        }
                        default:
                            currentPath = Path.Combine(currentPath, path);
                            break;
                    }
                }
                else
                {
                    var data = cmd.Split(' ');
                    if (long.TryParse(data[0], out long size))
                    {
                        int index;
                        var path = currentPath;
                        while ((index = path.LastIndexOf('\\')) > 0)
                        {
                            if (!directorySizes.ContainsKey(path))
                                directorySizes.Add(path, 0);
                            directorySizes[path] += size;
                            path = path[..index];
                        }
                        directorySizes[root] += size;
                    }
                }
            }

            return directorySizes.Where(x => x.Value is <= 100000 and > 0).Sum(x => x.Value).ToString();
        }
    }
}
