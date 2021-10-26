using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Foret : Card
    {
        public Foret() : base(CardColor.Bleu, new int[] { 5 }, 2){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(1);
        }
    }
}