using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Cafe : Card
    {
        public Cafe() : base(CardColor.Rouge, new int[]{3}, 2){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(1);
            Game.CurrentPlayer.LoseCoins(1);
        }
    }
}