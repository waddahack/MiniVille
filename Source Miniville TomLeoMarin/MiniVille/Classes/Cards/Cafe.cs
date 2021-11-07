using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Cafe : Card
    {
        public Cafe() : base()
        {
            Name = "Café";
            CardName = CardName.Cafe;
            Color = CardColor.Rouge;
            _activationNumbers = new int[] { 3 };
            Price = 2;
            Effet = "Recevez 1 pièce du joueur qui a lancé le dé";
        }

        public override void ApplyEffect()
        {
            if(Game.Players[Game.CurrentPlayerId].NbPiece >= 1)
                this.Owner.GainCoins(1);
            Game.Players[Game.CurrentPlayerId].LoseCoins(1);
        }
    }
}