using System.IO;
using System;

public class Program
{
   
    public static void Main(string[] args)
    {
        bool res;
        int a;
        Console.WriteLine("Enter first number : ");
        string num1 = Console.ReadLine();
        res = int.TryParse(num1, out a);
        if(!res){
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.WriteLine("Enter second number : ");
        int num2 = int.Parse(Console.ReadLine());

        int largest = FindLargestNum(a, num2);
        Console.WriteLine("The largest number is : " + largest);
    }

    static int FindLargestNum(int num1, int num2){
        return (num1>num2)? num1 : num2;
    }

}
