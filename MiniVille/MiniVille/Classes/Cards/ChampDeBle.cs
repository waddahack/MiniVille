using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class ChampDeBle : Card
    {
        public ChampDeBle() : base(CardColor.Bleu, new int[] { 1 }, 1){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(1);
        }
    }
}