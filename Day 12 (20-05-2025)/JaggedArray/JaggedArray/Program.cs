using System;

namespace JaggedArray
{
    class Post
    {
        public string Caption { get; set; }
        public int Likes { get; set; }
    }

    class Program {
        static void Main()
        {
            Console.WriteLine("Enter no of users : ");
            int numUsers = int.Parse(Console.ReadLine());

            Post[][] userPosts = new Post[numUsers][];

            for (int i = 0; i < numUsers; i++)
            {
                Console.WriteLine($"User {i + 1} : How many posts? : ");
                int numPosts = int.Parse(Console.ReadLine());
                userPosts[i] = new Post[numPosts];

                for (int j = 0; j < numPosts; j++)
                {
                    Console.WriteLine($"Enter caption for post {j + 1} : ");
                    string caption = Console.ReadLine();
                    Console.WriteLine("Enter likes : ");
                    int likes = int.Parse(Console.ReadLine());

                    userPosts[i][j] = new Post
                    {
                        Caption = caption,
                        Likes = likes
                    };

                }

            }

            Console.WriteLine("\n--- All Instagram Posts ---");
            for (int i = 0; i < userPosts.Length; i++)
            {
                Console.WriteLine($"\nUser {i + 1} Posts:");
                for (int j = 0; j < userPosts[i].Length; j++)
                {
                    Console.WriteLine($"Post {j + 1}: \"{userPosts[i][j].Caption}\" - {userPosts[i][j].Likes} likes");
                }
            }
        }
    }

}