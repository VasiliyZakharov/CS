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
                //Сортировка вставкой
                if (sortOrder == SortOrder.Ascending)
                    Array.Sort(array, comparison);
                else
                    Array.Sort(array, (x, y) => comparison(y, x));
                break;
            case "SelectionSort":
                SelectionSort(array, sortOrder, comparison);
                break;
            case "HeapSort":
                HeapSort(array, sortOrder, comparison);
                break;
            case "QuickSort":
                QuickSort(array, 0, array.Length - 1, sortOrder, comparison);
                break;
            case "MergeSort":
                array = MergeSort(array, sortOrder, comparison);
                break;
            default:
                throw new ArgumentException("Неподдерживаемый алгоритм сортировки", nameof(sortingAlgorithm));
        }

        return array;
    }

    //Сортировка выбором
    private static void SelectionSort<T>(T[] array, SortOrder sortOrder, Comparison<T> comparison)
    {
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
    }

    //Пирамидальная сортировка
    private static void HeapSort<T>(T[] array, SortOrder sortOrder, Comparison<T> comparison)
    {
        for (int i = array.Length / 2 - 1; i >= 0; i--)
            Heapify(array, array.Length, i, sortOrder, comparison);

        for (int i = array.Length - 1; i > 0; i--)
        {
            T temp = array[0];
            array[0] = array[i];
            array[i] = temp;

            Heapify(array, i, 0, sortOrder, comparison);
        }
    }

    private static void Heapify<T>(T[] array, int size, int rootIndex, SortOrder sortOrder, Comparison<T> comparison)
    {
        int largestIndex = rootIndex;
        int leftIndex = 2 * rootIndex + 1;
        int rightIndex = 2 * rootIndex + 2;

        if (leftIndex < size && CompareElements(array[leftIndex], array[largestIndex], sortOrder, comparison) > 0)
            largestIndex = leftIndex;

        if (rightIndex < size && CompareElements(array[rightIndex], array[largestIndex], sortOrder, comparison) > 0)
            largestIndex = rightIndex;

        if (largestIndex != rootIndex)
        {
            T temp = array[rootIndex];
            array[rootIndex] = array[largestIndex];
            array[largestIndex] = temp;

            Heapify(array, size, largestIndex, sortOrder, comparison);
        }
    }

    //Быстрая сортировка
    private static void QuickSort<T>(T[] array, int left, int right, SortOrder sortOrder, Comparison<T> comparison)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right, sortOrder, comparison);
            QuickSort(array, left, pivotIndex - 1, sortOrder, comparison);
            QuickSort(array, pivotIndex + 1, right, sortOrder, comparison);
        }
    }

    private static int Partition<T>(T[] array, int left, int right, SortOrder sortOrder, Comparison<T> comparison)
    {
        T pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (CompareElements(array[j], pivot, sortOrder, comparison) <= 0)
            {
                i++;
                SwapElements(array, i, j);
            }
        }

        SwapElements(array, i + 1, right);
        return i + 1;
    }

    private static void SwapElements<T>(T[] array, int i, int j)
    {
        T temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    //Сортировка слиянием
    private static T[] MergeSort<T>(T[] array, SortOrder sortOrder, Comparison<T> comparison)
    {
        if (array.Length <= 1)
            return array;

        int middleIndex = array.Length / 2;
        T[] leftArray = new T[middleIndex];
        T[] rightArray = new T[array.Length - middleIndex];

        Array.Copy(array, 0, leftArray, 0, middleIndex);
        Array.Copy(array, middleIndex, rightArray, 0, array.Length - middleIndex);

        leftArray = MergeSort(leftArray, sortOrder, comparison);
        rightArray = MergeSort(rightArray, sortOrder, comparison);

        return Merge(leftArray, rightArray, sortOrder, comparison);
    }

    private static T[] Merge<T>(T[] leftArray, T[] rightArray, SortOrder sortOrder, Comparison<T> comparison)
    {
        int leftIndex = 0;
        int rightIndex = 0;
        int resultIndex = 0;
        int resultLength = leftArray.Length + rightArray.Length;
        T[] resultArray = new T[resultLength];

        while (leftIndex < leftArray.Length && rightIndex < rightArray.Length)
        {
            if (CompareElements(leftArray[leftIndex], rightArray[rightIndex], sortOrder, comparison) <= 0)
            {
                resultArray[resultIndex] = leftArray[leftIndex];
                leftIndex++;
            }
            else
            {
                resultArray[resultIndex] = rightArray[rightIndex];
                rightIndex++;
            }
            resultIndex++;
        }

        while (leftIndex < leftArray.Length)
        {
            resultArray[resultIndex] = leftArray[leftIndex];
            leftIndex++;
            resultIndex++;
        }

        while (rightIndex < rightArray.Length)
        {
            resultArray[resultIndex] = rightArray[rightIndex];
            rightIndex++;
            resultIndex++;
        }

        return resultArray;
    }

    // Сравнение элементов в зависимости от порядка сортировки
    private static int CompareElements<T>(T x, T y, SortOrder sortOrder, Comparison<T> comparison)
    {
        int result = comparison(x, y);
        return sortOrder == SortOrder.Ascending ? result : -result;
    }
}

//Вспомогательный класс для обёртывания Comparer<T> в IComparer<T>
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

// Пример использования

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
