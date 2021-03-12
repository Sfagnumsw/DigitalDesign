using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace MyLibrary
{
    public class Class1
    {       
        public Dictionary<string, int> Words = new Dictionary<string, int>();
        private Dictionary<string, int> Catcher(String text)
        {
            Regex regex = new Regex("[^A-Za-zА-Яа-я]");
            text = regex.Replace(text, " ");
            text = text.ToLower();

            string[] MyList = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string word in MyList)
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
            return Words;
        }
    }
}
