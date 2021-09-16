using System;

using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FreqParsingText
{
    class Program
    {
        static List<string> frequenttriplets = new List<string>();
        static List<string> wordList = new List<string>();
        static void Main(string[] args)
        {           

            Console.WriteLine("input path to .TXT file: ");
            string path = Console.ReadLine();

            long prStart = DateTime.Now.Ticks;

            if (!File.Exists(path))
            {
                Console.WriteLine("Incorrect path!");
            }
            else
            {
                string filesource = File.ReadAllText(path);
                wordList = new List<string>(filesource.Split(' ').ToList());
                
                frequenttriplets = GetTripleSym(wordList);
                var result = frequenttriplets.GroupBy(s => s)
                    .OrderByDescending(g => g.Count())
                    .Take(10)
                    .Select(g => g.Key);
                result.ToList().Aggregate("", (c, n) => c + ("," + n)).Substring(1).ToList().ForEach(Console.Write);
                Console.WriteLine();
            }

            Console.WriteLine("execution time: {0} ms.", DateTime.Now.Ticks - prStart);
            Console.ReadKey();
        }
        private static List<string> GetTripleSym(List<string> words)
        {
            var tripleSynList = new List<string>();

            Parallel.ForEach(words, word =>
            {

                for (int i = 0; i < word.Length - 2; i++)
                {
                    string s = word.Substring(i, 3);
                    if (s.Count(s.First().Equals) == 3)
                        tripleSynList.Add(word.Substring(i, 3));
                }
            });

            return tripleSynList.ToList();
        }
    }
}
