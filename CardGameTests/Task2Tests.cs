using CardGame;
using System.Collections.Generic;
using Xunit;

namespace CardGameTests
{
    public class Task2Tests
    {
        private Game _game;

        public Task2Tests()
        {
            _game = new Game();
        }

        [Fact]
        public void DrawCardTest ()
        {
            var player = _game.Players[0];
            player.DiscardPile = new List<Card>().CreateDeck();

            var discardPileCount = player.DiscardPile.Count;

            Game.ShuffleDiscardPile(player);

            Assert.Equal(discardPileCount, player.DrawPile.Count);
        }
    }
}
