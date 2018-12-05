using KnowledgeStore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Encryptor.SHA256Encrypt("trungnguyen"));
            Console.ReadKey();
        }
    }
}
