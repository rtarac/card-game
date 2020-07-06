using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Model
{
    public class Card
    {
        /// <summary>
        /// The value of the card
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The <see cref="Suit"/> of the card
        /// </summary>
        public Suit Suit { get; set; }
    }
}
