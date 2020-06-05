using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            DrawPile = new List<Card>();
            DiscardPile = new List<Card>();
        }

        public string Name { get; set; }
        public List<Card> DrawPile { get; set; }
        public List <Card> DiscardPile { get; set; }   
    }
}
