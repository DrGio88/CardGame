using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    public class Game
    {
        private List<Card> _staleMatePile = new List<Card>();
        private const int _defaultDeckSize = 10;

        public Game()
        {
            DeckSize = _defaultDeckSize;

            Players = new List<Player>
            {
                new Player("Player 1"),
                new Player("Player 2")
            };
        }

        public Game(int deckSize, List<Player> players)
        {
            if (deckSize < 5) throw new ArgumentOutOfRangeException($"{nameof(deckSize)} can't be less than 5");
            if (players.Count < 1) throw new ArgumentOutOfRangeException($"Number of {nameof(players)} can't be less than 2");

            var playerCounts = players.GroupBy(x => x.Name)
                      .Select(g => new { g.Key, Count = g.Count() });

            if (playerCounts.Any(p => p.Count > 1)) throw new ArgumentException("There can't be multiple players with the same name");

            DeckSize = deckSize;
            Players = players;
        }

        public int DeckSize { get; set; }
        public List<Player> Players { get; set; }

        public void CreateGame()
        {
            var deck = new List<Card>().CreateDeck(DeckSize, Players.Count);

            foreach (var player in Players)
            {
                player.DrawPile = deck.Take(DeckSize * 2).ToList();
                deck.RemoveRange(0, player.DrawPile.Count);
            }
        }

        public void PlayGame()
        {
            if (this.Players.Count != 1)
            {
                var currentPile = DrawCard();

                foreach (var current in currentPile)
                {
                    var player = this.Players.Where(p => p.Name == current.Key).Single();
                    Console.WriteLine($"{player.Name} ({player.DrawPile.Count + player.DiscardPile.Count}) {current.Value}");
                }

                var winnerOfTheRound = ResolveWinnerOfTheRound(currentPile);

                Console.WriteLine(winnerOfTheRound);

                var losers = this.Players.Where(p => p.DiscardPile.Count == 0 && p.DrawPile.Count == 0).ToList();
                this.Players = this.Players.Except(losers).ToList();

                PlayGame();
            }
            else
            {
                Console.WriteLine($"{Players[0].Name} wins the game");
                return;
            }
        }

        public Dictionary<string, Card> DrawCard()
        {
            foreach (var player in this.Players)
                if (player.DrawPile.Count == 0)
                    ShuffleDiscardPile(player);

            return this.Players.Select(t => new
            {
                PlayerName = t.Name,
                Card = t.DrawPile.Last()
            }).ToDictionary(t => t.PlayerName, t => t.Card);
        }

        public static void ShuffleDiscardPile(Player player)
        {
            player.DrawPile = player.DiscardPile.Shuffle();
            player.DiscardPile = new List<Card>();
        }      

        public string ResolveWinnerOfTheRound(Dictionary<string, Card> currentPile)
        {
            var maxValue = currentPile.Max(cp => cp.Value.CardValue);
            var playersWithMaxValue = currentPile.Where(cp => cp.Value.CardValue == maxValue).Select(cp => cp.Key).ToList();

            if (playersWithMaxValue.Count == 1)
            {
                foreach (var player in this.Players)
                {
                    player.DrawPile.RemoveAt(player.DrawPile.Count - 1);
                    if (playersWithMaxValue[0] == player.Name)
                    {
                        player.DiscardPile.AddRange(currentPile.Select(cp => cp.Value).ToList());
                        if (_staleMatePile.Count > 0)
                        {
                            player.DiscardPile.AddRange(_staleMatePile);
                            _staleMatePile.Clear();
                        }
                    }
                }
                return $"{playersWithMaxValue[0]} wins this round";
            }
            else
            {
                ResolveStalemate(currentPile);

                return "No winners this round";
            }

        }

        public void ResolveStalemate(Dictionary<string, Card> currentPile)
        {
            _staleMatePile.AddRange(currentPile.Select(cp => cp.Value).ToList());
            foreach (var playerName in currentPile.Keys)
            {
                var player = this.Players.Where(p => p.Name == playerName).Single();
                player.DrawPile.RemoveAt(player.DrawPile.Count - 1);
            }
        }
    }
}
