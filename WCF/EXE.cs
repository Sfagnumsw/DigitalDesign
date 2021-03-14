using System.IO;
using System.Collections.Generic;

namespace EXE
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = new FileStream(@"C:\Users\Sfagnum\Documents\обучение\C#\Стажировка\Практика\1\TEXT.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string AllFile = reader.ReadToEnd();
            reader.Close();

            var Serv = new ServiceReference1.Service1Client().GetTXT(AllFile);

            FileStream NewFile = new FileStream(@"C:\Users\Sfagnum\Documents\обучение\C#\Стажировка\Практика\1\NewTXT.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(NewFile);
            foreach(KeyValuePair<string,int> i in Serv)
            {
                writer.Write(i);
            }
        }
    }
}
