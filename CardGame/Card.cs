using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public class Card
    {
        public int CardValue { get; set; }
        public Suits Suit { get; set; }

        public override string ToString()
        {
            return this.CardValue.ToString();
        }
    }
}
