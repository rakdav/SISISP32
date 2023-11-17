using System.Net;

string theBook =string.Empty;
Task task = Task.Factory.StartNew(()=>GetBook());
Console.WriteLine("Загрузка началась...");
Console.ReadLine();
void GetBook()
{
    WebClient wc=new WebClient();
    wc.DownloadStringCompleted += (s, eArgs) =>
    {
        theBook = eArgs.Result;
        Console.WriteLine(theBook);
        GetStats();
    };
    wc.DownloadStringAsync(new Uri("https://gutenberg.org/cache/epub/71755/pg71755.txt"));
}
void GetStats()
{
    string[] words = theBook.Split(
        new char[] { ' ', '\u000A', ',','.',';','?',':','!','-','/'},
        StringSplitOptions.RemoveEmptyEntries);
    //string[] tenMostCommon=FindTenMostCommon(words);
    //string longest = FindLongestWord(words);
    string[] tenMostCommon = null;
    string longest=string.Empty;
    Parallel.Invoke(
        () =>
        {
            tenMostCommon=FindTenMostCommon(words);
        },
        ()=>
        {
            longest=FindLongestWord(words);
        }
        );
    foreach (var item in tenMostCommon)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine($"Самое длинное слово:{longest}");
}
string[] FindTenMostCommon(string[] words)
{
    var frequencyOrder = from w in words
                         where w.Length > 6
                         group w by w into g
                         orderby g.Count() descending
                         select g.Key;
    string[] commonWord=frequencyOrder.Take(10).ToArray();
    return commonWord;
}
string FindLongestWord(string[] words)
{
    return (from w in words orderby w.Length descending select w).FirstOrDefault();
}
