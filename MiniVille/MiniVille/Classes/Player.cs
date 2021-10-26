using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    public class Player
    {
        public bool IsAlive { get; set; }
        public string Name { get; set; }
        private int nbPiece;
        public List<Card> Hand { get; set; }

        public Player(string name, int nbPiece, List<Card> hand)
        {
            Name = name;
            IsAlive = true;
            this.nbPiece = nbPiece;
            this.Hand = hand;
        }
        
        public void Buy(Pile pile){
            Card card = pile.Draw();
            if (card.Price <= nbPiece)
                nbPiece -= card.Price;
            else
                pile.PutBack(card);
        }

        public void GainCoins (int amount){ // MyPlayer.GainCoins(2);
            nbPiece += amount;
        }

        public void LoseCoins (int amount){
            if(nbPiece > 0)
                nbPiece -= amount;
            else
                IsAlive = false;
        }

        public bool HasRedCard()
        {
            bool b = false;
            foreach (Card c in Hand)
                if (c.Color == Card.CardColor.Rouge)
                    b = true;
            return b;
        }
    }
}
