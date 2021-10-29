using MiniVille.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Card{

    public CardColor Color { get; protected set; }
    public int[] _activationNumbers {get; protected set; }
    public int Price {get; protected set; }
    public string Name { get; set; }
    public string Effet {get; set;}
    public static int CardWidth { get; set; }
    public static int CardHeight { get; set; }

    public Player Owner { get; set; }

    public Card(){
        CardWidth = 19;
        CardHeight = 15; // 6 mini
    }

    public virtual void ApplyEffect()
    {
        Console.WriteLine($"{Owner.Name}, {Effet}");
    }

    public void Render(int x, int y)
    {
        string topAndBotLine = "+" + String.Concat(Enumerable.Repeat("-", CardWidth - 2)) + "+";
        string activationNumbers = "{";
        List<string> effetLines = new List<string>();
        string line;
        int lineIndex = 0;
        effetLines.Add(Effet);
        line = effetLines[effetLines.Count - 1];
        while (line.Length > CardWidth - 2)
        {
            effetLines.Remove(line);
            effetLines.Add(line.Substring(0, CardWidth-2));
            effetLines.Add(line.Substring(CardWidth - 2, line.Length - CardWidth-2));
        }
        Console.SetCursorPosition(x, y);
        Console.Write(topAndBotLine);
        for(int i = 0; i < CardHeight-2; i++)
        {
            Console.SetCursorPosition(x, y+i+1);
            string space;
            if (i == 0)
            {
                space = String.Concat(Enumerable.Repeat(" ", (int)MathF.Floor((CardWidth - 2 - Name.Length) / 2)));
                Console.Write("|{1}{0}{2}|", Name, space, space + (Name.Length%2 == 0 ? " " : ""));
            }
            else if(i == 1)
            {
                for (int j = 0; j < _activationNumbers.Length; j++)
                    activationNumbers += _activationNumbers[j].ToString() + (j < _activationNumbers.Length - 1 ? ", " : "}");
                space = String.Concat(Enumerable.Repeat(" ", (int)MathF.Floor((CardWidth - 2 - activationNumbers.Length) / 2)));
                Console.Write("|{1}{0}{2}|", activationNumbers, space, space + (activationNumbers.Length % 2 == 0 ? " " : ""));
            }
            else if(i >= (CardHeight - 2 - 1) - (effetLines.Count - 1))
            {
                line = effetLines[lineIndex];
                space = String.Concat(Enumerable.Repeat(" ", (int)MathF.Floor((CardWidth - 2 - line.Length) / 2)));
                Console.Write("|{1}{0}{2}|", line, space, space + (line.Length % 2 == 0 ? " " : ""));
                lineIndex++;
            }
            else
                Console.Write("|" + String.Concat(Enumerable.Repeat(" ", CardWidth-2)) + "|");
        }
        Console.SetCursorPosition(x, y+CardHeight-1);
        Console.Write(topAndBotLine);
        /*
        Console.WriteLine("+-----------+");
        Console.WriteLine("|Boulangerie|");
        Console.WriteLine("|   {1, 2}  |");
        Console.WriteLine("|           |");
        Console.WriteLine("| nieu nieu |");
        Console.WriteLine("| nieu nieu |");
        Console.WriteLine("+-----------+");*/
    }

    public void Display(){
        string activationNumbers = "";
        foreach(int number in _activationNumbers){
            activationNumbers += number+", ";
        }
        Console.WriteLine($"{this.Name} : {this.Color}, {activationNumbers}{this.Effet}");
    }

    public enum CardColor{
        Bleu,
        Vert,
        Rouge
    }
}