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
            DisplayPlayersInfo();
            DisplayPlayersCards();
            while (Players.Count > 1)
            {
                ClearUnder(Card.CardHeight+4);
                PlayerRound();
            }
            Console.WriteLine($"{Players[0].Name} a gagné la partie");
        }

        private void PlayerRound(){
            int x = 0, y = Card.CardHeight+4, margin = 1;
            if(CurrentPlayerId > 0)
                x = CurrentPlayerId * Players[CurrentPlayerId - 1].GetUniqueCards().Count * (Card.CardWidth + margin) - CurrentPlayerId*(margin - 3);
            int diceValue = 0;
            Player p = Players[CurrentPlayerId];
            Console.SetCursorPosition(x, y);
            Console.Write($"### {p.Name} ###");
            y++;
            // Lance les dés
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Entrée - lancer le{0} dé{0}", dices.Count > 1 ? "s" : "");
            y++;
            Console.SetCursorPosition(x, y);
            Console.ReadLine();
            foreach (Dice d in dices)
            {
                d.Throw();
                diceValue += d.Value;
                //d.Render(Console.CursorLeft, Console.CursorTop);
            }
            Console.SetCursorPosition(x, y);
            Console.Write("Valeur d{0} dé{1} : {2}", dices.Count > 1 ? "es" : "u", dices.Count > 1 ? "s" : "", diceValue);
            y++;
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
            // Reaffichage des sous-sous
            DisplayPlayersInfo();
            // Check la mort du player
            if (!p.IsAlive)
                Players.Remove(p);
            // Buy phase
            int choiceNb = 1;
            foreach(CardName cardName in piles.Keys){
                Console.SetCursorPosition(x, y);
                Console.Write($"{choiceNb} - Acheter {cardName} ({piles[cardName].Cards[0].Price}$)");
                y++;
                ++choiceNb;
            }
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Rien - Rien acheter");
            y++;
            bool bought, passed;
            do
            {
                Console.SetCursorPosition(x, y);
                string response = Console.ReadLine();
                bought = false;
                passed = false;
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
                        passed = true;
                        break;
                    default:
                        Console.SetCursorPosition(x, y);
                        Console.Write("Veuillez choisir un input valide");
                        y++;
                        break;
                }
            } while (!bought && !passed);
            if (bought)
            {
                DisplayPlayersInfo();
                DisplayPlayersCards();
            }
            // End turn
            ++CurrentPlayerId;
            if (CurrentPlayerId > Players.Count - 1)
                CurrentPlayerId = 0;
            if (!p.IsAlive) Players.Remove(p);
            //on tue tous les autre joueur car p est le gagnant
            /*if (p.NbPiece >= 20)
            {
                foreach (Player player in Players)
                {
                    Players.Remove(player);
                }
                Players.Add(p);
            }*/

            Console.SetCursorPosition(x, y);
            Console.WriteLine("Entrée - Fin du tour");
            y++;
            Console.SetCursorPosition(x, y);
            Console.ReadLine();
        }

        private void DisplayPlayersInfo()
        {
            ClearUnder(0, 2);
            int x = 0, margin = 1;
            string betweenPlayers = " | ";
            for (int i = 0 ; i < Players.Count ; i++)
            {
                Player p = Players[i];
                if(i > 0)
                {
                    x += Players[i - 1].GetUniqueCards().Count * (Card.CardWidth + margin) - margin; 
                    for (int j = 0; j < Card.CardHeight + 3; j++)
                    {
                        Console.SetCursorPosition(x, j);
                        Console.Write(betweenPlayers);
                    }
                    x += betweenPlayers.Length;
                }  
                Console.SetCursorPosition(x, 0);
                Console.Write("{0}", p.Name);
                Console.SetCursorPosition(x + p.Name.Length + 2, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("{0}$", p.NbPiece);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void DisplayPlayersCards()
        {
            int x = 0, y = 2, margin = 1;
            for (int i = 0; i < Players.Count; i++)
            {
                Player p = Players[i];
                if (i > 0)
                    x += Players[i - 1].GetUniqueCards().Count * (Card.CardWidth + margin) - margin + 3;// 3 = betweenPlayers.Length dans displayPlayersCards
                List<Card> uniqueCards = p.GetUniqueCards();
                for (int j = 0; j < uniqueCards.Count; j++)
                {
                    Card c = uniqueCards[j];
                    c.Render(x + j * (Card.CardWidth + margin), y);
                }
                Console.WriteLine();
            }  
        }
        private void ClearUnder(int top, int size = 0)
        {
            int y = Console.CursorTop;
            if (size == 0)
                size = Console.WindowHeight - 1;
            Console.SetCursorPosition(0, top);
            Console.Write(String.Concat(Enumerable.Repeat(" ", Console.WindowWidth * (size - top))));
            Console.SetCursorPosition(0, y);
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
