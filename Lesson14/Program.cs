Task? tasks = null;
try
{
    Console.Write("Введите n:");
    int n = int.Parse(Console.ReadLine()!);
    Task task1 = Task.Run(() => Sum1(n));
    Task task2 = Task.Run(() => Sum2(n));
    tasks=Task.WhenAll(task1, task2);
    await tasks;
}
catch(Exception ex)
{
    Console.WriteLine("Исключение: " + ex.Message);
    Console.WriteLine("IsFaulted: " + tasks!.IsFaulted);
    foreach (var inx in tasks.Exception!.InnerExceptions)
    {
        Console.WriteLine("Внутреннее исключение: " + inx.Message);
    }
}
async void Sum1(int n)
{
    Random random=new Random();
    double sum = 0;
    for(int i=1; i<=n;i++)
    {
        double x=random.NextDouble()*2*Math.PI*2-2*Math.PI;
        if (x == Math.PI / 4) throw new Exception($"Деление на ноль x={x} шаг={i}");
        sum += 3 / Math.Sqrt(Math.Cos(x));
    }
    await Console.Out.WriteLineAsync($"Sum1={sum}");
}
async void Sum2(int n)
{
    Random random = new Random();
    double sum = 0;
    for (int i = 1; i <= n; i++)
    {
        int y = random.Next(-20,20);
        if (y==0) throw 
                new Exception($"Деление на ноль y={y} шаг={i}");
        long fact = 1;
        for (int j = 1; j <= y; j++) fact *= j;
        sum += Math.Pow(7,y)/(fact-Math.Pow(9,y));
    }
    await Console.Out.WriteLineAsync($"Sum2={sum}");
}