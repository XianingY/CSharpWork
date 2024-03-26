using System;
using System.Timers;

public class ClockEventArgs : EventArgs
{
    public string Time { set; get; }
}

public delegate void ClockEventHandler(object sender, ClockEventArgs e);

public class Clock
{
    public event ClockEventHandler Tick;
    public event ClockEventHandler Alarm;
    private System.Timers.Timer timer;
    private string alarmTime;

    public Clock(string alarmTime)
    {
        this.alarmTime = alarmTime;
        timer = new System.Timers.Timer(1000); 
        timer.Elapsed += TimerElapsed;
        timer.Start();
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (Tick != null)
        {
            ClockEventArgs args = new ClockEventArgs();
            args.Time = DateTime.Now.ToString("T");
            Tick(this, args);
        }

        if (DateTime.Now.ToString("T") == alarmTime && Alarm != null)
        {
            ClockEventArgs args = new ClockEventArgs();
            args.Time = DateTime.Now.ToString("T");
            Alarm(this, args);
        }
    }
}

public class UseClock
{
    static void Main()
    {
        Console.WriteLine("当前时间是：" + DateTime.Now.ToString("T"));
        Console.WriteLine("请输入闹钟时间（HH:MM:SS）");
        string alarmTime = Console.ReadLine();
        var clock = new Clock(alarmTime);

        clock.Tick += ShowTime;
        clock.Alarm += Alarming;
        Console.ReadLine(); 
    }

    static void ShowTime(object sender, ClockEventArgs e)
    {
        Console.WriteLine("滴答，现在是：" + e.Time);
    }

    static void Alarming(object sender, ClockEventArgs e)
    {
        Console.WriteLine("\n\n响铃，现在是：" + e.Time+"\n\n");
    }
}
