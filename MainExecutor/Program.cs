using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCS800API;

namespace MainExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            SCS800API.SCS800API Filter = new SCS800API.SCS800API();
            Filter.filterTest();
            
            Console.ReadKey();
        }
    }
}
