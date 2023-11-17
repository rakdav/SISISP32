//AsParallel
//using System.Threading.Channels;

//int[] numbers = new int[] { 1, 5, 3, 6, 2, 7, 9, 4, 8, 0 };
//var squares=from n in numbers.AsParallel()
//            select Square(n);
//foreach(var i in squares)
//    Console.Write(i+" ");
//Console.WriteLine();
//var squares1=numbers.AsParallel().Select(i=>Square(i));
//foreach (var i in squares1)
//    Console.Write(i+" ");
//Console.WriteLine();
//int Square(int n) => n * n;
////ForAll()
//(from n in numbers.AsParallel() select Square(n)).ForAll(Console.WriteLine);
//numbers.AsParallel().Select(n => Square(n)).ForAll(Console.WriteLine);

using System.Collections;
try
{
    ArrayList list = new ArrayList() { 2, 3, 4, 0.6E1, 'r', 1, 5, 7, 8 };
    int[] mas = list.AsParallel().AsOrdered().OfType<int>().ToArray();
    var squares = from n in mas.AsParallel()
                  select res(n);
    foreach (var i in squares) Console.Write($"{i:F2} ");
    Console.WriteLine();
    Console.WriteLine(mas.AsParallel().Sum());
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
double res(int n)
{
    if (n == 0) throw new Exception("Деление на ноль");
    return Math.Pow(1 + (1.0 / n), n);
}
 