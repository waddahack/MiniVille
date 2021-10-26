using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Ferme : Card
    {
        public Ferme() : base(CardColor.Bleu, new int[] { 1 }, 2){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(1);
        }
    }
}