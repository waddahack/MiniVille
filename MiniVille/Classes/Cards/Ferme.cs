using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes.Cards
{
    class Ferme : Card
    {
        public Ferme(CardColor color, int[] activationNumbers, int reward, int price)
        {
            base(color, activationNumbers, reward, price);
        }

        public override void ApplyEffect()
        {

        }
    }
}