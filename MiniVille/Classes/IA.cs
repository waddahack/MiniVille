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
    
    public void BuyOrEconomy(){
        random = new Random()
        int indexChoixCarte;
        List<Pile> ChoixIA = new List<Pile>();
        foreach (Pile pile in Piles.Values) {
                if (pile.Cards.Count > 0 && IA.NbPiece >= Pile.Card[0].Price)
                {
                    ChoixIA.Add(pile);
                }
            }
            indexChoixCarte = random.next(0,ChoixIA.Count*2)
            if (indexChoixCarte == 0 || indexChoixCarte > ChoixIA.Count){ /*fait des économies*/ }
            else {
                IA.Buy(ChoixIA[indexChoixCarte-1]);
            }
        }   
    }