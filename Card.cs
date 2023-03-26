using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;


namespace Fsm_Blackjack.Model
{
    public class Card
    {
        public string value { get; set; }
        public string suit { get; set; }


        public Card(string val,string sui)
        {
            value = val;
            suit = sui;
        }

        public Card(Card c)
        {
            value = c.value;
            suit = c.suit;
        }
    }
}
