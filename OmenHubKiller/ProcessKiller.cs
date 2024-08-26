using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace OmenHubKiller
{
    public class ProcessKiller
    {
        private List<String> processNames;
        public ProcessKiller(string[] processNames)
        {
            this.processNames = new List<string>(processNames);
        }

        public ProcessKiller(string fileName)
        {
            this.processNames = new List<string>();
            AppendFromFile(fileName);
        }

        private void AddEntry(string line)
        {
            int index=line.IndexOf('#');
            if (index != -1)
            {
                line= line.Substring(0, index);
            }

            if (line.Length == 0)
            {
                return;
            }
            else if (line[0] == '+') // A new file should be considered
            {
                this.AppendFromFile(line.Substring(1));
            }
            else 
            {
                this.processNames.Add(line);
            }
        }

        public void AppendFromFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        this.AddEntry(line);
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.Write("File \""+fileName+"\" not found. Press any key to close the app.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        public void Kill()
        {
            int total = processNames.Count;
            int killed = 0;
            int done = 0;

            foreach (string processName in processNames)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    done++;
                }
                else
                {
                    foreach (Process process in processes)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch(Exception)
                        {
                            Console.Write("Something went wrong while trying to kill processes associated with the process \"" + processName + "\"; make sure you're running this script as administrator. Press any key to close the app.");
                            Console.ReadKey();
                            Environment.Exit(1);
                        }
                    }
                    done++;
                    killed++;
                }
                WriteBar(total, done, killed);

                //Console.ReadKey();
            }
        }

        private void WriteBar(int total, int done, int killed)
        {
            (int x, int y)=Console.GetCursorPosition();
            Console.SetCursorPosition(0, y);

            Console.Write("[");
            int length = 40;

            int killed_length = (length * killed) / total;
            int done_length = ((length * done) / total)-killed_length;
            int left_length = length - (killed_length + done_length);

            for(int i = 0; i < killed_length; i++)
            {
                Console.Write("#");
            }
            for (int i = 0; i < done_length; i++)
            {
                Console.Write("-");
            }
            for (int i = 0; i < left_length; i++)
            {
                Console.Write(" ");
            }
            Console.Write("]");
        }
    }
}
