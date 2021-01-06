using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Threading;

namespace MyProject
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var File1 = new FileSystemVisitor();
           
            Console.WriteLine("search with(1) or without(2) filters");
            int choice =Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1://с фильтром
                    {
                        Console.WriteLine("Enter path and filter.");
                        string path = Console.ReadLine();
                        string filters = Console.ReadLine();

                        Console.WriteLine("Enter abort sourch");
                        string abort = Console.ReadLine();

                        Console.WriteLine("Enter clear sourch");
                        string clear = Console.ReadLine();
                        var File = new FileSystemVisitor(path, Func(filters, filter => filters));

                        File.Start += Messeng;
                        File.Finish += Messeng;
                        File.filterFinded += Messeng;

                        File.filterFinded += Finded(File, abort,clear,choice);
                        break;
                    }
                case 2://без филтра
                    {
                        Console.WriteLine("Enter path");
                        string path = Console.ReadLine();

                        Console.WriteLine("Enter abort sourch");
                        string abort = Console.ReadLine();

                        Console.WriteLine("Enter clear sourch");
                        string clear = Console.ReadLine();

                        var File = new FileSystemVisitor(path);

                        File.Start += Messeng;
                        File.Finish += Messeng;
                        File.Finded += Messeng;

                        File.Finded += Finded(File,abort,clear,choice);
                        break;
                    }
            }
            Console.ReadLine();
        }

         static string Func(string filter, Func<string, string> filters) => filters(filter);

        
        static string Messeng(string mess)
        {
            Console.WriteLine(mess);
            return null;
        }
        static Func<string,string> Finded(FileSystemVisitor file,string abort,string clear, int choice)
        {
            var str = file.OutputFolder(choice);
            bool checkAbort = false;
            foreach (var item in str)
            {
               
                bool checkDivided=false;
                var divided = item.Split("\\");
                for (int i = 0; i < divided.Length; i++)
                {
                    if (divided[i].EndsWith(abort)  )
                    {
                        checkAbort =true;
                    }
                    if (divided[i] == clear)
                    {
                        checkDivided = true;
                    }
                }
                if (!checkDivided&&!checkAbort)
                {
                    for (int i = 0; i < divided.Length; i++)
                    {
                        Console.Write(divided[i] + @"\");
                    }
                    Console.WriteLine();
                }
               
               
            }

            return null;
        }

    }
}
