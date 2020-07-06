using CardGame.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public static class ModelFactory
    {
        /// <summary>
        /// Facotry for the <see cref="Card"/> instance
        /// </summary>
        /// <param name="suit"><see cref="Suit"/></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Card GetCard(Suit suit, int value) => new Card
        {
            Suit = suit,
            Value = value
        };

        /// <summary>
        /// Facotry for the <see cref="Deck"/> instance
        /// </summary>
        /// <returns></returns>
        public static Deck GetDeck() => new Deck
        {
            Cards = new List<Card>()
        };

        /// <summary>
        /// Facotry for the <see cref="Player"/> instance
        /// </summary>
        /// <returns></returns>
        public static Player GetPlayer() => new Player
        {
            DrawPile = GetDeck(),
            DiscardPile = GetDeck()
        };

        /// <summary>
        /// Facotry for the <see cref="Game"/> instance
        /// </summary>
        /// <param name="numOfPlayers"><see cref="NumOfPlayers"/></param>
        /// <param name="usingSuits"></param>
        /// <param name="deckSize"><see cref="DeckSize"/></param>
        /// <returns></returns>
        public static Game GetGame(NumOfPlayers numOfPlayers, bool usingSuits, DeckSize deckSize) => new Game
        {
            NumOfPlayers = numOfPlayers,
            UsingSuits = usingSuits,
            DeckSize = deckSize,
            Deck = GetDeck(),
            Players = new List<Player>(),
            Table = new List<Card>()
        };
    }
}
