 //Take username and password from user. Check if user name is "Admin" and password is "pass" if yes then print success message.
using System;

class Program{
    public static void Main(string[] args){
        UserLogin();
    }

    public static void UserLogin(){
        string reqUsername = "Admin";
        string reqPwd = "pass";

        for(int attempts=0; attempts<3; attempts++){
            Console.Write("Enter username : ");
            string username = Console.ReadLine();

            Console.Write("Enter password : ");
            string password = Console.ReadLine();

            if(username == reqUsername && password == reqPwd){
                Console.WriteLine("Login Successful!");
                return;
            }
            else{
                Console.WriteLine("Invalid Credentials. Please try again!");
            }
        }
        Console.WriteLine("Invalid attempts for 3 times. Exiting ...");
    }
}