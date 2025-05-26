//8) Given two integer arrays, merge them into a single array.
using System;

class Program{
    public static void Main(string[] args){
        int[] arr1 = {1,3,5};
        int[] arr2 = {2,4,6};
        int[] merged = new int[arr1.Length + arr2.Length];
        arr1.CopyTo(merged, 0);
        arr2.CopyTo(merged, arr1.Length);

        Console.WriteLine("Array after merging : ");
        foreach(int num in merged){
            Console.Write(num + " ");
        }
    }
}