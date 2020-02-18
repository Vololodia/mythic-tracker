using MythicTracker.Application.GameDeck;
using System.Threading.Tasks;
using Xunit;


namespace MythicTracker.Application.Tests
{
    public class GameDeckTests
    {
        [Fact]
        public async Task ShouldGetDeckWithDictionary()
        {
            int[] ids = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Deck deck = new Deck(ids);
            Assert.Equal(new[] { "1" }, );
        }
    }
}
