using MythicTracker.Application.GameDatabase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MythicTracker.Application.Tests
{
    public class GameCardsInfoDatabaseProviderTests
    {
        private GameCardsInfoDatabaseProvider CreateCardsProvider()
        {
            var filepath = @"C:\Users\Artem\Desktop\database.json";
            var provider = new GameCardsInfoDatabaseProvider(filepath);
            return provider;
        }

        [Fact]
        public async Task ShouldGetOneCard()
        {
            var cardsProvider = CreateCardsProvider();
            Card cardTest = cardsProvider.GetCard(6873);
            int idSample = 6873;
            string nameSample = "Crash of Rhinos";

            Assert.Equal(idSample, cardTest.Id);
            Assert.Equal(nameSample, cardTest.Name);
        }

        //[Fact]
        //public async Task ShouldGetOneAbilityCard()
        //{
        //    var cardsProvider = CreateCardsProvider();
        //    Card cardTest = cardsProvider.GetCard(1);
        //    int idSample = 1;
        //    string descriptionSample = "Deathtouch";

        //    Assert.Equal(idSample, cardTest.Id);
        //    Assert.Equal(descriptionSample, cardTest.Description);
        //}

        [Fact]
        public async Task ShouldGetAllCards()
        {
            var cardsProvider = CreateCardsProvider();
            Card[] allCards = cardsProvider.GetAllCards();

            Assert.NotEqual(250, allCards.Length);
        }
    }
}
