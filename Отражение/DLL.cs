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
            Regex regex1 = new Regex(@"человек(\w*)");
            MatchCollection matches1 = regex1.Matches(text);
            Words.Add("человек", matches1.Count);

            Regex regex2 = new Regex(@"владык(\w*)");
            MatchCollection matches2 = regex2.Matches(text);
            Words.Add("владыка", matches2.Count);

            Regex regex3 = new Regex(@"увидел(\w*)");
            MatchCollection matches3 = regex3.Matches(text);
            Words.Add("увидел", matches3.Count);

            Regex regex4 = new Regex(@"красив(\w*)");
            MatchCollection matches4 = regex4.Matches(text);
            Words.Add("красиво", matches4.Count);

            Words = Words.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            return Words;
        }
    }
}
