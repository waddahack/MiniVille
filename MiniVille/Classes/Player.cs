using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    class Player
    {
        private bool _isAlive;
        private int nbPiece;
        private Card[] hand;

        public Player(int nbPiece, Card[] hand)
        {
            _isAlive = true;
            this.nbPiece = nbPiece;
            this.hand = hand;
        }
        
        public void Buy(Pile pile){
            Card card = pile.Draw();
            if (card._price <= nbPiece)
                nbPiece -= card._price;
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
                _isAlive = false;
        }
    }
}
