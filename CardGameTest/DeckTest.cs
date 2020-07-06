using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CardGame;
using CardGame.Model;
using Xunit.Abstractions;

namespace CardGameTest
{
    
    public class DeckTest
    {

        /// <summary>
        /// Test that deck has the size of 40 cards
        /// </summary>
        [Fact]
        public void TestDeckSize()
        {
            // Arange
            var targetSize = (int)DeckSize.Small;
            var deck = ModelFactory.GetDeck();

            // Act
            deck.Init(DeckSize.Small);

            // Assert
            Assert.Equal(targetSize, deck.Cards.Count);
        }

        /// <summary>
        /// Test that the deck is shuffeled
        /// </summary>
        [Fact]
        public void TestDeckShuffle()
        {
            // Arange
            var usingSuits = false;
            var size = DeckSize.Small;
            var deck1 = ModelFactory.GetDeck();
            deck1.Init(size);
            var deck2 = ModelFactory.GetDeck();
            deck2.Init(size);

            // Act
            deck2.Shuffle();

            var hits = 0;
            for (int i = 0; i < deck2.Cards.Count - 1; i++)
            {
                if (!usingSuits && deck2.Cards[i].Value == deck1.Cards[i].Value)
                {
                    hits++;
                }

                if (usingSuits && (deck2.Cards[i].Value == deck1.Cards[i].Value && deck2.Cards[i].Suit == deck1.Cards[i].Suit))
                {
                    hits++;
                }
            }

            // Assert
            Assert.True((int)DeckSize.Small - 2  > hits);
        }

        /// <summary>
        /// Test that the discard pile is transfered to draw deck 
        /// when draw deck is empty
        /// </summary>
        [Fact]
        public void TestSuffleDiscardDeckToEmptyDrawDeck()
        {
            // Arange
            var player = ModelFactory.GetPlayer();
            var deck = ModelFactory.GetDeck();
            deck.Init(DeckSize.Small);
            player.DiscardPile = deck;

            // Act
            player.DrawPile.Cards.Clear();
            player.DrawCard();

            // Assert
            Assert.Equal((int)DeckSize.Small - 1, player.DrawPile.Cards.Count);
            Assert.Equal(0, player.DiscardPile.Cards.Count);
        }

        [Fact]
        public void TestHigherCardWins()
        {
            // Arange
            var game = ModelFactory.GetGame(NumOfPlayers.Two, false, DeckSize.Small);
            game.Players.Add(ModelFactory.GetPlayer());
            game.Players.Add(ModelFactory.GetPlayer());

            game.Players[0].DrawPile.Cards = new List<Card>
            {
                new Card
                {
                    Suit = Suit.Clubs,
                    Value = 1
                }
            };

            game.Players[1].DrawPile.Cards = new List<Card>
            {
                new Card
                {
                    Suit = Suit.Clubs,
                    Value = 2
                }
            };

            // Act
            game.Play();
        }

        [Fact]
        public void TestHandleDraw()
        {
            // Arange
            var game = ModelFactory.GetGame(NumOfPlayers.Two, false, DeckSize.Small);
            game.Players.Add(ModelFactory.GetPlayer());
            game.Players.Add(ModelFactory.GetPlayer());

            game.Players[0].DrawPile.Cards = new List<Card>
            {
                new Card
                {
                    Suit = Suit.Clubs,
                    Value = 1
                },
                new Card
                {
                    Suit = Suit.Clubs,
                    Value = 3
                }
            };

            game.Players[1].DrawPile.Cards = new List<Card>
            {
                new Card
                {
                    Suit = Suit.Clubs,
                    Value = 2
                },
                new Card
                {
                    Suit = Suit.Clubs,
                    Value = 3
                }
            };

            // Act
            game.Play();
        }
    }
}
