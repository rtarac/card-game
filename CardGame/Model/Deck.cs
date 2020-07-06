using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Model
{
    public class Deck
    {
        /// <summary>
        /// Deck cards
        /// </summary>
        public IList<Card> Cards { get; set; }

        /// <summary>
        /// Fisher-Yates shuffle implemantation
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();

            int n = Cards.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int k = i + (int)(rand.NextDouble() * (n - i));
                Card tmp = Cards[k];
                Cards[k] = Cards[n- 1];
                Cards[n - 1] = tmp;
            }
        }

        /// <summary>
        /// Populate Deck with cards
        /// </summary>
        /// <param name="deckSize"></param>
        public void Init(DeckSize deckSize)
        {
            for (int i = 1; i <= ((int)deckSize / 4); i++)
            {
                int value = (i > 10) ? i + 1 : i;
                foreach (var item in Enum.GetNames(typeof(Suit)))
                {
                    var card = ModelFactory.GetCard((Suit)Enum.Parse(typeof(Suit), item), value);
                    Cards.Add(card);
                }
            }
        }

    }

    /// <summary>
    /// Availale suits for the game
    /// </summary>
    public enum Suit
    {
        Clubs,
        Spades,
        Hearts,
        Diamonds
    }

    /// <summary>
    /// Available deck sizes for the game
    /// </summary>
    public enum DeckSize
    {
        Small = 40,
        Full = 52
    }
}
