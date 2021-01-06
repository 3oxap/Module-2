using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MyProject
{
    public class FileSystemVisitor
    {
        public event Func<string,string> Start;
        public event Func<string,string> Finish;
        public event Func<string,string> Finded;
        public event Func<string,string> filterFinded;

      
        string _path { get; set; }
        string _filter{ get; set; }
        
      
        public FileSystemVisitor() { }
       
        public FileSystemVisitor(string path,  string filter="")
        {
            _path = path;
            _filter =filter;
           
        }
        public void output()
        {
            Console.WriteLine(_path + " " + _filter);
        }
       
        public IEnumerable<string> OutputFolder(int choice)
        {
            string[] folders= null;
            Start?.Invoke("Start");
            try
            {
                folders = Directory.GetFiles(_path, _filter, SearchOption.AllDirectories);
            }
            catch(UnauthorizedAccessException)
            {
                Console.WriteLine("Access denieds");
                
            }
          
            if (folders != null)
            {
                if (choice == 1)
                {
                    filterFinded?.Invoke("With filtration");
                    for (int i = 0; i < folders.Length; i++)
                    {
                        yield return folders[i];
                    }
                }
                else
                {
                    Finded?.Invoke("No filtration");
                    for (int i = 0; i < folders.Length; i++)
                    {
                        yield return folders[i];
                    }
                }
            }
            Finish("Finish");
            yield break;
        }

    }
}
