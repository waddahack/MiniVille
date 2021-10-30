using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Foret : Card
    {
        public Foret() : base()
        {
            Name = "Forêt";
            CardName = CardName.Foret;
            Color = CardColor.Bleu;
            _activationNumbers = new int[] { 5 };
            Price = 2;
            Effet = "Recevez 1 pièce";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(1);
        }
    }
}