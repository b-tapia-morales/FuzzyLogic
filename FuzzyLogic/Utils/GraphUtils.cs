namespace FuzzyLogic.Utils;

public static class GraphUtils
{
    public static IList<IList<string>> FindCycles(IDictionary<string, IList<string>> adjacencyList)
    {
        var cycles = new List<IList<string>>();

        foreach (var startingNode in adjacencyList.Keys)
        {
            var stack = new Stack<(string CurrentNode, List<string> Path)>();
            stack.Push((startingNode, [startingNode]));

            while (stack.Count > 0)
            {
                var (currentNode, path) = stack.Pop();

                if (!adjacencyList.TryGetValue(currentNode, out var currentPath))
                    continue;

                foreach (var neighbor in currentPath)
                {
                    if (path.Contains(neighbor))
                    {
                        var startIndex = adjacencyList[neighbor].IndexOf(neighbor);
                        var cycle = path.Skip(startIndex).Append(neighbor).ToList();
                        cycles.Add(cycle);
                        continue;
                    }

                    stack.Push((neighbor, [..path, neighbor]));
                }
            }
        }

        return cycles.FilterUnique().ToList();
    }

    public static ISet<(string Descendant, string Ancestor)> FindBackEdges(IDictionary<string, IList<string>> adjacencyList) =>
        FindCycles(adjacencyList).Select(list => new[] {list[^2], list[^1]}).Select(array => (array[0], array[1])).ToHashSet();
}