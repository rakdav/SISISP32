//TPL
//1 способ
//Task task1 = new Task(() => Console.WriteLine("Hello task1"));
//task1.Start();
//task1.RunSynchronously();
//2 способ
//Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Hello task2"));
//3 способ
//Task task3 = Task.Run(() => Console.WriteLine("Hello task3"));
//task1.Wait();
//task2.Wait();
//task3.Wait();

//Вложенные задачи
//var outer = Task.Factory.StartNew(() =>
//{
//    Console.WriteLine("Outer start");
//    var inner = Task.Factory.StartNew(() =>
//    {
//        Console.WriteLine("Inner");
//        Thread.Sleep(2000);
//        Console.WriteLine("Inner finish");
//    },TaskCreationOptions.AttachedToParent
//    );
//});
//outer.Wait();
//Console.WriteLine("End of main");
////массив 
//Task[] tasks = new Task[3]
//{
//    new Task(() => Console.WriteLine("Task 1")),
//    new Task(() => Console.WriteLine("Task 2")),
//    new Task(() => Console.WriteLine("Task 3"))
//};
//foreach (var t in tasks) t.Start();
//Task.WaitAll(tasks);

//возвращение результатов из задач
Console.Write("Введите n:");
int n=int.Parse(Console.ReadLine()!);
Console.Write("Введите x:");
double x = int.Parse(Console.ReadLine()!);

Task<double>[] tasks=new Task<double>[3]
{
    new Task<double>(()=>Sum1(n)),
    new Task<double>(()=>Sum2(n)),
    new Task<double>(()=>Sum3(n,x))
};
foreach (Task i in tasks)
    i.Start();
for(int i = 0; i < tasks.Length; i++)
{
    Console.WriteLine($"Sum{i + 1}={tasks[i].Result:F2}");
}
double Sum1(int n)
{
    double s = 0;
    for (int i = 1; i <= n; i++)
        s += (i + 1) / Math.Pow(2, i);
    return s;   
}
double Sum2(int n)
{
    double s = 0;
    for (int i = 1; i <= n; i++)
        s += (i + 2) / (2*i-1);
    return s;
}
double Sum3(int n,double x)
{
    double s = 0;
    for (int i = 1; i <= n; i++)
    {
        long F = 1;
        for (int j = 1; j <= i; j++) F *= j;
        s += Math.Pow(x, i) / F;
    }
    return s;
}