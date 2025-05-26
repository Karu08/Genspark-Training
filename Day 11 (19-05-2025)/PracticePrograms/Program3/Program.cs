//Take 2 numbers from user, check the operation user wants to perform (+,-,*,/). Do the operation and print the result.

using System;

public class Program{

    public static void MathOp(int num1, int num2, int ch){
        switch(ch){
            case 1:
                Console.WriteLine(num1+num2);
                break;
            case 2:
                Console.WriteLine(num1-num2);
                break;
            case 3:
                Console.WriteLine(num1*num2);
                break;
            case 4:
                if (num2 != 0){
                   Console.WriteLine("Result: " + (num1 / num2));
                } 
                else {
                    Console.WriteLine("Division by zero is not allowed.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }

    public static void Main(string[] args){
        
        while(true){
            Console.WriteLine("1.Add  2.Subtract  3.Multiply  4.Divide  5.Exit");
            Console.WriteLine("Enter your choice : ");
            int ch = int.Parse(Console.ReadLine());

            if (ch == 5){
                Console.WriteLine("Exiting...");
                break;
            }

            MathOp(num1, num2, ch);
        }
    }
   
}