using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class ChampDeBle : Card
    {
        public ChampDeBle() : base()
        {
            Name = "Champ de blé";
            Color = CardColor.Bleu;
            _activationNumbers = new int[] { 1 };
            Price = 1;
            Effet = "Recevez 1 pièce";
        }

        public override void ApplyEffect()
        {
            this.Owner.GainCoins(1);
            base.ApplyEffect();
        }
    }
}