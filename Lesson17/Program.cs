using System.Diagnostics;

var runningProces=from proc in Process.GetProcesses(".")
                  orderby proc.Id 
                  select proc;
foreach (var proc in runningProces)
{
    Console.WriteLine($"PID:{proc.Id}\t Name:{proc.ProcessName}");
}
//Console.Write("Введите id процесса:");
//int id=int.Parse(Console.ReadLine()!);
try
{
    //Process proc=Process.GetProcessById(id);
    //Console.WriteLine(proc.ProcessName);
    //ProcessThreadCollection threads=proc.Threads;
    //foreach (ProcessThread thread in threads)
    //{
    //    Console.WriteLine($"{thread.Id} {thread.StartTime.ToShortTimeString()} {thread.PriorityLevel}");
    //}
    //proc = Process.Start(@"C:\Windows\System32\calc.exe");
    //Thread.Sleep(10000);
    //foreach (var item in Process.GetProcessesByName("CalculatorApp"))
    //{
    //    item.Kill();
    //} 
    Process? proc=null;
    //ProcessStartInfo startInfo = new ProcessStartInfo("MsEdge", "www.mail.ru");
    //startInfo.UseShellExecute = true;
    //proc = Process.Start(startInfo);
    ProcessStartInfo startInfo=new ProcessStartInfo(@"C:\Users\su\Desktop\gogs.docx");
    foreach(var verbs in startInfo.Verb)
    {
        Console.WriteLine(verbs);
    }
    startInfo.UseShellExecute = true;
    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
    startInfo.Verb = "Edit";
    Process.Start(startInfo);

}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}