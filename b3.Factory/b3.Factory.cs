using System;

abstract class Product
{

    public abstract double Area();


    public abstract double Perimeter();


}




class Rectangle : Product
{
    double width;
    double height;

    public Rectangle()
    {
        Random rnd = new Random();   
         width = rnd.Next(1, 101); // 生成1到100之间的随机整数
         height = rnd.Next(1, 101); // 生成1到100之间的随机整数
    }

    public override double Area()
    {
        return width * height;
    }

    public override double Perimeter()
    {
        return 2 * (width + height);
    }

}

class Square : Product
{
    double side;

    public Square()
    {
        Random rnd = new Random();
         side = rnd.Next(1, 101); // 生成1到100之间的随机整数

    }

    public override double Area()
    {
        return Math.Pow(side, 2);
    }

    public override double Perimeter()
    {
        return 4 * side;
    }


}


class Triangle : Product
{
    double baseLength;
    double height;

    public Triangle()
    {
        Random rnd = new Random();
         baseLength = rnd.Next(1, 101); // 生成1到100之间的随机整数
         height = rnd.Next(1, 101); // 生成1到100之间的随机整数
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

}

class Factory
{
    //静态工厂方法
    public static Product GetProduct(string arg)
    {
        Product product = null;
        if (arg.Equals("Square"))
        {
            product = new Square();
            
        }
        else if (arg.Equals("Rectangle"))
        {
            product = new Rectangle();
           
        }
        else if (arg.Equals("Triangle"))
        {
            product = new Triangle();   
           
        }
        return product;
    }
}

class Program
{
    static void Main(string[] args)
    {
        double sum = 0;
        for(int i = 0; i < 10; i++) {
            string name = " ";
            Random random = new Random();
            int num= random.Next(1,100);

            switch (num%3)
            {
                case 0:name = "Square";break;
                case 1:name = "Rectangle";break;
                case 2:name = "Triangle";break;
            }

            Product product;
            product = Factory.GetProduct(name); //通过工厂类创建产品对象

            sum += product.Area();
            Console.WriteLine("第"+i+"个创建的随机图形是"+name+" \n面积是"+product.Area());
        }

        Console.WriteLine("\n10个随机图形的面积和是： " + sum);
    }
}
