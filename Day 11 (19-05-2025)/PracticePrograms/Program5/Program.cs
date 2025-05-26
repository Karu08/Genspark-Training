//Take 10 numbers from user and print the number of numbers that are divisible by 7.

using System.IO;
using System;
using System.Collections.Generic;

class Program{

    public static void Main(string[] args){
        List<int> numbers = new List<int>();
        int cnt = 0;

        for (int i=0; i<10; i++){
            Console.Write("Enter a number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            numbers.Add(num);
        }
        
        foreach (int num in numbers){
           if (num % 7 == 0){
               cnt++;
           }
       }

        Console.WriteLine("The count of numbers divisible by 7 is : "+cnt);
    }
}
