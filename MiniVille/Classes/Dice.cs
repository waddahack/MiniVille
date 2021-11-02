using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniVille.Classes
{
    public class Dice
    {
        public int Value { get; set; }
        private static Random rand = new Random();
        public static int DiceWidth { get; set; }
        public static int DiceHeight { get; set; }
        public Dice()
        {
            Value = 1;
            DiceWidth = 7;
            DiceHeight = 5;
        }

        public void Throw()
        {
            Value = rand.Next(1, 7);
        }

        public void Render(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            string topAndBotLine = "+" + String.Concat(Enumerable.Repeat("-", DiceWidth - 2)) + "+";
            Console.SetCursorPosition(x, y);
            Console.Write(topAndBotLine);
            for (int i = 0; i < DiceHeight - 2; i++)
            {
                Console.SetCursorPosition(x, y + 1 + i);
                if (i == 0)
                    Console.Write("|{0}{2}{1}|", Value > 3 ? "0" : " ", Value > 1 ? "0" : " ", String.Concat(Enumerable.Repeat(" ", DiceWidth - 4)));
                else if (i == 1)
                    Console.Write("|{1}{0}{1}|", Value % 2 != 0 ? "0" : " ", String.Concat(Enumerable.Repeat(" ", (int)MathF.Floor((DiceWidth - 3) / 2))));
                else
                    Console.Write("|{0}{2}{1}|", Value > 1 ? "0" : " ", Value > 3 ? "0" : " ", String.Concat(Enumerable.Repeat(" ", DiceWidth - 4)));
            }
            Console.SetCursorPosition(x, y + DiceHeight - 1);
            Console.Write(topAndBotLine);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
