using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRunner
{
    public class RackManager1 : DailyChallenge
    {
        public RackManager1()
        {
        }

        public void Start()
        {
            Console.WriteLine($"scrabble(\"ladilmy\", \"daily\")-> {RackCheck("ladilmy", "daily")}");
            Console.WriteLine($"scrabble(\"eerriin\", \"eerie\")-> {RackCheck("eerriin", "eerie")}");
            Console.WriteLine($"scrabble(\"orrpgma\", \"program\")-> {RackCheck("orrpgma", "program")}");
            Console.WriteLine($"scrabble(\"orppgma\", \"program\")-> {RackCheck("orppgma", "program")}");
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
                        return false;
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
