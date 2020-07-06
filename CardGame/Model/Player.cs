using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Model
{
    public class Player
    {
        /// <summary>
        /// Players name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The draw pile to play
        /// </summary>
        public Deck DrawPile { get; set; }

        /// <summary>
        /// Discard pile for taken cards
        /// </summary>
        public Deck DiscardPile { get; set; }

        /// <summary>
        /// Draw the card from the piles
        /// </summary>
        /// <returns>CardGame.Model.Card</returns>
        public Card DrawCard()
        {
            Card card = null;

            if (DrawPile.Cards.Count == 0)
            {
                if (DiscardPile.Cards.Count > 0)
                {
                    DiscardPile.Shuffle();
                    DiscardPile.Cards.ToList().ForEach(x => DrawPile.Cards.Add(x));
                    DiscardPile.Cards.Clear();
                }
            }

            if (DrawPile.Cards.Count > 0)
            {
                card = DrawPile.Cards.Last();
                DrawPile.Cards.Remove(card);
            }

            return card;
        }
    }

    /// <summary>
    /// Available number of players in the game
    /// </summary>
    public enum NumOfPlayers
    {
        Two = 2,
        Four = 4
    }
}
