using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Restaurant : Card
    {
        public Restaurant() : base(CardColor.Rouge, new int[] { 5 }, 4){}

        public override void ApplyEffect()
        {
            this.owner.GainCoins(2);
            Game.CurrentPlayer.LoseCoins(2);
        }
    }
}