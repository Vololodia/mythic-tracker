using MythicTracker.Application.GameDeck;
using System.Threading.Tasks;
using Xunit;
using System;
using System.Collections.Generic;
using System.Collections;


namespace MythicTracker.Application.Tests
{
    public class GameDeckTests
    {
        [Fact]
        public async Task ShouldGetCardsWithNullPosition()
        {
            int[] ids = new int[] { 1, 2, 3 };
            Deck deck = new Deck(ids);
            List<int?> positions = new List<int?>();
            Dictionary<int, CardProbability> dict = deck.GetDeck();
            foreach (var entry in dict)
            {
                foreach (var deckCard in entry.Value.CardInstances)
                {
                    positions.Add(deckCard.Position);
                }
            }

            Assert.Equal(new int?[] { null, null, null }, positions);
        }

        [Fact]
        public async Task ShouldGetGrouppedCardsWithNullPosition()
        {
            int[] ids = new int[] { 1, 1, 3, 3, 4 };
            Deck deck = new Deck(ids);
            List<int?> positions = new List<int?>();
            Dictionary<int, CardProbability> dict = deck.GetDeck();
            foreach (var entry in dict)
            {
                foreach (var deckCard in entry.Value.CardInstances)
                {
                    positions.Add(deckCard.Position);
                }
            }

            Assert.Equal(3, dict.Count);
        }


        [Fact]
        public async Task ShouldGetCardWithPosition()
        {
            int[] ids = new int[] { 1, 2, 3};
            Deck deck = new Deck(ids);
            deck.AddCardInDeck(1234, 1);

            List<int?> positions = new List<int?>();
            Dictionary<int, CardProbability> dict = deck.GetDeck();
            foreach (var entry in dict)
            {
                foreach (var deckCard in entry.Value.CardInstances)
                {
                    positions.Add(deckCard.Position);
                }
            }

            Assert.Equal(new int?[] { null, null, null, 1 }, positions);
        }

        [Fact]
        public async Task ShouldGetCopyCardsWithPosition()
        {
            int[] ids = new int[] { 1 };
            Deck deck = new Deck(ids);

            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(1234, 1);

            List<int?> positions = new List<int?>();
            Dictionary<int, CardProbability> dict = deck.GetDeck();
            foreach (var entry in dict)
            {
                foreach (var deckCard in entry.Value.CardInstances)
                {
                    positions.Add(deckCard.Position);
                }
            }

            Assert.Equal(new int?[] { null, 2, 1 }, positions);
        }

        [Fact]
        public async Task ShouldGetDifferentTypesCardsWithPosition()
        {
            int[] ids = new int[] { 1 };
            Deck deck = new Deck(ids);

            deck.AddCardInDeck(112, 1);
            deck.AddCardInDeck(1234, 1);

            List<int?> positions = new List<int?>();
            Dictionary<int, CardProbability> dict = deck.GetDeck();
            foreach (var entry in dict)
            {
                foreach (var deckCard in entry.Value.CardInstances)
                {
                    positions.Add(deckCard.Position);
                }
            }

            Assert.Equal(new int?[] { null, 2, 1 }, positions);
        }

        [Fact]
        public async Task ShouldGetTypesCardsWithPosition()
        {
            int[] ids = new int[] { 1 };
            Deck deck = new Deck(ids);

            deck.AddCardInDeck(112, 1);
            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(112, 1);

            var dict = deck.GetDeck();

            Assert.Equal(4, dict[112].CardInstances[0].Position);
            Assert.Equal(1, dict[112].CardInstances[1].Position);
            Assert.Equal(3, dict[1234].CardInstances[0].Position);
            Assert.Equal(2, dict[1234].CardInstances[1].Position);
        }

        [Fact]
        public async Task ShouldRemoveCardIdOnly()
        {
            int[] ids = new int[] { 1 };
            Deck deck = new Deck(ids);

            deck.AddCardInDeck(112, 1);
            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(112, 1);

            deck.RemoveCardInDeck(112);

            var dict = deck.GetDeck();

            Assert.Equal(1, dict[1234].CardInstances[1].Position);
            Assert.Equal(2, dict[1234].CardInstances[0].Position);
        }

        [Fact]
        public async Task ShouldRemoveCardIdAndPosition()
        {
            int[] ids = new int[] { 1 };
            Deck deck = new Deck(ids);

            deck.AddCardInDeck(112, 1);
            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(1234, 1);
            deck.AddCardInDeck(112, 1);

            deck.RemoveCardInDeck(112, 4);

            var dict = deck.GetDeck();

            Assert.Equal(2, dict[1234].CardInstances[1].Position);
            Assert.Equal(3, dict[1234].CardInstances[0].Position);
            Assert.Equal(1, dict[112].CardInstances[0].Position);
        }
    }
}
