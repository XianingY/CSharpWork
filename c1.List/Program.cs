using System;


// 链表节点
public class Node<T>
{
    public Node<T> Next { get; set; }
    public T Data { get; set; }

    public Node(T t)
    {
        Next = null;
        Data = t;
    }
}

// 泛型链表类
public class GenericList<T>
{
    private Node<T> head;
    private Node<T> tail;

    public GenericList()
    {
        tail = head = null;
    }

    public Node<T> Head
    {
        get => head;
    }
    public void Add(T t)
    {
        Node<T> n = new Node<T>(t);
        if (tail == null)
        {
            head = tail = n;
        }
        else
        {
            tail.Next = n;
            tail = n;
        }
    }
    public void GLForEach(Action<T> action)
    {
        Node<T> cur = head;
        while(cur!= null)
        {
            action(cur.Data);
            cur= cur.Next;
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        GenericList<int> intlist = new GenericList<int>();
        for (int x = 0; x < 10; x++)
        {
            intlist.Add(x);
        }

        intlist.GLForEach(i => Console.Write(i));

        int max = 0; int min = 0; int sum = 0;
        intlist.GLForEach(i =>
        {
            if (i > max) max = i;
        }  
        );

        intlist.GLForEach(i =>
        {
 
            if (i < min) min = i;

        });

        intlist.GLForEach(i =>
        {

            sum += i;

        });
        Console.WriteLine("\nmax is: " + max);
        Console.WriteLine("min is: " + min);       
        Console.WriteLine("sum is: " + sum);
    }
}