using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Boulangerie : Card
    {
        public Boulangerie() : base()
        {
            Name = "Boulangerie";
            Color = CardColor.Vert;
            _activationNumbers = new int[] { 2 };
            Price = 1;
            Effet = "Recevez 2 pièces";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(2);
        }
    }
}
