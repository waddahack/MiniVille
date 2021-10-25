using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    class Pile
    {
        public List<Card> Cards { get; set; }
        public Pile(Card cardType, int nbCards)
        {
            Cards = new List<Card>();
            for (int i = 0; i < nbCards; i++)
                Cards.Add(new Card(cardType._color, cardType._name, cardType._activationNumbers, cardType._reward, cardType._price));
        }

        public Card Draw()
        {
            Card c = Cards[Cards.Count - 1];
            Cards.Remove(c);
            return c;
        }

        public void PutBack(Card c)
        {
            Cards.Add(c);
        }
    }
}
