namespace MiniVille.Classes.Cards
{
	public class Piscine : Card
	{
		public Piscine() : base()
		{
			Name = "Piscine";
			CardName = CardName.Piscine;
			Color = CardColor.Rouge;
			_activationNumbers = new int[] { 2 , 4};
			Price = 6;
			Effet = "Recevez 3 pièces du joueur qui a lancé le dé";
		}

		public override void ApplyEffect()
		{
			for(int i = 0; i < 3; i++)
				if (Game.Players[Game.CurrentPlayerId].NbPiece >= 1)
					this.Owner.GainCoins(1);
			Game.Players[Game.CurrentPlayerId].LoseCoins(3);
		}
	}
}