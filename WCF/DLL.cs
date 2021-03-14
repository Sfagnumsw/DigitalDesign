using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace DLL
{
    public class Class1
    {
        private readonly object locker = new object();
        public Dictionary<string, int> Catcher2(string text)
        {
            Dictionary<string, int> Words = new Dictionary<string, int>();
            Regex regex = new Regex("[^A-Za-zА-Яа-я]");
            text = regex.Replace(text, " ");
            text = text.ToLower();

            string[] MyList = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Parallel.ForEach(MyList, word =>
            {
                lock (locker)
                {
                    if (!Words.ContainsKey(word))
                    {
                        Words.Add(word, 1);
                    }
                    else
                    {
                        Words.TryGetValue(word, out int value);
                        Words[word] = value + 1;
                    }
                }
            });
            Words = Words.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            return Words;
        }
    }
}
