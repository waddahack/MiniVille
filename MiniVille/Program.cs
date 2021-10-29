using MiniVille.Classes;
using System;

namespace MiniVille
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            Console.WriteLine("Bonjour ! et bienvenu dans");
            Console.WriteLine("###### MINI VILLE ######");
            Console.WriteLine("Adaptation du jeu de société du même nom par Marin Ruelen, Léo Slomczynski et Tom Caudrillier \n");
            Console.ReadLine();
            Console.Clear();
            //Choix du nombre de joueur
            Console.Write("Nombre de joueurs : ");
            int nbJoueurs;
            while (int.TryParse(Console.ReadLine(), out nbJoueurs) && nbJoueurs < 2)
            {
                Console.WriteLine("Veuillez saisir un nombre valide (minimum 2)");
                Console.ReadLine();
                Console.Clear();
                Console.Write("Nombre de joueurs : ");
            }
            Console.WriteLine($"Vous avez choisi {nbJoueurs} de joueurs");
            Console.ReadLine();
            Console.Clear();
            //Choix du nombre de dés
            Console.Write("Nombre de dé(s) : ");
            int nbDices = 0;
            while (int.TryParse(Console.ReadLine(), out nbDices) && nbDices < 1)
            {
                Console.WriteLine("Veuillez saisir un nombre valide (minimum 1)");
                Console.ReadLine();
                Console.Clear();
                Console.Write("Nombre de dé(s) : ");
            }
            Console.WriteLine("Vous avez choisi {0} {1}", nbDices, nbDices > 1 ? "dés" : "dé");
            Console.ReadLine();
            Console.Clear();
            //Choix du nombre de cartes dans une pile
            Console.Write("Nombre de cartes par pile : ");
            int nbCards = 0;
            while (int.TryParse(Console.ReadLine(), out nbCards) && nbCards < 10)
            {
                Console.WriteLine("Veuillez saisir un nombre valide (minimum 10)");
                Console.ReadLine();
                Console.Clear();
                Console.Write("Nombre de cartes par pile : ");
            }
            Console.WriteLine($"Vous avez choisi {nbCards} cartes pour les piles de départ");
            Console.ReadLine();
            Console.Clear();
            //Choix du nombre de piéce de départ par joueur
            Console.Write("Nombre de piéces de départ : ");
            int nbPieces = 0;
            while (int.TryParse(Console.ReadLine(), out nbPieces) && nbPieces < 3)
            {
                Console.WriteLine("Veuillez saisir un nombre valide (minimum 3)");
                Console.ReadLine();
                Console.Clear();
                Console.Write("Nombre de piéces de départ : ");
            }
            Console.WriteLine($"Vous avez choisi {nbPieces} piéces pour débuter la partie");
            Console.ReadLine();
            Console.Clear();
            new Game(nbJoueurs, nbDices, nbCards, nbPieces);
        }
    }
}
