using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookOrca.Core;

public static class Extentions
{
    public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
        {
            observableCollection.Add(item);
        }
    }
}