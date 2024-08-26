// See https://aka.ms/new-console-template for more information
using OmenHubKiller;
using System.Diagnostics;

/*
List<Process> totalList = new List<Process>();

Process[] processes = Process.GetProcessesByName("SystemOptimizer");
foreach(Process process in processes)
{
    totalList.Add(process);
}
processes = Process.GetProcessesByName("OmenCommandCenterBackground");
foreach (Process process in processes)
{
    totalList.Add(process);
}

foreach(Process process in totalList)
{
    Console.Write("Kill process \"" + process.ProcessName + "\"? [Y|whichever other input]>");
    string? input=Console.ReadLine();
    if (input != null && input=="Y")
    {
        process.Kill();
    }
}
*/

Console.Write("Input the killer file\n>");
string? input=Console.ReadLine();
if (input == null)
{
    Console.Write("Null input.");
}
else
{
    ProcessKiller killer=new ProcessKiller(input);
    killer.Kill();
}

Console.WriteLine("\nThe application finished its job. Press any key to exit.");
Console.ReadKey();