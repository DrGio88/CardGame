using CardGame;
using System.Collections.Generic;
using Xunit;

namespace CardGameTests
{
    public class Task1Tests
    {
        [Fact]
        public void CreateDeckTest()
        {
            var deck = new List<Card>();

            deck.CreateDeck();

            Assert.Equal(40, deck.Count);
        }

        [Fact]
        public void ShuffleDeckTest()
        {
            var deck = new List<Card>().CreateDeck();

            var shuffledDeck = new List<Card>();
            shuffledDeck.AddRange(deck);
            shuffledDeck.Shuffle();

            Assert.NotEqual(shuffledDeck, deck);
        }
    }
}
