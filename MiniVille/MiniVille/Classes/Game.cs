using MiniVille.Classes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniVille.Classes
{
    public class Game
    {
        private Dictionary<CardName, Pile> piles;
        private Player bank{ get; set; }
        private List<Player> players;
        private static List<Dice> dices;
        public static Player CurrentPlayer { get; set; }

        public Game(int nbPlayer, int nbDice, int nbCardInPiles, int nbStartCoin)
        {
            dices = new List<Dice>();
            for (int i = 0; i < nbDice; i++)
                dices.Add(new Dice());

            piles = new Dictionary<CardName, Pile>();
            Pile pileChampDeBle = new Pile();
            Pile pileFerme = new Pile();
            Pile pileBoulangerie = new Pile();
            Pile pileCafe = new Pile();
            Pile pileSuperette = new Pile();
            Pile pileForet = new Pile();
            Pile pileRestaurant = new Pile();
            Pile pileStade = new Pile();
            for(int i = 0; i < nbCardInPiles; i++)
            {
                pileChampDeBle.PutBack(new ChampDeBle());
                pileFerme.PutBack(new Ferme());
                pileBoulangerie.PutBack(new Boulangerie());
                pileCafe.PutBack(new Cafe());
                pileSuperette.PutBack(new Superette());
                pileForet.PutBack(new Foret());
                pileRestaurant.PutBack(new Restaurant());
                pileStade.PutBack(new Stade());
            }
            piles.Add(CardName.ChampDeBle, pileChampDeBle);
            piles.Add(CardName.Ferme, pileFerme);
            piles.Add(CardName.Boulangerie, pileBoulangerie);
            piles.Add(CardName.Cafe, pileCafe);
            piles.Add(CardName.Superette, pileSuperette);
            piles.Add(CardName.Foret, pileForet);
            piles.Add(CardName.Restaurant, pileRestaurant);
            piles.Add(CardName.Stade, pileStade);

            players = new List<Player>();
            for (int i = 0; i < nbPlayer; i++)
            {
                List<Card> hand = new List<Card>();
                hand.Add(pileChampDeBle.Draw());
                hand.Add(pileBoulangerie.Draw());
                players.Add(new Player("Joueur "+(i+1), nbStartCoin, hand));
            }
            
            Start();
        }

        private void Start()
        {
            int diceValue = 0;
            int x, y;
            while (players.Count > 1)
            {
                Console.Clear();
                DisplayPlayers();
                x = Console.CursorLeft;
                y = Console.CursorTop;
                foreach (Player p in players)
                {
                    CurrentPlayer = p;
                    // Lance les dés
                    foreach (Dice d in dices)
                    {
                        d.Throw();
                        diceValue += d.Value;
                    }
                    // Action des adversaires
                    foreach(Player opponent in players)
                        if(opponent != p && opponent.HasRedCard())
                        {
                            Console.WriteLine("- {0} peut jouer une carte -", opponent.Name);
                            Console.ReadLine();
                            // Lire l'input
                        }
                    // Check la mort du player
                    if (!p.IsAlive)
                        players.Remove(p);
                }
                Console.ReadLine();
            }
        }

        private void DisplayPlayers()
        {
            int nbCards = 0;
            foreach (Player p in players)
                nbCards += p.Hand.Count;
            int x, y, margin = 1;
            string betweenPlayers = " | ";
            Console.SetWindowSize(nbCards * (Card.CardWidth + margin + betweenPlayers.Length), Console.WindowHeight);
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];
                x = i * (p.Hand.Count*Card.CardWidth + margin);
                y = 0;
                if (i > 0)
                {
                    x += betweenPlayers.Length * i;
                    for (int j = 0; j < Card.CardHeight+2; j++)
                    {
                        Console.SetCursorPosition(x-3, y+j);
                        Console.Write(betweenPlayers);
                    }
                }
                Console.SetCursorPosition(x, y);
                Console.Write("{0}", p.Name);
                y += 2; // 2 = nb de ligne avant le render des cartes
                for(int j = 0; j < p.Hand.Count; j++)
                {
                    Card c = p.Hand[j];
                    c.Render(x + j * (Card.CardWidth + margin), y);
                }
                Console.WriteLine();
            }
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
