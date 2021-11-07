using MiniVille.Classes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniVille.Classes
{
    public class IA : Player
    {
        private static Random random;

        public IA(int nbPiece, List<Card> cards) : base("IA", nbPiece, cards)
        {
            IsAnIa = true;
        }
    
        public void BuyOrEconomy(Dictionary<CardName, Pile> piles)
        {
            random = new Random();
            int indexChoixCarte;
            List<Pile> ChoixIA = new List<Pile>();
            foreach (Pile pile in piles.Values)
            {
                if (pile.Cards.Count > 0 && NbPiece >= pile.Cards[0].Price)
                {
                    ChoixIA.Add(pile);
                }
            }
            indexChoixCarte = random.Next(0,ChoixIA.Count*2);
            if (indexChoixCarte != 0 && indexChoixCarte < ChoixIA.Count)
            {
                Buy(ChoixIA[indexChoixCarte]);
            }
        }
    }
}