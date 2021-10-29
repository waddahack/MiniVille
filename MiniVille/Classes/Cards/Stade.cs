using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Stade : Card
    {
        public Stade() : base(){
            Name = "Stade";
            Color = CardColor.Bleu;
            _activationNumbers = new int[] { 6 };
            Price = 6;
            Effet = "Recevez 4 pièce";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(4);
            base.ApplyEffect();
        }
    }
}