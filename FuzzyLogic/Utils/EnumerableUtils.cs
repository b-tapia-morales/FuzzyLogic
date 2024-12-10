namespace FuzzyLogic.Utils;

public static class EnumerableUtils
{
    public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2) where T : IComparable<T>
    {
        var set1 = new HashSet<T>(list1);
        var set2 = new HashSet<T>(list2);
        return set1.SetEquals(set2);
    }

    public static IList<IList<T>> FilterUnique<T>(this IEnumerable<IList<T>> listOfLists) where T : IComparable<T>
    {
        var uniqueLists = new List<IList<T>>();

        foreach (var list in listOfLists)
        {
            if (!uniqueLists.Exists(uniqueList => ScrambledEquals(uniqueList, list)))
                uniqueLists.Add(list);
        }

        return uniqueLists;
    }
}