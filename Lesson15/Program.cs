//using System.Collections.ObjectModel;

//Console.Write("Введите n:");
//int n = int.Parse(Console.ReadLine()!);
//Console.Write(SumFromOneToCount(n));
//Console.WriteLine();
//foreach (var i in SumFromOneToCountYield(n))
//{
//    Console.Write(i);
//}
//Console.WriteLine();
//var result=await SumFromOneToCountAsync(n);
//Console.WriteLine(result);
//var src=await SumFromOneToCountTaskEnumerable(n);
//foreach (var i in src)
//{
//    Console.WriteLine($"{i}");
//}
//int SumFromOneToCount(int count)
//{
//    Console.Write("Сумма SumFromOneToCount:");
//    var sum = 0;
//    for (int i = 0; i <= count; i++) sum += i;
//    return sum;
//}
//IEnumerable<int> SumFromOneToCountYield(int count)
//{
//    Console.Write("Сумма SumFromOneToCountYield:");
//    var sum = 0;
//    for (int i = 0; i <= count; i++)
//    {
//        sum += i;
//    }
//    yield return sum;
//}

//async Task<int> SumFromOneToCountAsync(int n)
//{
//    Console.Write("Сумма SumFromOneToCountAsync:");
//    var result = await Task.Run(
//        () =>
//        {
//            var sum = 0;
//            for (int i = 0; i <=n; i++)
//            {
//                sum += i;
//            }
//            return sum;
//        }
//    );
//    return result;
//}

//async Task<IEnumerable<int>> SumFromOneToCountTaskEnumerable(int count)
//{
//    var collection = new Collection<int>();
//    var result = await Task.Run(
//        ()=>
//        {
//            var sum = 0;
//            for (var i=0;i<=count ;i++) 
//            {
//                sum += i;
//                collection.Add(sum);
//            }
//            return collection;
//        }
//        );
//    return result;
//}
using System.Collections;
using System.Collections.ObjectModel;

ArrayList list=new ArrayList()
{
    "ArrayList",-56109,3.14,"List","Sort void",0.0309,2.71E-3,"z",'F'
};

    Console.WriteLine("Меню:");
    Console.WriteLine("1 - задача 1:");
    Console.WriteLine("2 - задача 2:");
    Console.WriteLine("3 - задача 3:");
    Console.WriteLine("4 - задача 4:");
    Console.WriteLine("5 - задача 5:");
do
{
    Console.Write("Введите число:");
    int n=int.Parse(Console.ReadLine()!);
    switch(n)
    {
        case 1:
            {
                var src = await SumTypes(list);
                int k = 1;
                foreach (var i in src)
                {
                    switch(k)
                    {
                        case 1: Console.WriteLine($"Строк {i}"); break;
                        case 2: Console.WriteLine($"Целых чисел {i}"); break;
                        case 3: Console.WriteLine($"Дробных чисел {i}"); break;
                        case 4: Console.WriteLine($"Символов {i}"); break;
                        case 5: Console.WriteLine($"Всего {i}"); break;
                    }
                    k++;
                }
                async Task<IEnumerable<int>> SumTypes(ArrayList list)
                {
                    var collection = new Collection<int>();
                    int countString = 0;
                    int countInt = 0;
                    int countDouble = 0;
                    int countChar = 0;
                    var result = await Task.Run(
                        () =>
                        {
                            var sum = 0;
                            for (var i = 0; i <list.Count; i++)
                            {
                                if (list[i]!.GetType() == typeof(String)) countString++;
                                if(list[i]!.GetType() == typeof(int)) countInt++;
                                if (list[i]!.GetType() == typeof(Double)) countDouble++;
                                if (list[i]!.GetType() == typeof(Char)) countChar++;

                            }
                            collection.Add(countString);
                            collection.Add(countInt);
                            collection.Add(countDouble);
                            collection.Add(countChar);
                            collection.Add(list.Count);
                            return collection;
                        }
                        );
                    return result;
                }
            }
            break;
        case 2:
            {
                Console.WriteLine($"{await SumTypes(list):F2}");
                async Task<double> SumTypes(ArrayList list)
                {
                    var collection = new Collection<int>();

                    var result = await Task.Run(
                        () =>
                        {
                            double sum = 0;
                            for (var i = 0; i < list.Count; i++)
                            {
                                if (list[i]!.GetType() == typeof(int)) sum += (int)list[i]!;
                                if (list[i]!.GetType() == typeof(Double)) sum += (double)list[i]!;

                            }
                            return sum;
                        }
                        );
                    return result;
                }
            }
            break;
        case 3:
            {
                var src = await SumTypes(list);
                foreach (var item in src)
                {
                    Console.Write(item+" ");
                }
                Console.WriteLine();
                async Task<ArrayList> SumTypes(ArrayList list)
                {
                    var collection = new ArrayList();
                    var result = await Task.Run(
                        () =>
                        {
                            var sum = 0;
                            for (var i = 0; i < list.Count; i++)
                            {
                                if (list[i]!.GetType() == typeof(String)) collection.Add(list[i]);
                                if (list[i]!.GetType() == typeof(Char)) collection.Add(list[i]);
                            }
                            return collection;
                        }
                        );
                    return result;
                }
            }
            break;
        case 4:
            {

            }
            break;
        case 5:
            {
            }
            break;
    }
}
while (true);
