using System;
using System.Linq;
using Microsoft.FSharp.Collections;
using QuickSortFSharpLib;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите элементы массива через пробел и нажмите Enter");
            var inString = Console.ReadLine();
            var splittedInString = inString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var intArray = splittedInString.Select(x => Int32.Parse(x));
            var fSharpIntArray = ListModule.OfSeq(intArray);
            var sortedArray = QuickSortFSharpLib.QuickSort.Qsort(fSharpIntArray).ToArray();
            var joinedString = String.Join(' ', sortedArray);
            Console.WriteLine(joinedString);
            Console.ReadKey();
        }
    }
}