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
        private static IA IAJoueur;

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
                
                if (nbPlayer == 1){
                    IAJoueur = new IA(nbStartCoin, new List<Card>());
                    IAJoueur.AddToHand(pileChampDeBle.Draw());
                    IAJoueur.AddToHand(pileBoulangerie.Draw());
                    Players.Add(IAJoueur);
                } 
            }
            
            Start();
        }

        private void Start(){
            CurrentPlayerId = 0;
            DisplayPlayersInfo();
            DisplayPlayersCards();
            int loop = 0;
            while (Players.Count > 1)
            {
                ClearUnder(0, Card.CardHeight+4);
                if (!Players[loop].IsAnIa)
                    PlayerRound();
                else
                    IARound();
                loop = (loop + 1) % Players.Count;
            }
            Console.WriteLine($"{Players[0].Name} a gagné la partie");
        }

        private void IARound(){
            int x = 0, y = Card.CardHeight+4, margin = 1;
            int moneyEarned, playerMoneyEarner;
            for(int j = 0; j < CurrentPlayerId; j++)
                x += Players[j].GetUniqueCards().Count * (Card.CardWidth + margin) - margin + 3; // 3 = betweenPlayers.Length
            List<int> diceValues = new List<int>();
            
            Display("*** IA ***", x, ref y);
            Console.ReadLine();
            ClearUnder(x, y, 1);
            y++;

            for(int j = 0; j < dices.Count; j++)
            {
                Dice d = dices[j];
                d.Throw();
                diceValues.Add(d.Value);
                d.Render(x+j*Dice.DiceWidth+1, y);
            }
            y += Dice.DiceHeight+1;

            System.Threading.Thread.Sleep(1000);

            // Action du joueur
            playerMoneyEarner = IAJoueur.NbPiece;
            foreach (Card c in IAJoueur.Hand)
                foreach (int diceValue in diceValues)
                    if ((c.Color == Card.CardColor.Vert || c.Color == Card.CardColor.Bleu) && c._activationNumbers.Contains<int>(diceValue))
                        c.ApplyEffect();
            // Action des adversaires et affichage gain
            foreach (Player opponent in Players)
                if(opponent != IAJoueur)
                {
                    moneyEarned = opponent.NbPiece;
                    foreach (Card c in opponent.Hand)
                        foreach (int diceValue in diceValues)
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
            // affichage gain joueur
            playerMoneyEarner = IAJoueur.NbPiece - playerMoneyEarner;
            if (playerMoneyEarner != 0)
            {
                string displayMoneyEarned = (playerMoneyEarner > 0 ? "+" : "-") + MathF.Abs(playerMoneyEarner);
                Display($"{IAJoueur.Name}", x, ref y, false);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Display($"{displayMoneyEarned}$", x + IAJoueur.Name.Length + 1, ref y);
                Console.ForegroundColor = ConsoleColor.White;
            }
            // Reaffichage des sous-sous
            DisplayPlayersInfo();
            // Check la mort du player
            if (!IAJoueur.IsAlive)
                Players.Remove(IAJoueur);

            IAJoueur.BuyOrEconomy(Piles);
            ++CurrentPlayerId;
            
                DisplayPlayersInfo();
                DisplayPlayersCards();
            }

        private void PlayerRound(){
            int x = 0, y = Card.CardHeight+4, margin = 1;
            int moneyEarned, playerMoneyEarner;
            for(int j = 0; j < CurrentPlayerId; j++)
                x += Players[j].GetUniqueCards().Count * (Card.CardWidth + margin) - margin + 3; // 3 = betweenPlayers.Length
            List<int> diceValues = new List<int>();
            Player p = Players[CurrentPlayerId];
            CenterView(x);
            Display($"*** {p.Name} ***", x, ref y);
            // Lance les dés
            Console.ReadLine();
            ClearUnder(x, y, 1);
            y++;
            for(int j = 0; j < dices.Count; j++)
            {
                Dice d = dices[j];
                d.Throw();
                diceValues.Add(d.Value);
                d.Render(x+j*Dice.DiceWidth+1, y);
            }
            y += Dice.DiceHeight+1;
            // Action du joueur
            playerMoneyEarner = p.NbPiece;
            foreach (Card c in p.Hand)
                foreach (int diceValue in diceValues)
                    if ((c.Color == Card.CardColor.Vert || c.Color == Card.CardColor.Bleu) && c._activationNumbers.Contains<int>(diceValue))
                        c.ApplyEffect();
            // Action des adversaires et affichage gain
            foreach (Player opponent in Players)
                if(opponent != p)
                {
                    moneyEarned = opponent.NbPiece;
                    foreach (Card c in opponent.Hand)
                        foreach (int diceValue in diceValues)
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
            // affichage gain joueur
            playerMoneyEarner = p.NbPiece - playerMoneyEarner;
            if (playerMoneyEarner != 0)
            {
                string displayMoneyEarned = (playerMoneyEarner > 0 ? "+" : "-") + MathF.Abs(playerMoneyEarner);
                Display($"{p.Name}", x, ref y, false);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Display($"{displayMoneyEarned}$", x + p.Name.Length + 1, ref y);
                Console.ForegroundColor = ConsoleColor.White;
            }
            // Reaffichage des sous-sous
            DisplayPlayersInfo();
            // Buy phase
            CenterView(x);
            Console.ReadLine();
            y++;
            int i = 0;
            List<Card> choices = new List<Card>();
            foreach (Pile pile in Piles.Values) {
                if (pile.Cards.Count > 0)
                {
                    choices.Add(pile.Cards[0]);
                    pile.Cards[0].Render(x+i, y);
                    i += Card.CardWidth + margin;
                }
            }
            y += Card.CardHeight/2;
            Display("Rien acheter", x+i, ref y);
            y += Card.CardHeight / 2 + 1;

            CenterView(x);
            bool bought = false;
            CardName choice = CardChoice(x, y, choices);
            y -= Card.CardHeight + 2;
            ClearUnder(x, y);
            CenterView(x);
            if (choice != CardName.Void)
                bought = p.Buy(Piles[choice]);
            if (bought)
            {
                DisplayPlayersInfo();
                DisplayPlayersCards();
            }
            // End turn
            ++CurrentPlayerId;
            if (CurrentPlayerId > Players.Count - 1)
                CurrentPlayerId = 0;
            if (!p.IsAlive)
                Players.Remove(p);
            //on tue tous les autre joueur car p est le gagnant
            foreach (Player player in Players)
                if (player.NbPiece >= 20)
                {
                    Players = new List<Player>();
                    Players.Add(player);
                }

            Display("Fin du tour", x, ref y);
            Console.ReadLine();
        }

        private CardName CardChoice(int x, int y, List<Card> choices)
        {
            string arrow = "<"+String.Concat(Enumerable.Repeat("-", Card.CardWidth-2))+">";
            string space;
            int choice = 0;
            ConsoleKey key;
            bool canBuy;
            do
            {
                space = String.Concat(Enumerable.Repeat(" ", choice*(Card.CardWidth+1)));
                ClearUnder(x, y, 1);
                Display(space+arrow, x, ref y, false);
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.LeftArrow && choice > 0)
                    choice--;
                else if (key == ConsoleKey.RightArrow && choice < choices.Count)
                    choice++;
                if (choice >= choices.Count || Players[CurrentPlayerId].NbPiece >= choices[choice].Price)
                    canBuy = true;
                else
                    canBuy = false;
            }
            while (key != ConsoleKey.Enter || !canBuy);
            if (choice >= choices.Count)
                return CardName.Void;
            return choices[choice].CardName;
        }

        private void DisplayPlayersInfo()
        {
            ClearUnder(0, 0, 2);
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
            //Console.SetCursorPosition(0, 0);
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
            }  
        }

        private void Display(string text, int x, ref int y, bool upY = true)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.SetCursorPosition(x, y);
            if (upY)
                y++;
            CenterView(x);
        }
        private void ClearUnder(int left, int top, int size = 0)
        {
            int windX = Console.WindowLeft;
            if (size == 0)
                size = Console.WindowHeight - 1;
            Console.SetCursorPosition(left, top);
            Console.Write(String.Concat(Enumerable.Repeat(" ", Console.BufferWidth * size)));
            Console.SetWindowPosition(windX, 0);
        }

        private void CenterView(int baseX)
        {
            int limit = baseX+(Piles.Count+1)*(Card.CardWidth+1);
            if (limit >= Console.WindowWidth)
                Console.SetWindowPosition(limit-Console.WindowWidth, 0);
            else
                Console.SetWindowPosition(0, 0);
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
    Stade,
    Void
}
