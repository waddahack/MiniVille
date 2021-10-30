using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Restaurant : Card
    {
        public Restaurant() : base()
        {
            Name = "Restaurant";
            CardName = CardName.Restaurant;
            Color = CardColor.Rouge;
            _activationNumbers = new int[] { 5 };
            Price = 4;
            Effet = "Recevez 2 pièces du joueur qui a lancé le dé";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(2);
            Game.Players[Game.CurrentPlayerId].LoseCoins(2);
        }
    }
}