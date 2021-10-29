using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Ferme : Card
    {
        public Ferme() : base(){
            Name = "Ferme";
            Color = CardColor.Bleu;
            _activationNumbers = new int[] { 1 };
            Price = 2;
            Effet = "Recevez 1 pièce";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(1);
            base.ApplyEffect();
        }
    }
}