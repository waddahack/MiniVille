using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Stade : Card
    {
        public Stade() : base(CardColor.Bleu, new int[] { 6 }, 6){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(4);
        }
    }
}