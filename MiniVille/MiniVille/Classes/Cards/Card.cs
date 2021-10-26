using MiniVille.Classes;
using System;
using System.Linq;

public abstract class Card{

    public CardColor Color { get; private set; }
    public int[] _activationNumbers {get; private set; }
    public int Price {get; private set; }
    public string Name { get; set; }
    public static int CardWidth { get; set; }
    public static int CardHeight { get; set; }

    protected Player owner;

    public Card(CardColor color, int[] activationNumbers,  int price){
        Name = "Carte à jouer";
        CardWidth = 19;
        CardHeight = 15;
        Color = color;
        _activationNumbers = activationNumbers;
        Price = price;
    }

    public virtual void ApplyEffect(){}

    public void Render(int x, int y)
    {
        string topAndBotLine = "+" + String.Concat(Enumerable.Repeat("-", CardWidth - 2)) + "+";
        Console.SetCursorPosition(x, y);
        Console.Write(topAndBotLine);
        for(int i = 0; i < CardHeight-2; i++)
        {
            Console.SetCursorPosition(x, y+i+1);
            if (i == MathF.Floor((CardHeight-2)/2))
                Console.Write("|{1}{0}{2}|", Name, String.Concat(Enumerable.Repeat(" ", (int)MathF.Floor(CardWidth-2-Name.Length)/2)), String.Concat(Enumerable.Repeat(" ", (int)MathF.Ceiling(CardWidth-2-Name.Length)/2)));
            else
                Console.Write("|" + String.Concat(Enumerable.Repeat(" ", CardWidth-2)) + "|");
        }
        Console.SetCursorPosition(x, y+CardHeight-1);
        Console.Write(topAndBotLine);
        /*
        Console.WriteLine("+-----------+");
        Console.WriteLine("|           |");
        Console.WriteLine("|           |");
        Console.WriteLine("|Boulangerie|");
        Console.WriteLine("|           |");
        Console.WriteLine("|           |");
        Console.WriteLine("+-----------+");*/
    }

    public enum CardColor{
        Bleu,
        Vert,
        Rouge
    }
}