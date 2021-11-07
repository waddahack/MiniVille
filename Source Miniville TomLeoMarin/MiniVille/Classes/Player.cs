using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    public class Player
    {
        public bool IsAlive { get; set; }
        public string Name { get; set; }
        public int NbPiece { get; set; }
        public List<Card> Hand { get; set; }

        public bool IsAnIa {get; protected set;}

        public Player()
        {}

        public Player(string name, int nbPiece, List<Card> hand)
        {
            Name = name;
            IsAlive = true;
            this.NbPiece = nbPiece;
            this.Hand = hand;
            IsAnIa = false;
        }
        
        public void AddToHand(Card card)
        {
            card.Owner = this;
            Hand.Add(card);
        }

        public bool Buy(Pile pile){
            Card card = pile.Draw();
            if (card.Price <= NbPiece)
            {
                NbPiece -= card.Price;
                AddToHand(card);
                return true;
            }
            else
            {
                pile.PutBack(card);
                return false;
            }
        }

        public void GainCoins (int amount){ // MyPlayer.GainCoins(2);
            NbPiece += amount;
        }

        public void LoseCoins (int amount){
            if(NbPiece > 0)
                NbPiece -= amount;
            else
                IsAlive = false;
        }

        public List<Card> GetUniqueCards()
        {
            List<Card> uniqueCards = new List<Card>();
            bool containSameCard;
            foreach (Card c in Hand)
            {
                containSameCard = false;
                foreach (Card uniqueCard in uniqueCards)
                    if(c.Name == uniqueCard.Name)
                    {
                        containSameCard = true;
                        break;
                    }
                if (!containSameCard)
                    uniqueCards.Add(c);
            }
            return uniqueCards;
        }
    }
}
