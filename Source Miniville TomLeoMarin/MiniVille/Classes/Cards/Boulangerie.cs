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
            CardName = CardName.Boulangerie;
            Color = CardColor.Vert;
            _activationNumbers = new int[] { 2, 3 };
            Price = 1;
            Effet = "Recevez 1 pièce";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(1);
        }
    }
}
