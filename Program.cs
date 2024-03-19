using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int num = int.Parse(Console.ReadLine());

        if (num <= 0) Console.Write("Non-positive integers are not within the scope of discussion.");
        else if (num == 1) Console.Write(num + " has no prime factors.");
        else
        {
            List<int> tmp = new List<int>();
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    int j = 2;
                    bool isPrime = true;
                    while (j <= Math.Sqrt(i) && isPrime == true)
                    {
                        if (i % j == 0) isPrime = false;
                        j++;
                    }
                    if (isPrime == true) tmp.Add(i);
                }
            }
            if (tmp.Count() == 0) tmp.Add(num);

            Console.Write("The prime factors of " + num + " are: ");
            for (int i = 0; i < tmp.Count(); i++)
                Console.Write(tmp[i] + " ");
        }
    }
}
