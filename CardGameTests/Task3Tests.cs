using CardGame;
using System.Collections.Generic;
using Xunit;

namespace CardGameTests
{
    public class Task3Tests
    {
        private Game _game;

        public Task3Tests()
        {
            _game = new Game();
        }

        [Fact]
        public void CardComparisonTest()
        {
            var player1 = _game.Players[0];
            player1.DrawPile = new List<Card>
            {
                new Card()
                {
                    CardValue = 1
                }
            };

            var player2 = _game.Players[1];
            player2.DrawPile = new List<Card>
            {
                new Card()
                {
                    CardValue = 5
                }
            };

            var currentPile = _game.DrawCard();
             _= _game.ResolveWinnerOfTheRound(currentPile);

            Assert.Equal(2, player2.DiscardPile.Count);
        }

        [Fact]
        public void StalemateTest()
        {
            var player1 = _game.Players[0];
            player1.DrawPile = new List<Card>
            {
                new Card()
                {
                    CardValue = 1
                },
                 new Card()
                {
                    CardValue = 3
                }
            };

            var player2 = _game.Players[1];
            player2.DrawPile = new List<Card>
            {
                new Card()
                {
                    CardValue = 5
                },
                 new Card()
                {
                    CardValue = 3
                }
            };

            var currentPile = _game.DrawCard();
            _ = _game.ResolveWinnerOfTheRound(currentPile);

            currentPile = _game.DrawCard();
            _ = _game.ResolveWinnerOfTheRound(currentPile);

            Assert.Equal(4, player2.DiscardPile.Count);
        }
    }
}
