using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRunner
{
    public class RackManager1 : DailyChallenge
    {

        private static readonly char Blank = '?';
        public RackManager1()
        {
        }

        public void Start()
        {
            Console.WriteLine($"scrabble(\"ladilmy\", \"daily\")-> {RackCheck("ladilmy", "daily")}");
            Console.WriteLine($"scrabble(\"eerriin\", \"eerie\")-> {RackCheck("eerriin", "eerie")}");
            Console.WriteLine($"scrabble(\"orrpgma\", \"program\")-> {RackCheck("orrpgma", "program")}");
            Console.WriteLine($"scrabble(\"orppgma\", \"program\")-> {RackCheck("orppgma", "program")}");
            Console.WriteLine();
            Console.WriteLine($"scrabble(\"pizza??\", \"pizzazz\")-> {RackCheck("pizza??", "pizzazz")}");
            Console.WriteLine($"scrabble(\"piizza?\", \"pizzazz\")-> {RackCheck("piizza?", "pizzazz")}");
            Console.WriteLine($"scrabble(\"a??????\", \"program\")-> {RackCheck("a??????", "program")}");
            Console.WriteLine($"scrabble(\"b??????\", \"program\")-> {RackCheck("b??????", "program")}");
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
    }
}
