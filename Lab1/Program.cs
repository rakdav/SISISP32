using System.Diagnostics;

Console.Write("Введите размер массива:");
int n=int.Parse(Console.ReadLine()!);
Thread.CurrentThread.Name = "main";
Avg(n);
Thread mythread = new Thread(Avg);
mythread.Name = "back";
mythread.Start(n);
void Avg(object? size)
{
    Console.WriteLine();
    Console.WriteLine(Thread.CurrentThread.Name+" начал работу...");
    int[] mas = new int[(int)size!];
    Random random = new Random();
    for (int i = 0; i < mas.Length; i++)
    {
        mas[i] = random.Next(10, 99);
        //Console.Write(mas[i] + " ");
    }
    Stopwatch stpWatch = new Stopwatch();
    stpWatch.Start();
    double s = 0;
    for (int i = 0; i < mas.Length; i++)
    {
        s += mas[i];
    }
    Console.WriteLine(s/mas.Length);
    stpWatch.Stop();
    Console.WriteLine("StopWatch: " + stpWatch.ElapsedTicks.ToString());
    Console.WriteLine(Thread.CurrentThread.Name + " закончил работу...");
}