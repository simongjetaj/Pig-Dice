using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigDice
{
    public class Game
    {
        private Random random = new Random();
        public static Player p1 = null, p2 = null;

        public static void PlayGame()
        {
            Console.Write("What is the name of Player One: ");
            string input1 = Console.ReadLine();
            string playerOne = (input1 != string.Empty) ? input1 : "Player 1";

            Console.Write("What is the name of Player Two: ");
            string input2 = Console.ReadLine();
            string playerTwo = (input2 != string.Empty) ? input2 : "Player 2";

            p1 = new Player(playerOne);
            p2 = new Player(playerTwo);


            Console.Write("Who want to start first: (1/2)");
            string input = Console.ReadLine();

            int firstRoll = 0, secondRoll = 0;
            var scoresOfPlayerOne = new List<int>();
            var scoresOfPlayerTwo = new List<int>();
            string rollOrHold = "";

            if (String.IsNullOrEmpty(input) || Convert.ToByte(input) == 1)
            {
                PlayNext(p1, p2, firstRoll, secondRoll, scoresOfPlayerOne, scoresOfPlayerTwo, rollOrHold);
            }
            else
            {
                PlayNext(p2, p1, firstRoll, secondRoll, scoresOfPlayerOne, scoresOfPlayerTwo, rollOrHold);
            }
        }

        public static void PlayNext(Player p1, Player p2, int firstRoll, int secondRoll, List<int> scoresOfPlayerOne, List<int> scoresOfPlayerTwo, string rollOrHold)
        {
            do
            {
                int[] roll = p1.RollDices();
                firstRoll = roll[0];
                secondRoll = roll[1];
                Console.WriteLine($"{p1.Name}, you rolled a {firstRoll} and a {secondRoll}.");
                
                if (firstRoll == 1 || secondRoll == 1)
                {
                    scoresOfPlayerOne.Clear();
                    p1.TotalScore = 0;
                    Console.WriteLine($"{p1.Name}, your current score is {p1.TotalScore}. Turn to {p2.Name}. Press Enter!\n");
                    Console.ReadKey();

                    PlayNext(p2, p1, 0, 0, scoresOfPlayerTwo, scoresOfPlayerOne, rollOrHold); //send p2 as p1(first player) and p1 as p2(second player)
                    return;
                }

                scoresOfPlayerOne.Add(firstRoll + secondRoll);
                
                if (p1.CalculateScore(scoresOfPlayerOne) < 0) return; 


                Console.WriteLine($"{p1.Name}, your current score is {p1.TotalScore}. Do you want to roll or hold? (r/h)");
                Console.WriteLine($"{p1.Name}: {p1.TotalScore}\n{p2.Name}: {p2.TotalScore}");

                rollOrHold = Console.ReadLine().ToLower();
                if (rollOrHold.Contains('h'))
                {
                    PlayNext(p2, p1, 0, 0, scoresOfPlayerTwo, scoresOfPlayerOne, rollOrHold); //send p2 as p1(first player) and p1 as p2(second player)
                    Console.ReadKey();
                    return;
                }
            } while (rollOrHold.Contains('r'));
        }
    }
}
