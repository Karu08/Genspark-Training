using System;

class Program{
    public static void Main(string[] args){
        List<int> numbers = new List<int>();
        List<int> rotated = new List<int>();
        Console.WriteLine("Enter no. of elements in array : ");
        int len = Convert.ToInt32(Console.ReadLine());

        for (int i=0; i<len; i++){
            Console.Write("Enter a number : ");
            int num = Convert.ToInt32(Console.ReadLine());
            numbers.Add(num);
        }

        int first = numbers[0];
        for(int i=1; i<len; i++){
            rotated.Add(numbers[i]);
        }
        rotated.Add(first);

        Console.WriteLine("Original list : ");
        foreach (int num in numbers){
            Console.Write(num + " ");
        }
        Console.WriteLine("\n");
        Console.WriteLine("Rotated list : ");
        foreach (int num in rotated){
            Console.Write(num + " ");
        }
    }
}
