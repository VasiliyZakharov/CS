using System;
using System.Collections.Generic;

public enum SortOrder
{
    Ascending,
    Descending
}

public static class ArraySortingExtensions
{
    // Методы расширения для сортировки массивов

    public static T[] Sort<T>(this T[] array, SortOrder sortOrder, string sortingAlgorithm)
        where T : IComparable<T>
    {
        return Sort(array, sortOrder, sortingAlgorithm, Comparer<T>.Default);
    }

    public static T[] Sort<T>(this T[] array, SortOrder sortOrder, string sortingAlgorithm, IComparer<T> comparer)
    {
        return Sort(array, sortOrder, sortingAlgorithm, Comparer<T>.Create(comparer.Compare));
    }

    public static T[] Sort<T>(this T[] array, SortOrder sortOrder, string sortingAlgorithm, Comparer<T> comparer)
    {
        return Sort(array, sortOrder, sortingAlgorithm, comparer.Compare);
    }

    public static T[] Sort<T>(this T[] array, SortOrder sortOrder, string sortingAlgorithm, Comparison<T> comparison)
    {
        switch (sortingAlgorithm)
        {
            case "InsertionSort":
                if (sortOrder == SortOrder.Ascending)
                    Array.Sort(array, comparison);
                else
                    Array.Sort(array, (x, y) => comparison(y, x));
                break;
            case "SelectionSort":
                for (int i = 0; i < array.Length - 1; i++)
                {
                    int minIndex = i;
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            if (comparison(array[j], array[minIndex]) < 0)
                                minIndex = j;
                        }
                        else
                        {
                            if (comparison(array[j], array[minIndex]) > 0)
                                minIndex = j;
                        }
                    }
                    T temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
                break;
            default:
                throw new ArgumentException("Неподдерживаемый алгоритм сортировки", nameof(sortingAlgorithm));
        }

        return array;
    }
}

// Вспомогательный класс для обёртывания Comparer<T> в IComparer<T>
public class ComparerWrapper<T> : IComparer<T>
{
    private readonly Comparer<T> comparer;

    public ComparerWrapper(Comparer<T> comparer)
    {
        this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public int Compare(T x, T y)
    {
        return comparer.Compare(x, y);
    }
}




class Program
{
    static void Main()
    {
        int[] numbers = { 5, 2, 8, 3, 1 };

        // Пример использования различных методов сортировки:

        // Встроенный тип со значением по умолчанию:
        var sortedArray1 = numbers.Sort(SortOrder.Ascending, "InsertionSort");
        Console.WriteLine(string.Join(", ", sortedArray1));

        // Использование внешнего Comparer<int> со сравнением по убыванию:
        var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));
        var sortedArray2 = numbers.Sort(SortOrder.Descending, "SelectionSort", comparer);
        Console.WriteLine(string.Join(", ", sortedArray2));

        // Использование Comparison<int> для определения пользовательского порядка сортировки:
        var sortedArray3 = numbers.Sort(SortOrder.Ascending, "InsertionSort", (x, y) =>
        {
        if (x % 2 == 0 && y % 2 != 0) // Четные числа в начале
                return -1;
            if (x % 2 != 0 && y % 2 == 0) // Нечетные числа в конце
                return 1;
            return x.CompareTo(y); // По возрастанию остальных чисел
        });
        Console.WriteLine(string.Join(", ", sortedArray3));
    }
}
