using System.Threading.Channels;

Thread currentThread = Thread.CurrentThread;
//свойства
Console.WriteLine(currentThread.ExecutionContext);//контекс, в котором выпоняется поток
Console.WriteLine(currentThread.IsAlive);//работает ли поток
Console.WriteLine(currentThread.IsBackground);//фоновый или нет
currentThread.Name = "Коваленко";
Console.WriteLine(currentThread.Name);//имя потока
Console.WriteLine(currentThread.ManagedThreadId);//числовой идентификатор потока
Console.WriteLine(currentThread.Priority);//приоритет потока
currentThread.Priority = ThreadPriority.Highest;
Console.WriteLine(currentThread.Priority);
Console.WriteLine(currentThread.ThreadState);//состояние потока
//методы
Console.WriteLine(Thread.GetDomain());//ccылка на домен приложения
Console.WriteLine(Thread.GetDomainID());//id домена приложения
//Thread.Sleep(5000);
Console.WriteLine("Finish");
//Interrupt() - прерывает поток в состоянии WaitSleepJoin
//Join() - блокирует выполнение вызвавшего его потока до тех пор пока не завершится поток, для которого был вызыван данный метод
//Start() - запуск потока
//for (int i = 0; i < 10; i++)
//{
//    Console.Write(i + " ");
//    Thread.Sleep(1000);
//}
//Console.WriteLine();

//Создание потоков
//Thread thread1 = new Thread(Print);
//thread1.Start();
//Thread thread2 = new Thread(new ThreadStart(Print));
//thread2.Start();
//Thread thread3 = new Thread(()=>Console.WriteLine("Hello thread"));
//thread3.Start();
//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine("main thread:" + i);
//    Thread.Sleep(1000);
//}

//void Print()
//{
//    for (int i = 0; i < 10; i++)
//    {
//        Console.WriteLine("2 thread:" + i);
//        Thread.Sleep(500);
//    }
//}

//потоки с параметрами

//Thread thread1 =new Thread(new ParameterizedThreadStart(Print));
//Console.Write("Введите сообщение:");
//string mes=Console.ReadLine()!;
//thread1.Start(mes);


//void Print(object mes) => Console.WriteLine(mes);
Thread thread2 = new Thread(Power);
thread2.Start(10);
void Power(object? obj)
{
    if (obj is int n)
    {
        Console.WriteLine($"{n}*{n}={n * n}");
    }
}