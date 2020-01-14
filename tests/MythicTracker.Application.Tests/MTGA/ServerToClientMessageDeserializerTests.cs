using System.IO;
using System.Linq;
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
        public void ShouldSuccessfulyDeserializeOnConnectionResponse()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/ConnectionResponse.json");

            Assert.Equal("8352e8c4-2994-4fbc-b8ae-f879dae0e83f", result.TransactionId);
            Assert.Single(result.GreToClientEvent.GreToClientMessages);

            var message = result.GreToClientEvent.GreToClientMessages.Single();
            Assert.IsType<GreToClientConnectRespMessage>(message);

            var connectionResponseMessage = (GreToClientConnectRespMessage)message;
            Assert.Equal(GreToClientMessageType.GREMessageType_ConnectResp, connectionResponseMessage.Type);
            Assert.Equal(ConnectionStatus.ConnectionStatus_Success, connectionResponseMessage.ConnectionResponseMessage.Status);
            Assert.Equal(new[] { 70404, 70404, 70404, 70404, 70207, 70207, 70207, 70207, 70244, 70244, 70244, 70244, 70408, 70408, 70408, 70408, 70408, 70335, 70335, 70335, 70335, 70189, 70189, 70189, 70189, 70197, 70197, 70197, 68545, 68545, 70218, 70218, 70218, 70218, 68740, 68740, 68740, 68740, 70389, 70389, 70389, 70030, 70030, 70030, 70030, 70388, 70342, 70342, 70391, 70391, 70391, 70391, 67224, 67224, 67224, 67224, 70248, 70248, 69646, 69646 }, connectionResponseMessage.ConnectionResponseMessage.DeckMessage.DeckCards);
            Assert.Equal(new[] { 70205, 70205, 70205, 69646, 69895, 69895, 69895, 69895, 70380, 69827, 69827, 69550, 69550, 70012, 68545 }, connectionResponseMessage.ConnectionResponseMessage.DeckMessage.SideboardCards);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateFull()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/GameState_Full.json");

            Assert.Equal("109dd5c5-3e32-4cd7-8e6f-b66eed40ecda", result.TransactionId);
            Assert.Single(result.GreToClientEvent.GreToClientMessages);

            var message = result.GreToClientEvent.GreToClientMessages.Single();
            Assert.IsType<GreToClientGameStateMessage>(message);

            var gameStateMessage = (GreToClientGameStateMessage)message;
            Assert.Equal(GreToClientMessageType.GREMessageType_GameStateMessage, gameStateMessage.Type);
            Assert.Equal(GameStateMessageType.GameStateType_Full, gameStateMessage.GameStateMessage.Type);
            Assert.Equal(16, gameStateMessage.GameStateMessage.Zones.Length);

            Assert.Equal(13, gameStateMessage.GameStateMessage.Zones[0].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Revealed, gameStateMessage.GameStateMessage.Zones[0].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[0].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[0].ObjectInstanceIds);

            Assert.Equal(14, gameStateMessage.GameStateMessage.Zones[1].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Revealed, gameStateMessage.GameStateMessage.Zones[1].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[1].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[1].ObjectInstanceIds);

            Assert.Equal(18, gameStateMessage.GameStateMessage.Zones[2].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Pending, gameStateMessage.GameStateMessage.Zones[2].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[2].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[2].ObjectInstanceIds);

            Assert.Equal(26, gameStateMessage.GameStateMessage.Zones[3].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Command, gameStateMessage.GameStateMessage.Zones[3].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[3].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[3].ObjectInstanceIds);

            Assert.Equal(27, gameStateMessage.GameStateMessage.Zones[4].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Stack, gameStateMessage.GameStateMessage.Zones[4].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[4].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[4].ObjectInstanceIds);

            Assert.Equal(28, gameStateMessage.GameStateMessage.Zones[5].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Battlefield, gameStateMessage.GameStateMessage.Zones[5].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[5].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[5].ObjectInstanceIds);

            Assert.Equal(29, gameStateMessage.GameStateMessage.Zones[6].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Exile, gameStateMessage.GameStateMessage.Zones[6].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[6].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[6].ObjectInstanceIds);

            Assert.Equal(30, gameStateMessage.GameStateMessage.Zones[7].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Limbo, gameStateMessage.GameStateMessage.Zones[7].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[7].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[7].ObjectInstanceIds);

            Assert.Equal(31, gameStateMessage.GameStateMessage.Zones[8].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Hand, gameStateMessage.GameStateMessage.Zones[8].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[8].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[8].ObjectInstanceIds);

            Assert.Equal(32, gameStateMessage.GameStateMessage.Zones[9].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Library, gameStateMessage.GameStateMessage.Zones[9].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[9].OwnerSeatId);
            Assert.Equal(new[] { 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222 }, gameStateMessage.GameStateMessage.Zones[9].ObjectInstanceIds);

            Assert.Equal(33, gameStateMessage.GameStateMessage.Zones[10].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Graveyard, gameStateMessage.GameStateMessage.Zones[10].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[10].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[10].ObjectInstanceIds);

            Assert.Equal(34, gameStateMessage.GameStateMessage.Zones[11].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Sideboard, gameStateMessage.GameStateMessage.Zones[11].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[11].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[11].ObjectInstanceIds);

            Assert.Equal(35, gameStateMessage.GameStateMessage.Zones[12].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Hand, gameStateMessage.GameStateMessage.Zones[12].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[12].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[12].ObjectInstanceIds);

            Assert.Equal(36, gameStateMessage.GameStateMessage.Zones[13].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Library, gameStateMessage.GameStateMessage.Zones[13].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[13].OwnerSeatId);
            Assert.Equal(new[] { 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282 }, gameStateMessage.GameStateMessage.Zones[13].ObjectInstanceIds);

            Assert.Equal(37, gameStateMessage.GameStateMessage.Zones[14].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Graveyard, gameStateMessage.GameStateMessage.Zones[14].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[14].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[14].ObjectInstanceIds);

            Assert.Equal(38, gameStateMessage.GameStateMessage.Zones[15].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Sideboard, gameStateMessage.GameStateMessage.Zones[15].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[15].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[15].ObjectInstanceIds);

            Assert.Empty(gameStateMessage.GameStateMessage.GameObjects);
            Assert.Empty(gameStateMessage.GameStateMessage.Annotations);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateDiff()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/GameState_Diff.json");

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateDiffAnnotations()
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
