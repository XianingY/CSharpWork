using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
 
        Random random = new Random();
        List<int> numbers = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            numbers.Add(random.Next(0, 1001));
        }


        var sortedNumbers = numbers.OrderByDescending(n => n);

   
        int sum = sortedNumbers.Sum();

   
        double average = sortedNumbers.Average();

     
        Console.WriteLine("排序后的整数数组：");
        foreach (var number in sortedNumbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine();
        Console.WriteLine("数组的和为：" + sum);
        Console.WriteLine("数组的平均数为：" + average);
    }
}
