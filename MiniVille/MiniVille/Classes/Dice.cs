using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    public class Dice
    {
        public int Value { get; set; }
        private static Random rand = new Random();
        public Dice()
        {
            Value = 1;
        }

        public void Throw()
        {
            Value = rand.Next(1, 7);
        }
    }
}
