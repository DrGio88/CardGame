using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            game.CreateGame();

            game.PlayGame();

            Console.Read();
        }
    }
}
