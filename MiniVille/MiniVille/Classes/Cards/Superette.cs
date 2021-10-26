using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Superette : Card
    {
        public Superette() : base(CardColor.Vert, new int[] { 4 }, 2){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(3);
        }
    }
}