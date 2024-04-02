using System;
using System.Reflection;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person()
    {
        Name = "Unknown";
        Age = 0;
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 获取 Person 类型
        Type personType = typeof(Person);

        // 默认构造方法
        ConstructorInfo Constructor = personType.GetConstructor(Type.EmptyTypes);

        // 创建实例对象
        Person Person = (Person)Constructor.Invoke(null);
        Console.WriteLine("Default Person - Name: {0}, Age: {1}", Person.Name, Person.Age);
    }
}
