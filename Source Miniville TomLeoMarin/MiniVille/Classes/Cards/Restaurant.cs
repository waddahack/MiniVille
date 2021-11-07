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
            for(int i = 0; i < 2; i++)
                if (Game.Players[Game.CurrentPlayerId].NbPiece >= 1)
                    this.Owner.GainCoins(1);
            Game.Players[Game.CurrentPlayerId].LoseCoins(2);
        }
    }
}