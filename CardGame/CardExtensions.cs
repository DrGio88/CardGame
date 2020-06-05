using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public static class CardExtensions
    {
        private const int _sameCardsPerPlayer = 2;
        private const int _defaultSize = 10;
        private const int _defaultNumberOfPlayers = 2;

        public static List<Card> Shuffle(this List<Card> cards)
        {
            List<Card> transformedCards = cards;

            Random r = new Random();

            for (int n = transformedCards.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);

                Card temp = transformedCards[n];
                transformedCards[n] = transformedCards[k];
                transformedCards[k] = temp;
            }

            return transformedCards;
        }

        public static List<Card> CreateDeck(this List<Card> cards, int size = _defaultSize, int numberOfPlayers = _defaultNumberOfPlayers)
        {
            for (int i = 1; i <= size; i++)
            {
                for (int j = 0; j < _sameCardsPerPlayer * numberOfPlayers; j++)
                {
                    cards.Add(new Card() { CardValue = i });
                }
            }

            return cards.Shuffle();
        }
    }
}
