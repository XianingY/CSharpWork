using System;
using System.Collections.Generic;
using System.Linq;

// 表示订单的类
public class Order
{
    public string id;
    private OrderDetails details;

    // 构造函数，用于初始化订单信息
    public Order(string _id, string name, string client, double price)
    {
        id = _id;
        details = new OrderDetails(name, client, price);
    }

    // 重写 Equals 方法，根据 id 和订单详情来比较订单
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Order order = (Order)obj;
        return id == order.id && details.Equals(order.details);
    }

    // 重写 ToString 方法，提供订单的字符串表示形式
    public override string ToString()
    {
        return "订单编号为：" + id;
    }

    // 获取订单详情的方法
    public OrderDetails GetOrderDetails()
    {
        return details;
    }
}

// 表示订单详情的类
public class OrderDetails
{
    private string name;
    private string client;
    private double price;

    // 构造函数，用于初始化订单详情
    public OrderDetails(string name, string client, double price)
    {
        this.name = name;
        this.client = client;
        this.price = price;
    }

    // 重写 Equals 方法，比较订单详情
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        OrderDetails orderdetails = (OrderDetails)obj;
        return name == orderdetails.name && client == orderdetails.client && price == orderdetails.price;
    }

    // 重写 ToString 方法，提供订单详情的字符串表示形式
    public override string ToString()
    {
        return "订单详情：" + name + " " + client + " " + price;
    }

    // 获取订单价格的方法
    public double GetPrice()
    {
        return price;
    }
}

// 自定义订单排序委托
public delegate bool SortDelegate(Order o1, Order o2);

// 表示订单服务的类
public class OrderService
{
    List<Order> orders = new List<Order>(); // 用于存储订单的列表

    // 获取所有订单的方法
    public List<Order> GetOrders()
    {
        return orders;
    }

    // 根据订单编号查找订单的方法
    public Order Find(string id)
    {
        var target = orders.FirstOrDefault(order => order.id == id);

        if (target != null)
        {
            Console.WriteLine("订单查找成功！");
            return target;
        }
        else
        {
            throw new OrderException(id + " 未找到！");
        }
    }

    // 添加新订单的方法
    public void Add(Order neworder)
    {
        if (orders.Any(order => neworder.Equals(order)))
        {
            throw new OrderException("订单已经存在！");
        }

        orders.Add(neworder);
        Console.WriteLine("订单添加成功！");
    }

    // 根据订单编号移除订单的方法
    public void Remove(string id)
    {
        var target = orders.FirstOrDefault(order => order.id == id);

        if (target != null)
        {
            orders.Remove(target);
            Console.WriteLine("移除成功！");
        }
        else
        {
            throw new OrderException("未找到该订单！");
        }
    }

    // 修改订单的方法
    public void Change(string id, Order neworder)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].id == id)
            {
                orders[i] = neworder;
                break; // 找到并更新后，跳出循环
            }
        }
    }

    // 按订单编号排序的方法
    public List<Order> Sort()
    {
        return orders.OrderBy(order => order.id).ToList();
    }

    // 使用自定义排序委托排序订单的方法
    public List<Order> Sort(SortDelegate s)
    {
        return orders.OrderBy(order => order.GetOrderDetails().GetPrice()).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            OrderService orderservice = new OrderService();

            Order order1 = new Order("111", "a", "yang", 300);
            Order order3 = new Order("222", "b", "fanyuan", 200);
            orderservice.Add(order1);
            orderservice.Add(order3);

            List<Order> l1 = orderservice.Sort((x1, x2) => x1.GetOrderDetails().GetPrice() > x2.GetOrderDetails().GetPrice());
            l1.ForEach(x => Console.WriteLine(x));

            Order order2 = orderservice.Find("111");
            orderservice.Change("111", new Order("333", "c", "qianni", 100));
            order2 = orderservice.Find("333");
            orderservice.Remove(order2.id);
            orderservice.Remove(order3.id);

            order2 = orderservice.Find("111");
        }
        catch (OrderException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

// 表示订单服务的自定义异常类
class OrderException : ApplicationException
{
    public OrderException(string message) : base(message)
    {
        Console.WriteLine(message);
    }
}
