using System;
using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json;

namespace Fsm_Blackjack.Model
{
    public class Game
    {
        public class Board
        {
            public int s { get; set; }
            public List<Card> pCards { get; set; }
            public List<Card> dCards { get; set; }
        }

        public List<Card> GameCards { get; set; }

        private int state;
        public List<Card> pCards { get; set; }
        public List<Card> dCards { get; set; }

        public Game()
        {
            state = 0;
            GameCards = new List<Card> {     new Card(
      "A",
      "spades"
    ),
    new Card(
      "A",
      "diamonds"
    ),
    new Card(
      "A",
      "clubs"
    ),
    new Card(
      "A",
      "hearts"
    ),
    new Card(
      "2",
      "spades"
    ),
    new Card(
      "2",
      "diamonds"
    ),
    new Card(
      "2",
      "clubs"
    ),
    new Card(
      "2",
      "hearts"
    ),
    new Card(
      "3",
      "spades"
    ),
    new Card(
      "3",
      "diamonds"
    ),
    new Card(
      "3",
      "clubs"
    ),
    new Card(
      "3",
      "hearts"
    ),
    new Card(
      "4",
      "spades"
    ),
    new Card(
      "4",
      "diamonds"
    ),
    new Card(
      "4",
      "clubs"
    ),
    new Card(
      "4",
      "hearts"
    ),
    new Card(
      "5",
      "spades"
    ),
    new Card(
      "5",
      "diamonds"
    ),
    new Card(
      "5",
      "clubs"
    ),
    new Card(
      "5",
      "hearts"
    ),
    new Card(
      "6",
      "spades"
    ),
    new Card(
      "6",
      "diamonds"
    ),
    new Card(
      "6",
      "clubs"
    ),
    new Card(
      "6",
      "hearts"
    ),
    new Card(
      "7",
      "spades"
    ),
    new Card(
      "7",
      "diamonds"
    ),
    new Card(
      "7",
      "clubs"
    ),
    new Card(
      "7",
      "hearts"
    ),
    new Card(
      "8",
      "spades"
    ),
    new Card(
      "8",
      "diamonds"
    ),
    new Card(
      "8",
      "clubs"
    ),
    new Card(
      "8",
      "hearts"
    ),
    new Card(
      "9",
      "spades"
    ),
    new Card(
      "9",
      "diamonds"
    ),
    new Card(
      "9",
      "clubs"
    ),
    new Card(
      "9",
      "hearts"
    ),
    new Card(
      "10",
      "spades"
    ),
    new Card(
      "10",
      "diamonds"
    ),
    new Card(
      "10",
      "clubs"
    ),
    new Card(
      "10",
      "hearts"
    ),
    new Card(
      "J",
      "spades"
    ),
    new Card(
      "J",
      "diamonds"
    ),
    new Card(
      "J",
      "clubs"
    ),
    new Card(
      "J",
      "hearts"
    ),
    new Card(
      "Q",
      "spades"
    ),
    new Card(
      "Q",
      "diamonds"
    ),
    new Card(
      "Q",
      "clubs"
    ),
    new Card(
      "Q",
      "hearts"
    ),
    new Card(
      "K",
      "spades"
    ),
    new Card(
      "K",
      "diamonds"
    ),
    new Card(
      "K",
      "clubs"
    ),
    new Card(
      "K",
      "hearts"
  ) };
            pCards = new List<Card>();
            dCards = new List<Card>();
        }

        public void reset()
        {
            state = 1;
            pCards.Clear();
            dCards.Clear();
        }

        public void playerhit()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, GameCards.Count);
            pCards.Add(new Card(GameCards[index].value, GameCards[index].suit));
            GameCards.RemoveAt(index);
        }

        public void dealerhit()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, GameCards.Count);
            dCards.Add(new Card(GameCards[index].value, GameCards[index].suit));
            GameCards.RemoveAt(index);
        }

        public int calculate(List<Card> cards)
        {
            int total = 0;
            foreach (Card card in cards)
            {
                if (!card.value.Equals("A"))
                {
                    switch (card.value)
                    {
                        case "K":
                            total += 10;
                            break;
                        case "Q":
                            total += 10;
                            break;
                        case "J":
                            total += 10;
                            break;
                        default:
                            total += int.Parse(card.value);
                            break;
                    }
                }
            };

            List<Card> aces = new List<Card>();
            foreach (Card card in cards)
            {
                if (card.value.Equals("A"))
                {
                    aces.Add(card);
                };
            }

            foreach (Card card in aces)
            {
                if ((total + 11) > 21)
                {
                    total += 1;
                }
                else if ((total + 11) == 21)
                {
                    if (aces.Count > 1)
                    {
                        total += 1;
                    }
                    else
                    {
                        total += 11;
                    }
                }
                else
                {
                    total += 11;
                }
            }

            return total;
        }

        public bool playergetBlackjack()
        {
            if (calculate(pCards) == 21) { return true; };
            return false;
        }

        public bool dealergetBlackjack()
        {
            if (calculate(dCards) == 21) { return true; };
            return false;
        }

        public bool playergetBust()
        {
            if (calculate(pCards) > 21) { return true; };
            return false;
        }

        public bool dealergetBust()
        {
            if (calculate(dCards) > 21) { return true; };
            return false;
        }

        public bool start()
        {
            if (state != 0) { return false; }
            state = 1;
            reset();

            playerhit(); playerhit();
            dealerhit(); dealerhit();

            if (playergetBlackjack()) { state = 2; }
            return true;
        }

        public bool hit()
        {
            if (state != 1) { return false; }
            playerhit();
            if (playergetBlackjack()) { state = 2; }
            else if (playergetBust()) { state = 3; }
            return true;
        }

        public bool stand()
        {
            if (state != 1) { return false; }
            while ((calculate(dCards) < calculate(pCards))&&(calculate(dCards)<16))
            {
                dealerhit();
            }

            if (dealergetBlackjack() || (calculate(dCards) > calculate(pCards) && calculate(dCards)<21)) { state = 3; }
            else if (calculate(dCards) == calculate(pCards)) { state = 4; }
            else if (dealergetBust() || calculate(dCards) < calculate(pCards)) { state = 2; }
            return true;
        }

        public bool rematch()
        {
            state = 0;
            GameCards = new List<Card> {     new Card(
      "A",
      "spades"
    ),
    new Card(
      "A",
      "diamonds"
    ),
    new Card(
      "A",
      "clubs"
    ),
    new Card(
      "A",
      "hearts"
    ),
    new Card(
      "2",
      "spades"
    ),
    new Card(
      "2",
      "diamonds"
    ),
    new Card(
      "2",
      "clubs"
    ),
    new Card(
      "2",
      "hearts"
    ),
    new Card(
      "3",
      "spades"
    ),
    new Card(
      "3",
      "diamonds"
    ),
    new Card(
      "3",
      "clubs"
    ),
    new Card(
      "3",
      "hearts"
    ),
    new Card(
      "4",
      "spades"
    ),
    new Card(
      "4",
      "diamonds"
    ),
    new Card(
      "4",
      "clubs"
    ),
    new Card(
      "4",
      "hearts"
    ),
    new Card(
      "5",
      "spades"
    ),
    new Card(
      "5",
      "diamonds"
    ),
    new Card(
      "5",
      "clubs"
    ),
    new Card(
      "5",
      "hearts"
    ),
    new Card(
      "6",
      "spades"
    ),
    new Card(
      "6",
      "diamonds"
    ),
    new Card(
      "6",
      "clubs"
    ),
    new Card(
      "6",
      "hearts"
    ),
    new Card(
      "7",
      "spades"
    ),
    new Card(
      "7",
      "diamonds"
    ),
    new Card(
      "7",
      "clubs"
    ),
    new Card(
      "7",
      "hearts"
    ),
    new Card(
      "8",
      "spades"
    ),
    new Card(
      "8",
      "diamonds"
    ),
    new Card(
      "8",
      "clubs"
    ),
    new Card(
      "8",
      "hearts"
    ),
    new Card(
      "9",
      "spades"
    ),
    new Card(
      "9",
      "diamonds"
    ),
    new Card(
      "9",
      "clubs"
    ),
    new Card(
      "9",
      "hearts"
    ),
    new Card(
      "10",
      "spades"
    ),
    new Card(
      "10",
      "diamonds"
    ),
    new Card(
      "10",
      "clubs"
    ),
    new Card(
      "10",
      "hearts"
    ),
    new Card(
      "J",
      "spades"
    ),
    new Card(
      "J",
      "diamonds"
    ),
    new Card(
      "J",
      "clubs"
    ),
    new Card(
      "J",
      "hearts"
    ),
    new Card(
      "Q",
      "spades"
    ),
    new Card(
      "Q",
      "diamonds"
    ),
    new Card(
      "Q",
      "clubs"
    ),
    new Card(
      "Q",
      "hearts"
    ),
    new Card(
      "K",
      "spades"
    ),
    new Card(
      "K",
      "diamonds"
    ),
    new Card(
      "K",
      "clubs"
    ),
    new Card(
      "K",
      "hearts"
  ) };
            pCards = new List<Card>();
            dCards = new List<Card>();
            return true;
        }

        public string getBoard()
        {
            var b = new Board()
            {
                s = state,
                dCards = dCards,
                pCards = pCards
            };

            string s = JsonConvert.SerializeObject(b);
            return s;
        }
    }
}
