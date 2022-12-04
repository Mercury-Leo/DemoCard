namespace _Project.Core{
	public static class CoreConventions {

		#region SO path

		public const string CardPath = CardBase + CardItem;
		public const string EffectPath = CardBase + Effect;
		
		const string CardBase = "Card";
		const string Effect = "/Effect";
		const string CardItem = "/Item";

		#endregion

		public const int MaxCardValue = 13;
		public const int MinCardValue = -1;
		public const int DeckSize = 54;
		public const int SpecialCards = 6;
		
		public static int[] specialValues = { -1, -1, 0, 0, 13, 13 };

		#region CardEffects

		public enum CardEffect {
			NoEffect,
			SpySelf,
			SpyOther,
			BlindSwitch,
			SpySwitch,
		}

		#endregion
	}
}