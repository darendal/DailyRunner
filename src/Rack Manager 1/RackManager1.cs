using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyRunner
{
    public class RackManager1 : DailyChallenge
    {

        private static readonly char Blank = '?';
        private static readonly int[] LetterScore = new int[]{1,3,3,2,1,4,2,4,1,8,5,1,3,1,1,3,10,1,1,1,1,4,4,8,4,10};

        public RackManager1()
        {
        }

        public void Start()
        {
            Console.WriteLine("Score \"oology\": " + ScoreWord("oology"));
            Console.WriteLine($"scrabble(\"ladilmy\", \"daily\")-> {RackCheck("ladilmy", "daily")}");
            Console.WriteLine($"scrabble(\"eerriin\", \"eerie\")-> {RackCheck("eerriin", "eerie")}");
            Console.WriteLine($"scrabble(\"orrpgma\", \"program\")-> {RackCheck("orrpgma", "program")}");
            Console.WriteLine($"scrabble(\"orppgma\", \"program\")-> {RackCheck("orppgma", "program")}");
            Console.WriteLine();
            Console.WriteLine($"scrabble(\"pizza??\", \"pizzazz\")-> {RackCheck("pizza??", "pizzazz")}");
            Console.WriteLine($"scrabble(\"piizza?\", \"pizzazz\")-> {RackCheck("piizza?", "pizzazz")}");
            Console.WriteLine($"scrabble(\"a??????\", \"program\")-> {RackCheck("a??????", "program")}");
            Console.WriteLine($"scrabble(\"b??????\", \"program\")-> {RackCheck("b??????", "program")}");

            var x = LongestWord("seevurtfci");
        }

        public bool RackCheck(string tiles, string word)
        {
            if(tiles.Equals(word))
            {
                return true;
            }

            if(tiles.Length>=word.Length)
            {
                var wordFreq = GetCharacterFrequency(word);
                var tileFreq = GetCharacterFrequency(tiles);

                foreach(var entry in wordFreq)
                {
                    int tileCount = 0;

                    tileFreq.TryGetValue(entry.Key, out tileCount);

                    if( !(tileCount >= entry.Value))
                    {
                        int numberBlanks = 0;

                        if(tileFreq.TryGetValue(Blank,out numberBlanks))
                        {
                            int tilesNeeded = entry.Value - tileCount;
                            if( numberBlanks >= tilesNeeded )
                            {
                                tileFreq[Blank] -= tilesNeeded;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public int ScoreWord(string word)
        {
            int result = 0;

            for(int i =0; i<word.Length;i++)
            {
                char currentChar = word[i];
               result += (currentChar == Blank ? 0 : LetterScore[currentChar % 'a']) * (i + 1);
            }

            return result;
        }

        public Dictionary<char, int> GetCharacterFrequency(string word)
        {
            var returnable = new Dictionary<char, int>();

            foreach(char c in word)
            {
                int currentValue = 0;

                if(returnable.TryGetValue(c, out currentValue))
                {
                    returnable[c]++;
                }
                else
                {
                    returnable.Add(c, 1);
                }

            }
            return returnable;
        }

        public string LongestWord(string tiles)
        {
            return File.ReadAllLines("C:\\Users\\ware\\Desktop\\words.txt")
                .Where(x => Regex.IsMatch(x, @"^[A-Za-z]{1," + tiles.Length + "}$") && RackCheck(tiles, x))
                .OrderByDescending(x => ScoreWord(x))
                .FirstOrDefault();
        }
    }
}
