//Console.Write("Введите n:");
//int n=int.Parse(Console.ReadLine()!);
//await Task.Run(()=> GenNumbers());
//Sum(n);
Console.Write("Введите a:");
int a=int.Parse(Console.ReadLine()!);
Console.Write("Введите b:");
int b = int.Parse(Console.ReadLine()!);
await Task.Run(()=>SimpleSum(a,b));
SimpleSumAsync(a, b);
Console.WriteLine(await SimpleSumReturnAsync(a,b));
async Task SimpleSumAsync(int a,int b)
{
    await Task.Run(()=>SimpleSum(a,b));
}
async Task<int> SimpleSumReturnAsync(int a, int b)
{
    return await Task.Run(()=>SimpleSumReturn(a, b));
}
int SimpleSumReturn(int a, int b)
{
    int s = 0;
    for (int i = a; i <= b; i++)
    {
        int k = 0;
        for (int j = 2; j < i; j++)
        {
            if (i % j == 0)
            {
                k++;
                break;
            }
        }
        if (k == 0) s += i;
    }
    return s;
}
void SimpleSum(int a,int b)
{
    int s = 0;
    for (int i = a; i <= b; i++)
    {
        int k = 0;
        for(int j=2;j<i;j++)
        {
            if(i%j==0)
            {
                k++;
                break;
            }
        }
        if (k == 0) s += i;
    }
    Console.WriteLine($"Sum={s}");
}
async void GenNumbers()
{
    int count = 0;
    int first=0,second=0;
    Random random=new Random();
    do
    {
        first = random.Next(100, 1000000);
        second = random.Next(100, 1000000);
       // Thread.Sleep(10);
        count++;
    }
    while (first+second!=54982);
    await Console.Out.WriteLineAsync($"{first} {second} Циклов:{count}");
}
void Sum(int n)
{
    double s = 0;
    for (double i = 1; i <= n; i++)
    {
        s += Math.Pow(1 + (1 / i), i);
    }
    Console.WriteLine($"Сумма равна:{s:F2}");
}
