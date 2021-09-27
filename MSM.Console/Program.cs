using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSM.ConsoleAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(()=> {
                Console.WriteLine("");
                //,,,,,
            });
            thread.IsBackground = true;
            thread.Start();
            //Thread.Sleep();

            

            /*string str = "I am a student";
            string[] strArr = str.Split(' ');
            StringBuilder newStr = new StringBuilder();
            for (int i = 0; i < strArr.Length; i++)
            {
                newStr.AppendFormat("{0} ",strArr[strArr.Length - i - 1]);
            }
            Console.WriteLine(newStr);
            Console.ReadKey();*/
        }

        public static void tt()
        {

        }
    }
}
