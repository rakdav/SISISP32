Task<int> task1 = new Task<int>(()=>GetMonth());
Task<int> task2 = task1.ContinueWith(task1 => GetDay(task1.Result));
Task<int[]> task3 = task2.ContinueWith(task2 => HMS());
Task task4 = task3.ContinueWith(task3 => Print(task1.Result, task2.Result,
    task3.Result[0], task3.Result[1], task3.Result[2]));
task1.Start();
task2.Wait();
task3.Wait();
task4.Wait();
int GetMonth()
{
    Random random=new Random();
    return random.Next(1, 12);
}
int GetDay(int m)
{
    Random random=new Random();
    int n=random.Next(1,DateTime.DaysInMonth(DateTime.Now.Year,m));
    return n;
}
int[] HMS()
{
    Random random=new Random();
    int[] mas=new int[3];
    mas[0]=random.Next(1,23);
    mas[1] = random.Next(1, 59);
    mas[2] = random.Next(1, 59);
    return mas;
}
void Print(int month,int d,int h,int min,int s)
{
    Console.WriteLine($"{month} {d} {h}:{min}:{s}");
}