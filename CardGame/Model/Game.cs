using CardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Model
{
    public class Game
    {
        /// <summary>
        /// Number of players in the game
        /// </summary>
        public NumOfPlayers NumOfPlayers { get; set; }

        /// <summary>
        /// Is game using suits
        /// </summary>
        public bool UsingSuits { get; set; }

        /// <summary>
        /// The deck size the game is using
        /// </summary>
        public DeckSize DeckSize { get; set; }

        /// <summary>
        /// Game players
        /// </summary>
        public IList<Player> Players { get; set; }

        /// <summary>
        /// Table holding drawn cards
        /// </summary>
        public IList<Card> Table { get; set; }

        public Deck Deck { get; set; }

        /// <summary>
        /// Create a shuffeled deck for playing
        /// </summary>
        public void CreateDeck()
        {
            Deck.Init(DeckSize);
            Deck.Shuffle();
        }

        /// <summary>
        /// Create game players
        /// </summary>
        public void CreatePlayers()
        {
            for (int i = 0; i < (int)NumOfPlayers; i++)
            {
                var player = ModelFactory.GetPlayer();
                Console.WriteLine("Player {0}, enter name: ", i + 1);
                player.Name = Console.ReadLine();
                Players.Add(player);
            }
        }

        /// <summary>
        /// Deal card to the players
        /// </summary>
        public void DealCards()
        {
            while (Deck.Cards.Count > 0)
            {
                Console.WriteLine(Deck.Cards.Count);
                for (int j = 0; j < Players.Count; j++)
                {
                    var last = Deck.Cards.Last();
                    Players[j].DrawPile.Cards.Add(last);
                    Deck.Cards.Remove(last);
                }
            }
        }

        /// <summary>
        /// Logic for playing the game
        /// </summary>
        public void Play()
        {
            while (Players.Where(x => (x.DrawPile.Cards.Count + x.DiscardPile.Cards.Count) > 0).ToList().Count > 1)
            {


                Player turnWinner = null;
                Card maxCard = null;
                foreach (var player in Players)
                {
                    var card = player.DrawCard();

                    if (card == null)
                    {
                        Console.WriteLine("Player {0} lost the game!", player.Name);
                    }
                    else
                    {
                        Table.Add(card);
                        Console.WriteLine("{0} ({1} cards): {2}",
                                            player.Name,
                                            player.DiscardPile.Cards.Count + player.DrawPile.Cards.Count + Math.Ceiling((double)Table.Count / 2),
                                            (UsingSuits) ? card.Suit + " " + card.Value.ToString() : card.Value.ToString());
                        if (maxCard == null || maxCard.Value < card.Value)
                        {
                            maxCard = card;
                            turnWinner = player;
                        }
                    }
                }

                if (!IsDraw(maxCard))
                {
                    Table.ToList().ForEach(x => turnWinner.DiscardPile.Cards.Add(x));
                    Table.Clear();
                    Console.WriteLine("{0} wins this round", turnWinner.Name);
                }
                else
                {
                    Console.WriteLine("No winner this round");
                }

            }


            Console.WriteLine("Player {0} wins the game!", Players.Where(x=> (x.DrawPile.Cards.Count + x.DiscardPile.Cards.Count) > 0).ToList().First().Name);
        }

        /// <summary>
        /// Check if the turn is draw
        /// </summary>
        /// <param name="winCard"></param>
        /// <returns></returns>
        private bool IsDraw(Card winCard)
        {
            var result = false;
            if (UsingSuits)
            {
                result = Table.Where(x => x.Value == winCard.Value && x.Suit == winCard.Suit).Count() > 1;
            }
            else
            {
                result = Table.Where(x => x.Value == winCard.Value).Count() > 1;
            }

            return result;
        }
    }
}
