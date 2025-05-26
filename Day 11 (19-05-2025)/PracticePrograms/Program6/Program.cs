//6) Given an array, count the frequency of each element and print the result.
using System.IO;
using System;
using System.Collections.Generic;

class Program{
    public static void Main(string[] args){
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter no. of elements in array : ");
        int len = Convert.ToInt32(Console.ReadLine());

        for (int i=0; i<len; i++){
            Console.Write("Enter a number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            numbers.Add(num);
        }

        Dictionary<int, int> FreqDict = new Dictionary<int, int>();

        foreach (int num in numbers){
            if(FreqDict.ContainsKey(num))
                FreqDict[num] ++;
            else
                FreqDict[num] = 1;
        }

        foreach (var ele in FreqDict){
            Console.WriteLine($"{ele.Key} occurs {ele.Value} time(s).");
        }
    }
}