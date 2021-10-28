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
        public static List<Player> Players { get; private set; }
        private static List<Dice> dices;
        public static int CurrentPlayerId{get; private set;}

        public Game(int nbPlayer, int nbDice, int nbCardInPiles, int nbStartCoin){
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
                pileBoulangerie.PutBack(new Boulangerie());
                pileFerme.PutBack(new Ferme());
                pileCafe.PutBack(new Cafe());
                pileSuperette.PutBack(new Superette());
                pileForet.PutBack(new Foret());
                pileRestaurant.PutBack(new Restaurant());
                pileStade.PutBack(new Stade());
            }
            piles.Add(CardName.ChampDeBle, pileChampDeBle);
            piles.Add(CardName.Boulangerie, pileBoulangerie);
            piles.Add(CardName.Ferme, pileFerme);
            piles.Add(CardName.Cafe, pileCafe);
            piles.Add(CardName.Superette, pileSuperette);
            piles.Add(CardName.Foret, pileForet);
            piles.Add(CardName.Restaurant, pileRestaurant);
            piles.Add(CardName.Stade, pileStade);

            Players = new List<Player>();
            for (int i = 0; i < nbPlayer; i++)
            {
                Player p = new Player($"Joueur {i + 1}", nbStartCoin, new List<Card>());
                p.AddToHand(pileChampDeBle.Draw());
                p.AddToHand(pileBoulangerie.Draw());
                Players.Add(p);
            }
            
            Start();
        }

        private void Start(){
            CurrentPlayerId = 0;
            while (Players.Count > 1)
            {
                Console.Clear();
                DisplayPlayers();

                PlayerRound();
            }
        }

        private void PlayerRound(){
            int diceValue = 0;
            Player p = Players[CurrentPlayerId];
            Console.WriteLine($"### {p.Name} ###");

            // Lance les dés
            Console.WriteLine("Entrée - lancer le{0} dé{0}", dices.Count > 1 ? "s" : "");
            Console.ReadLine();
            foreach (Dice d in dices)
            {
                d.Throw();
                diceValue += d.Value;
                //d.Render(Console.CursorLeft, Console.CursorTop);
            }
            Console.WriteLine("Valeur d{0} dé{1} : {2}", dices.Count > 1 ? "es" : "u", dices.Count > 1 ? "s" : "", diceValue);
            // Action des adversaires
            foreach (Player opponent in Players)
                if(opponent != p)
                {
                    foreach (Card c in opponent.Hand)
                        if ((c.Color == Card.CardColor.Rouge || c.Color == Card.CardColor.Bleu) && c._activationNumbers.Contains<int>(diceValue))
                            c.ApplyEffect();
                }
            // Action du joueur
            foreach (Card c in p.Hand)
                if ((c.Color == Card.CardColor.Vert || c.Color == Card.CardColor.Bleu) && c._activationNumbers.Contains<int>(diceValue))
                    c.ApplyEffect();
            // Check la mort du player
            if (!p.IsAlive)
                Players.Remove(p);
            // Buy phase
            int choiceNb = 1;
            foreach(CardName cardName in piles.Keys){
                Console.WriteLine($"{choiceNb} - Acheter {cardName} ({piles[cardName].Cards[0].Price}$)");
                ++choiceNb;
            }
            Console.WriteLine("Entrée - Rien acheter");
            bool bought;
            do
            {
                string response = Console.ReadLine();
                bought = false;
                switch (response)
                {
                    case "1":
                        bought = p.Buy(piles[CardName.ChampDeBle]);
                        break;
                    case "2":
                        bought = p.Buy(piles[CardName.Boulangerie]);
                        break;
                    case "3":
                        bought = p.Buy(piles[CardName.Ferme]);
                        break;
                    case "4":
                        bought = p.Buy(piles[CardName.Cafe]);
                        break;
                    case "5":
                        bought = p.Buy(piles[CardName.Superette]);
                        break;
                    case "6":
                        bought = p.Buy(piles[CardName.Foret]);
                        break;
                    case "7":
                        bought = p.Buy(piles[CardName.Restaurant]);
                        break;
                    case "8":
                        bought = p.Buy(piles[CardName.Stade]);
                        break;
                    case "":
                        bought = true;
                        break;
                    default:
                        Console.WriteLine("Veuillez choisir un input valide");
                        break;
                }
            } while (!bought);
            // End turn
            ++CurrentPlayerId;
            if (CurrentPlayerId > Players.Count - 1)
                CurrentPlayerId = 0;
            Console.WriteLine("Entrée - joueur suivant");
            Console.ReadLine();
        }

        private void DisplayPlayers()
        {
            foreach(Player player in Players){
                Console.WriteLine($"{player.Name} {player.NbPiece}$ : ");
                foreach(Card card in player.Hand){
                    card.Display();
                }
                Console.WriteLine("\n--------------------------------\n");
            }
            //DisplayCompliqué
            /*int nbCards = 0;
            foreach (Player p in Players)
                nbCards += p.Hand.Count;
            int x = 0, y = 0, margin = 1;
            string betweenPlayers = " | ";
            Console.SetWindowSize(nbCards * (Card.CardWidth + margin + betweenPlayers.Length), Console.WindowHeight);
            for (int i = 0; i < Players.Count; i++)
            {
                Player p = Players[i];
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
            }*/
            
        }
    }
}

enum CardName {
    ChampDeBle,
    Boulangerie,
    Ferme,
    Cafe,
    Superette,
    Foret,
    Restaurant,
    Stade
}
