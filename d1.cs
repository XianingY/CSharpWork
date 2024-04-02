using System;
using System.Collections.Generic;
using System.Linq;
public class Order
{
    public string id;
    private OrderDetails details;
    public Order(string _id, string name, string client, double price)
    {
        id = _id;
        details = new OrderDetails(name, client, price);
    }
    public override bool Equals(object obj)
    {
        Order order = obj as Order;
        return this.id == order.id && details.Equals(order.details);
    }
    public override string ToString()
    {
        return "Order id is " + id;
    }
    public Orde()rDetails GetOrderDetails
    {
        return details;
    }
}
public class OrderDetails
{
    private string name;
    private string client;
    private double price;
    public OrderDetails(string name, string client, double price)
    {
        this.name = name;
        this.client = client;
        this.price = price;
    }
    public override bool Equals(object obj)
    {
        OrderDetails orderdetails = obj as OrderDetails;
        return this.name == orderdetails.name && this.client == orderdetails.client && this.price == orderdetails.price;
    }
    public override string ToString()
    {
        return "Order details: " + name + " " + client + " " + price;
    }
    public double getprice()
    {
        return price;
    }
}
public delegate bool SortDelegate(Order o1, Order o2);
public class OrderService
{
    //订单列表
    List<Order> orders = new List<Order>();
    public List<Order> GetOrders()
    {
        return orders;
    }
    //查找订单
    public Order Find(string id)
    {
        var target = from order in orders
                     where order.id == id
                     select order;

        if (target.Count() != 0)    //查找的目标订单存在
        {
            Order a = target.FirstOrDefault();
            Console.WriteLine("Order find successly!");
            return a;
        }
        else         //查找的目标订单存在，抛出异常
        {
            throw new OrderException(id + " isn't find!");
        }

    }
    //新建订单
    public void Add(Order neworder)
    {
        //检查订单是否已经创建,若查找到已经创建则抛出异常
        foreach (var o in orders)
        {
            if (neworder.Equals(o))
            {
                throw (new OrderException("Order has already added!"));
            }
        }
        orders.Add(neworder);
        Console.WriteLine("Order adds successly!");
    }
    //删除订单
    public void Remove(string id)
    {
        var target = from order in orders
                     where order.id == id
                     select order;

        if (target.Count() != 0)
        {
            Order a = target.First();
            orders.Remove(a);
            Console.WriteLine("Remove successly!");
        }
        else
        {
            throw new OrderException("Can't find the order!");
        }
    }
    //修改订单
    public void Change(string id, Order neworder)
    {
        for (int i = 0; i < orders.Count(); i++)
        {
            if (orders[i].id == id)
            {
                orders[i] = neworder;
            }
        }

    }
    //订单排序
    public List<Order> Sort()
    {
        var query = from order in orders
                    orderby order.id
                    select order;
        return query.ToList();
    }
    //自定义排序
    public List<Order> Sort(SortDelegate s)
    {
        List<Order> orders = this.orders;
        for (int i = 0; i < orders.Count(); i++)
        {
            for (int j = 0; j < orders.Count() - i; i++)
            {
                if (s(orders[j], orders[j + 1]))
                {
                    Order o = orders[j];
                    orders[j] = orders[j + 1];
                    orders[j + 1] = o;
                }
            }
        }
        return orders;
    }
}
internal class Program
{
    static void Main(string[] args)
    {

        try
        {
            OrderService orderservice = new OrderService();

            Order order1 = new Order("123", "a", "feng", 300);
            Order order3 = new Order("124", "b", "song", 200);
            orderservice.Add(order1);
            orderservice.Add(order3);
            //lamda表达式排序
            List<Order> l1 = orderservice.Sort((x1, x2) => { return x1.GetOrderDetails().getprice() > x2.GetOrderDetails().getprice(); });
            l1.ForEach(x => Console.WriteLine(x));

            Order order2 = orderservice.Find("123");
            orderservice.Change("123", new Order("125", "c", "kou", 100));
            order2 = orderservice.Find("125");
            orderservice.Remove(order2.id);
            orderservice.Remove(order3.id);

            //orderservice.Remove("123123");
            order2 = orderservice.Find("123");
        }
        catch (OrderException ex) { }

    }
}

//自定义异常类
class OrderException : ApplicationException
{
    public OrderException(string message)
    {
        Console.WriteLine(message);
    }
}
