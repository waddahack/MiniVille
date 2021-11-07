using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Superette : Card
    {
        public Superette() : base()
        {
            Name = "Superette";
            CardName = CardName.Superette;
            Color = CardColor.Vert;
            Price = 2;
            _activationNumbers = new int[] { 4 };
            Effet = "Recevez 3 pièces";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(3);
        }
    }
}