using System.IO;
using System.Reflection;
using MyLibrary;// Dll
using System.Collections.Generic;

namespace Searching
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = new FileStream(@"C:\Users\Sfagnum\Documents\обучение\C#\Стажировка\Практика\1\TEXT.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string AllFile = reader.ReadToEnd();
            reader.Close();

            Class1 c = new Class1();
            var R = c.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            FileStream NewFile = new FileStream(@"C:\Users\Sfagnum\Documents\обучение\C#\Стажировка\Практика\1\NewTXT.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(NewFile);

            object[] arguments = { AllFile };
            for (int i = 0; i < R.Length; i++)
            {
                if(R[i].Name == "Catcher")
                {                 
                    var MyMethod = R[i].Invoke(c, arguments);
                    Dictionary<string, int> sss = (Dictionary<string, int>)MyMethod;                   
                    foreach(KeyValuePair<string,int> key in sss)
                    {
                        writer.Write(key.ToString()+"\n");
                    }
                }
            }
            writer.Close();
        }
    }
}
