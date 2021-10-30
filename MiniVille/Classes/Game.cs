using MiniVille.Classes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniVille.Classes
{
    public class Game
    {
        public static Dictionary<CardName, Pile> Piles { get; set; }
        private Player bank{ get; set; }
        public static List<Player> Players { get; private set; }
        private static List<Dice> dices;
        public static int CurrentPlayerId{get; private set;}

        public Game(int nbPlayer, int nbDice, int nbCardInPiles, int nbStartCoin){
            dices = new List<Dice>();
            for (int i = 0; i < nbDice; i++)
                dices.Add(new Dice());

            Piles = new Dictionary<CardName, Pile>();
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
            Piles.Add(CardName.ChampDeBle, pileChampDeBle);
            Piles.Add(CardName.Boulangerie, pileBoulangerie);
            Piles.Add(CardName.Ferme, pileFerme);
            Piles.Add(CardName.Cafe, pileCafe);
            Piles.Add(CardName.Superette, pileSuperette);
            Piles.Add(CardName.Foret, pileForet);
            Piles.Add(CardName.Restaurant, pileRestaurant);
            Piles.Add(CardName.Stade, pileStade);

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
            int moneyEarned;
            if(CurrentPlayerId > 0)
                x = CurrentPlayerId * Players[CurrentPlayerId - 1].GetUniqueCards().Count * (Card.CardWidth + margin) - CurrentPlayerId*(margin - 3);
            int diceValue = 0;
            Player p = Players[CurrentPlayerId];
            Display($"### {p.Name} ###", x, ref y);
            // Lance les dés
            Display($"Entrée - lancer les dés", x, ref y);
            Console.SetCursorPosition(x, y);
            Console.ReadLine();
            ClearUnder(y-1, 1);
            foreach (Dice d in dices)
            {
                d.Throw();
                diceValue += d.Value;
                //d.Render(Console.CursorLeft, Console.CursorTop);
            }
            Display($"Valeur des dés : {diceValue}", x, ref y);
            // Action du joueur
            moneyEarned = p.NbPiece;
            foreach (Card c in p.Hand)
                if ((c.Color == Card.CardColor.Vert || c.Color == Card.CardColor.Bleu) && c._activationNumbers.Contains<int>(diceValue))
                    c.ApplyEffect();
            moneyEarned = p.NbPiece - moneyEarned;
            if (moneyEarned > 0)
            {
                Display($"{p.Name}", x, ref y, false);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Display($"+{moneyEarned}$", x + p.Name.Length + 1, ref y);
                Console.ForegroundColor = ConsoleColor.White;
            }
            // Action des adversaires
            foreach (Player opponent in Players)
                if(opponent != p)
                {
                    moneyEarned = opponent.NbPiece;
                    foreach (Card c in opponent.Hand)
                        if ((c.Color == Card.CardColor.Rouge || c.Color == Card.CardColor.Bleu) && c._activationNumbers.Contains<int>(diceValue))
                            c.ApplyEffect();
                    moneyEarned = opponent.NbPiece - moneyEarned;
                    if (moneyEarned > 0)
                    {
                        Display($"{opponent.Name}", x, ref y, false);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Display($"+{moneyEarned}$", x+opponent.Name.Length+1, ref y);
                        Console.ForegroundColor = ConsoleColor.White;
                    }     
                }
            // Reaffichage des sous-sous
            DisplayPlayersInfo();
            // Check la mort du player
            if (!p.IsAlive)
                Players.Remove(p);
            // Buy phase
            Console.ReadLine();
            y++;
            int i = 0;
            foreach (Pile pile in Piles.Values) {
                if (pile.Cards.Count > 0)
                {
                    pile.Cards[0].Render(x+i, y);
                    i += Card.CardWidth + margin;
                }
            }
            y += Card.CardHeight/2;
            Display("Rien - Rien acheter", x+i, ref y);
            y += Card.CardHeight / 2 + 1;
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
                        bought = p.Buy(Piles[CardName.ChampDeBle]);
                        break;
                    case "2":
                        bought = p.Buy(Piles[CardName.Boulangerie]);
                        break;
                    case "3":
                        bought = p.Buy(Piles[CardName.Ferme]);
                        break;
                    case "4":
                        bought = p.Buy(Piles[CardName.Cafe]);
                        break;
                    case "5":
                        bought = p.Buy(Piles[CardName.Superette]);
                        break;
                    case "6":
                        bought = p.Buy(Piles[CardName.Foret]);
                        break;
                    case "7":
                        bought = p.Buy(Piles[CardName.Restaurant]);
                        break;
                    case "8":
                        bought = p.Buy(Piles[CardName.Stade]);
                        break;
                    case "":
                        passed = true;
                        break;
                    default:
                        Display("Veuillez choisir un input valide", x, ref y);
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

            Display("Entrée - Fin du tour", x, ref y);
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

        private void Display(string text, int x, ref int y, bool upY = true)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            if(upY)
                y++;
        }
        private void ClearUnder(int top, int size = 0)
        {
            int x = Console.CursorLeft, y = Console.CursorTop;
            int windX = Console.WindowLeft, windY = Console.WindowTop;
            if (size == 0)
                size = Console.WindowHeight - 1;
            Console.SetCursorPosition(0, top);
            Console.Write(String.Concat(Enumerable.Repeat(" ", Console.BufferWidth * size)));
            Console.SetCursorPosition(x, y);
            Console.SetWindowPosition(windX, windY);
        }
    }
}

public enum CardName {
    ChampDeBle,
    Boulangerie,
    Ferme,
    Cafe,
    Superette,
    Foret,
    Restaurant,
    Stade
}
