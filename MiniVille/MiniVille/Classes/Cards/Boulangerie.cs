using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Boulangerie : Card
    {
        public Boulangerie() : base(CardColor.Vert, new int[] { 2 }, 1){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(2);
        }
    }
}
