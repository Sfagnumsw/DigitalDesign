using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Class1
    {       
        #region PRIVATE
        Stopwatch stopWatch = new Stopwatch();
        private Dictionary<string, int> Catcher(string text)
        {
            stopWatch.Start();
            Dictionary<string, int> Words = new Dictionary<string, int>();
            Regex regex = new Regex("[^A-Za-zА-Яа-я]");
            text = regex.Replace(text, " ");
            text = text.ToLower();

            string[] MyList = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in MyList)
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
            Words = Words.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Приватный: " + elapsedTime);

            return Words;
        }
        #endregion
        #region PUBLIC
        private readonly object locker = new object();
        public Dictionary<string, int> Catcher2(string text)
        {
            stopWatch.Start();
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

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Публичный: " + elapsedTime);

            return Words;
        }
        #endregion
    }
}
