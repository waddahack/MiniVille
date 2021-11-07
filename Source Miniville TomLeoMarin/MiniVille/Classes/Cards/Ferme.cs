using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Ferme : Card
    {
        public Ferme() : base(){
            Name = "Ferme";
            CardName = CardName.Ferme;
            Color = CardColor.Bleu;
            _activationNumbers = new int[] { 2 };
            Price = 1;
            Effet = "Recevez 1 pièce";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(1);
        }
    }
}