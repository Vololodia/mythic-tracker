using System.IO;
using MythicTracker.Application.MTGA.Models;
using Newtonsoft.Json;
using Xunit;

namespace MythicTracker.Application.MTGA.Tests
{
    public class ServerToClientMessageDeserializerTests
    {
        [Fact]
        public void ShouldReturnNullOnNullString()
        {
            var result = DeserealizeFromFile(null);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldReturnNullOnEmptyString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/Empty.json");

            Assert.Null(result);
        }

        [Fact]
        public void ShouldReturnNullOnNonJsonString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/NonJson.json");

            Assert.Null(result);
        }

        [Fact]
        public void ShouldReturnNullOnInvalidLogRowString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/Invalid.json");

            Assert.Null(result);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnConnectionResponseString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/ConnectionResponse.json");

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateFullString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/GameState_Full.json");

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateDiffString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/GameState_Diff.json");

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateDiffAnnotationsString()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/GameState_Diff_Annotations.json");

            Assert.NotNull(result);
        }

        private ServerToClientMessage DeserealizeFromFile(string filepath)
        {
            var fileContent = !string.IsNullOrEmpty(filepath) ? File.ReadAllText(filepath) : null;

            return JsonConvert.DeserializeObject<ServerToClientMessage>(fileContent);
        }
    }
}
