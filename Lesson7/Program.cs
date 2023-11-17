//Parallel.Invoke(
//    Display,
//    () =>
//    {
//        Console.WriteLine($"Выполняется задача:{Task.CurrentId}");
//        Thread.Sleep(1000);
//    },
//    ()=>Factorial(5)
//    );
//Parallel.For(1, 11, Factorial);

//ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() {1,3,5,8},Factorial);
//Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");
//void Display()
//{
//    Console.WriteLine($"Выполняется задача:{Task.CurrentId}");
//    Thread.Sleep(1000);
//}
//void Factorial(int n,ParallelLoopState pls)
//{
//    long F = 1;
//    for (int i = 1; i <= n; i++)
//    { 
//        F *= i;
//        if(i==5) pls.Break();
//    }
//    Console.WriteLine($"Выполняется задача:{Task.CurrentId}");
//    Thread.Sleep(1000);
//    Console.WriteLine($"Результат:{F}");
//}
Console.WriteLine("Для завершения задачи нажмите y");
CancellationTokenSource cToken=new CancellationTokenSource();
CancellationToken token = cToken.Token;
Task task = new Task(()=>
    {
        for (int i = 1;i<10;i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Операция прервана");
                return;
            }
            Console.WriteLine($"Квадрат числа {i} равен {i*i}");
            Thread.Sleep(1000);
        }
    },token
);
task.Start();
if (Console.ReadKey(true).KeyChar == 'y') cToken.Cancel();
Console.WriteLine($"Статус задачи:{task.Status}");
task.Wait();
cToken.Dispose();
