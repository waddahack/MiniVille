﻿using System;
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

        public Player(string name, int nbPiece, List<Card> hand)
        {
            Name = name;
            IsAlive = true;
            this.NbPiece = nbPiece;
            this.Hand = hand;
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
                Console.WriteLine($"{Name} à acheté {card.Name}");
                NbPiece -= card.Price;
                AddToHand(card);
                return true;
            }
            else
            {
                Console.WriteLine("Vous ne pouvez pas acheter cela !");
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
    }
}
