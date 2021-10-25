using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVille.Classes
{
    class Game
    {
        private Dictionary<CardName, Pile> piles;
        public Player bank{ get; set; }

        public Game(int nbPlayer, int nbDice, int nbCardInPiles, int nbStartCoin)
        {
            piles = new Dictionary<CardName, Pile>();
        }
    }
}
enum CardName {
    ChampDeBle,
    Ferme,
    Boulangerie,
    Cafe,
    Superette,
    Foret,
    Restaurant,
    Stade
}
