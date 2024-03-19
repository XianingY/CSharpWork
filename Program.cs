using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the first number: ");
        double num1 = double.Parse(Console.ReadLine());

        Console.Write("Enter the operator (+, -, *, /): ");
        char operatorChar = Console.ReadLine()[0];

        Console.Write("Enter the second number: ");
        double num2 = double.Parse(Console.ReadLine());

        double result;
        switch (operatorChar)
        {
            case '+':
                result = num1 + num2;
                break;
            case '-':
                result = num1 - num2;
                break;
            case '*':
                result = num1 * num2;
                break;
            case '/':
                if (num2 == 0)
                {
                    Console.WriteLine("Error: Division by zero");
                    return;
                }
                result = num1 / num2;
                break;
            default:
                Console.WriteLine("Error: Invalid operator");
                return;
        }

        Console.WriteLine("The result is: " + result);
    }
}