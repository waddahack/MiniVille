using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    class Dice
    {
        public int Num { get; set; }
        private static Random rand = new Random();
        public Dice()
        {
            Num = 1;
        }

        public void Throw()
        {
            Num = rand.Next(1, 7);
        }
    }
}
