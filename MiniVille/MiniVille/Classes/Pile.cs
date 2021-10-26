using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    public class Pile
    {
        public List<Card> Cards { get; set; }
        public Pile()
        {
            Cards = new List<Card>();
        }

        public Card Draw()
        {
            if (Cards.Count > 0)
            {
                Card c = Cards[Cards.Count - 1];
                Cards.Remove(c);
                return c;
            }
            else
                return null;
        }

        public void PutBack(Card c)
        {
            Cards.Add(c);
        }
    }
}
