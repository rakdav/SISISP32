//синхронизация locker
//проблема

//int x = 0;
//for(int i=1;i<5;i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}

//void Print()
//{
//    x = 1;
//    for(int i=6;i<10; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//        x++;
//        Thread.Sleep(1000);
//    }
//}

//решение
//int x = 0;
//object locker = new();
//for (int i = 1; i < 5; i++)
//{
//    Thread thread = new Thread(PrintLock);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void PrintLock()
//{
//    lock (locker)
//    {
//        x = 1;
//        for (int i = 6; i < 10; i++)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//            x++;
//            Thread.Sleep(1000);
//        }
//    }
//}

//мониторы
//int x = 6;
//object locker = new();
//for (int i = 1; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}

//void Print()
//{
//    bool acquireLock = false;
//    try
//    {
//        Monitor.Enter(locker, ref acquireLock);
//        for (int i = 6; i < 10; i++)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//            x--;
//            Thread.Sleep(1000);
//        }
//    }
//    finally
//    {
//        if (acquireLock) Monitor.Exit(locker);
//    }
//}

//класс AutoResetEvent
//int x = 6;
//AutoResetEvent waitHendler = new AutoResetEvent(true);
//for (int i = 1; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void Print()
//{
//    waitHendler.WaitOne();
//    for (int i = 6; i < 10; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//        x--;
//        Thread.Sleep(1000);
//    }
//    waitHendler.Set();
//}

//Mutex
//int x = 6;
//Mutex murex = new();
//for (int i = 1; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}

//void Print()
//{
//    murex.WaitOne();
//    for (int i = 6; i < 10; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//        x--;
//        Thread.Sleep(1000);
//    }
//    murex.ReleaseMutex();
//}

//Semaphore

for (int i = 1; i < 6; i++)
{
    Reader reader = new Reader(i);
}
class Reader
{
    static Semaphore sem = new Semaphore(3, 3);
    Thread thread;
    int count = 3;
    public Reader(int i)
    {
        thread = new Thread(Read);
        thread.Name = $"Читатель {i}";
        thread.Start();
    }
    public void Read()
    {
        while(count>0)
        {
            sem.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} входит в библиотеку");
            Console.WriteLine($"{Thread.CurrentThread.Name} читает");
            Thread.Sleep(1000);
            Console.WriteLine($"{Thread.CurrentThread.Name} уходит из библиотеки");
            sem.Release();
            count--;
            Thread.Sleep(1000);
        }
    }
}