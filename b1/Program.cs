using System;
using System.Xml.Serialization;

class Program
{

    static void Main()
    {
        int length = 10;

        int[] randomNumbers = GenerateRandomIntArray(length);

        Console.WriteLine("The array generated is: ");
        foreach (int num in randomNumbers)
        {
            Console.WriteLine(num+" ");
        }
        Console.WriteLine();

        int max =findMax(randomNumbers);
        int min = findMin(randomNumbers);
        double avg= getAverage(randomNumbers);
        int sum= getSum (randomNumbers);

        Console.WriteLine("Max is: " + max);
        Console.WriteLine("Min is: " + min);
        Console.WriteLine("Avg is: " + avg);
        Console.WriteLine("Sum is: "+sum);
        
    }


    static int[] GenerateRandomIntArray(int length)
    {
        Random random = new Random();
        int[] array= new int[length];   
        for (int i = 0; i < length; i++)
        {
            array[i]=random.Next(1,114515);
            //在1到114514之间生成
        }
        return array;   
    }

    static int findMax(int[] array)
    {
        int max = array[0];
        foreach(int num in array)
        {
            if (num > max) max = num;      
        }
        return max;
    }

    static int findMin(int[] array)
    {
        int min= array[0];
        foreach (int num in array)
        {
            if (num > min) min = num;
        }
        return min;

    }       

    static double getAverage(int[] array)
    {
        double sum = 0; 
        foreach(int num in array)
        {
            sum += num;
        }

        return sum/array.Length;

    }   
    


    static int getSum(int[] array)
    {
        int sum = 0;    
        foreach(int num in array)
        {
            sum += num; 
        }
        return sum;

    }


}