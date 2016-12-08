using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyRunner;

namespace DailyRunner
{
    public enum WireColors
    {
        Purple = 0,
        Red = 1,
        Black = 2,
        Orange = 3,
        White = 4,
        Green = 5
    }
    public class Defuser : DailyChallenge
    {
        private static bool[,] RuleSet = new bool[,]
        {
            {false, false, true,  false, false, false },
            {true,  false, true,  true,  true,  false },
            {true,  false, true,  true,  false, false },
            {false, false, false, false, true,  true  },
            {false, false, false, false, false, true  },
            {false, true,  false, false, true,  false }
        };

        public void Start()
        {
            Console.WriteLine(Defuse("white","red","green","white"));
            Console.WriteLine(Defuse("white", "orange", "green", "white"));
        }

        public string Defuse(params string[] wires)
        {
            int currentState = -1;
            foreach(var wire in wires)
            {
                int nextState = (int)Enum.Parse(typeof(WireColors), wire, true);

                if (currentState != -1)
                {
                    if(!RuleSet[nextState, currentState])
                    {
                        return "BOOM!";
                    }
                }
                    currentState = nextState;
            }


            return "Defused";
        }
    }
}
