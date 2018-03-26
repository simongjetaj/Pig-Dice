using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigDice
{
    public class Player
    {
        public string Name { get; set; }
        public int TotalScore { get; set; }

        public Player(string name)
        {
            Name = name;
            TotalScore = 0;
        }

        public int[] RollDices()
        {
            Random random = new Random();
            int firstRoll = random.Next(6) + 1;
            int secondRoll = random.Next(6) + 1;

            return new int[2] { firstRoll, secondRoll };
        }

        public int CalculateScore(List<int> list)
        {
            int sum = 0;
            foreach (int item in list)
            {
                sum += item;
            }
            TotalScore = sum;

            if (this.TotalScore >= 100)
            {
                Console.WriteLine($"{this.Name} Won! Congratulations!");
        
                return -1;
            }
            return sum;
        }
    }
}
