using MiniVille.Classes;

public abstract class Card{

    public CardColor _color { get; private set; }
    public int[] _activationNumbers {get; private set; }
    public int _reward {get; private set; }
    public int _price {get; private set; }

    private Player owner;

    public Card(CardColor color, int[] activationNumbers, int reward,  int price){
        _color = color;
        _activationNumbers = activationNumbers;
        _reward = reward;
        _price = price;
    }

    public virtual void ApplyEffect(){}
}

public enum CardColor{
    Bleu,
    Vert,
    Rouge
}