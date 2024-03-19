using System;


abstract class Shape
{
   
    public abstract double Area();

 
    public abstract double Perimeter();

  
    public abstract void Initialization();
}





class Rectangle : Shape
{
    double width;
    double height;

    public Rectangle()
    {
        Initialization();
    }

    public override double Area()
    {
        return width * height;
    }

    public override double Perimeter()
    {
        return 2 * (width + height);
    }

    public override void Initialization()
    {
        Console.WriteLine("请输入长方形的宽度：");
        string widthInput = Console.ReadLine();
        Console.WriteLine("请输入长方形的高度：");
        string heightInput = Console.ReadLine();

        if (!double.TryParse(widthInput, out width) || !double.TryParse(heightInput, out height) || width <= 0 || height <= 0)
        {
            Console.WriteLine("输入非法，请重新输入有效的宽度和高度。");
            Initialization();
        }
    }
}

class Square : Shape
{
    double side;

    public Square()
    {
        Initialization();
    }

    public override double Area()
    {
        return Math.Pow(side, 2);
    }

    public override double Perimeter()
    {
        return 4 * side;
    }

    public override void Initialization()
    {
        Console.WriteLine("请输入正方形的边长：");
        string input = Console.ReadLine();
        if (!double.TryParse(input, out side) || side <= 0)
        {
            Console.WriteLine("输入非法，请重新输入有效的边长。");
            Initialization();
        }
    }
}


class Triangle : Shape
{
    double baseLength;
    double height;

    public Triangle()
    {
        Initialization();
    }

    public override double Area()
    {
        return 0.5 * baseLength * height;
    }

    public override double Perimeter()
    {
        // 假设三角形的三边相等
        return 3 * baseLength;
    }

    public override void Initialization()
    {
        Console.WriteLine("请输入三角形的底边长度：");
        string baseInput = Console.ReadLine();
        Console.WriteLine("请输入三角形的高度：");
        string heightInput = Console.ReadLine();

        if (!double.TryParse(baseInput, out baseLength) || !double.TryParse(heightInput, out height) || baseLength <= 0 || height <= 0)
        {
            Console.WriteLine("输入非法，请重新输入有效的底边长度和高度。");
            Initialization();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
     
   
        Shape rectangle = new Rectangle();
        Console.WriteLine($"长方形的面积：{rectangle.Area()}");
        Console.WriteLine($"长方形的周长：{rectangle.Perimeter()}");

    
        Shape square = new Square();
        Console.WriteLine($"正方形的面积：{square.Area()}");
        Console.WriteLine($"正方形的周长：{square.Perimeter()}");

 
        Shape triangle = new Triangle();
        Console.WriteLine($"三角形的面积：{triangle.Area()}");
        Console.WriteLine($"三角形的周长：{triangle.Perimeter()}");
    }
}