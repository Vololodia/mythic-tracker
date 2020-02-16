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
        public void ShouldReturnNullOnEmptyString()
        {
            var result = DeserealizeFromFile(string.Empty);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldReturnNullOnEmptyJson()
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
            var result = DeserealizeFromFile("./MTGA/TestData/InvalidStructureLogRow.json");

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

            Assert.Equal("09b08892-9d39-4272-ad95-225b081a5739", result.TransactionId);
            Assert.Single(result.GreToClientEvent.GreToClientMessages);

            var message = result.GreToClientEvent.GreToClientMessages.Single();
            Assert.IsType<GreToClientGameStateMessage>(message);

            var gameStateMessage = (GreToClientGameStateMessage)message;
            Assert.Equal(GreToClientMessageType.GREMessageType_GameStateMessage, gameStateMessage.Type);
            Assert.Equal(GameStateMessageType.GameStateType_Diff, gameStateMessage.GameStateMessage.Type);
            Assert.Equal(4, gameStateMessage.GameStateMessage.Zones.Length);

            Assert.Equal(31, gameStateMessage.GameStateMessage.Zones[0].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Hand, gameStateMessage.GameStateMessage.Zones[0].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[0].OwnerSeatId);
            Assert.Equal(new[] { 169, 168, 167, 166, 165, 164, 163 }, gameStateMessage.GameStateMessage.Zones[0].ObjectInstanceIds);

            Assert.Equal(32, gameStateMessage.GameStateMessage.Zones[1].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Library, gameStateMessage.GameStateMessage.Zones[1].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[1].OwnerSeatId);
            Assert.Equal(new[] { 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222 }, gameStateMessage.GameStateMessage.Zones[1].ObjectInstanceIds);

            Assert.Equal(35, gameStateMessage.GameStateMessage.Zones[2].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Hand, gameStateMessage.GameStateMessage.Zones[2].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[2].OwnerSeatId);
            Assert.Equal(new[] { 229, 228, 227, 226, 225, 224, 223 }, gameStateMessage.GameStateMessage.Zones[2].ObjectInstanceIds);

            Assert.Equal(36, gameStateMessage.GameStateMessage.Zones[3].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Library, gameStateMessage.GameStateMessage.Zones[3].Type);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Zones[3].OwnerSeatId);
            Assert.Equal(new[] { 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282 }, gameStateMessage.GameStateMessage.Zones[3].ObjectInstanceIds);

            Assert.Equal(8, gameStateMessage.GameStateMessage.GameObjects.Length);

            Assert.Equal(50, gameStateMessage.GameStateMessage.GameObjects[0].InstanceId);
            Assert.Equal(70484, gameStateMessage.GameStateMessage.GameObjects[0].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Adventure, gameStateMessage.GameStateMessage.GameObjects[0].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[0].ZoneId);
            Assert.Equal(418485, gameStateMessage.GameStateMessage.GameObjects[0].NameId);
            Assert.Equal(165, gameStateMessage.GameStateMessage.GameObjects[0].ParentId);

            Assert.Equal(163, gameStateMessage.GameStateMessage.GameObjects[1].InstanceId);
            Assert.Equal(70404, gameStateMessage.GameStateMessage.GameObjects[1].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[1].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[1].ZoneId);
            Assert.Equal(652, gameStateMessage.GameStateMessage.GameObjects[1].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[1].ParentId);

            Assert.Equal(164, gameStateMessage.GameStateMessage.GameObjects[2].InstanceId);
            Assert.Equal(67224, gameStateMessage.GameStateMessage.GameObjects[2].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[2].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[2].ZoneId);
            Assert.Equal(16650, gameStateMessage.GameStateMessage.GameObjects[2].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[2].ParentId);

            Assert.Equal(165, gameStateMessage.GameStateMessage.GameObjects[3].InstanceId);
            Assert.Equal(70244, gameStateMessage.GameStateMessage.GameObjects[3].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[3].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[3].ZoneId);
            Assert.Equal(413910, gameStateMessage.GameStateMessage.GameObjects[3].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[3].ParentId);

            Assert.Equal(166, gameStateMessage.GameStateMessage.GameObjects[4].InstanceId);
            Assert.Equal(68545, gameStateMessage.GameStateMessage.GameObjects[4].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[4].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[4].ZoneId);
            Assert.Equal(272755, gameStateMessage.GameStateMessage.GameObjects[4].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[4].ParentId);

            Assert.Equal(167, gameStateMessage.GameStateMessage.GameObjects[5].InstanceId);
            Assert.Equal(69646, gameStateMessage.GameStateMessage.GameObjects[5].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[5].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[5].ZoneId);
            Assert.Equal(336805, gameStateMessage.GameStateMessage.GameObjects[5].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[5].ParentId);

            Assert.Equal(168, gameStateMessage.GameStateMessage.GameObjects[6].InstanceId);
            Assert.Equal(70030, gameStateMessage.GameStateMessage.GameObjects[6].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[6].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[6].ZoneId);
            Assert.Equal(46164, gameStateMessage.GameStateMessage.GameObjects[6].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[6].ParentId);

            Assert.Equal(169, gameStateMessage.GameStateMessage.GameObjects[7].InstanceId);
            Assert.Equal(70335, gameStateMessage.GameStateMessage.GameObjects[7].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[7].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[7].ZoneId);
            Assert.Equal(414227, gameStateMessage.GameStateMessage.GameObjects[7].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[7].ParentId);

            Assert.Single(gameStateMessage.GameStateMessage.Annotations);

            Assert.Equal(2018, gameStateMessage.GameStateMessage.Annotations[0].Id);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Annotations[0].AffectorId);
            Assert.Equal(new[] { 2 }, gameStateMessage.GameStateMessage.Annotations[0].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_NewTurnStarted }, gameStateMessage.GameStateMessage.Annotations[0].Types);
        }

        [Fact]
        public void ShouldSuccessfulyDeserializeOnGameStateDiffAnnotations()
        {
            var result = DeserealizeFromFile("./MTGA/TestData/GameState_Diff_Annotations.json");

            Assert.Equal("807862be-d859-4bfc-a29b-d6dbf9f6422a", result.TransactionId);
            Assert.Single(result.GreToClientEvent.GreToClientMessages);

            var message = result.GreToClientEvent.GreToClientMessages.Single();
            Assert.IsType<GreToClientGameStateMessage>(message);

            var gameStateMessage = (GreToClientGameStateMessage)message;
            Assert.Equal(GreToClientMessageType.GREMessageType_GameStateMessage, gameStateMessage.Type);
            Assert.Equal(GameStateMessageType.GameStateType_Diff, gameStateMessage.GameStateMessage.Type);

            Assert.Equal(5, gameStateMessage.GameStateMessage.Zones.Length);

            Assert.Equal(27, gameStateMessage.GameStateMessage.Zones[0].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Stack, gameStateMessage.GameStateMessage.Zones[0].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[0].OwnerSeatId);
            Assert.Empty(gameStateMessage.GameStateMessage.Zones[0].ObjectInstanceIds);

            Assert.Equal(30, gameStateMessage.GameStateMessage.Zones[1].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Limbo, gameStateMessage.GameStateMessage.Zones[1].Type);
            Assert.Null(gameStateMessage.GameStateMessage.Zones[1].OwnerSeatId);
            Assert.Equal(new[] { 421, 353, 419, 344, 351, 413, 232, 231, 229, 224, 230, 409, 350, 226, 223, 346, 347 }, gameStateMessage.GameStateMessage.Zones[1].ObjectInstanceIds);

            Assert.Equal(31, gameStateMessage.GameStateMessage.Zones[2].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Hand, gameStateMessage.GameStateMessage.Zones[2].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[2].OwnerSeatId);
            Assert.Equal(new[] { 423, 345, 348, 349, 343 }, gameStateMessage.GameStateMessage.Zones[2].ObjectInstanceIds);

            Assert.Equal(32, gameStateMessage.GameStateMessage.Zones[3].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Library, gameStateMessage.GameStateMessage.Zones[3].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[3].OwnerSeatId);
            Assert.Equal(new[] { 354, 355, 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 352 }, gameStateMessage.GameStateMessage.Zones[3].ObjectInstanceIds);

            Assert.Equal(33, gameStateMessage.GameStateMessage.Zones[4].ZoneId);
            Assert.Equal(ZoneType.ZoneType_Graveyard, gameStateMessage.GameStateMessage.Zones[4].Type);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Zones[4].OwnerSeatId);
            Assert.Equal(new[] { 424 }, gameStateMessage.GameStateMessage.Zones[4].ObjectInstanceIds);

            Assert.Equal(3, gameStateMessage.GameStateMessage.GameObjects.Length);

            Assert.Equal(421, gameStateMessage.GameStateMessage.GameObjects[0].InstanceId);
            Assert.Equal(67224, gameStateMessage.GameStateMessage.GameObjects[0].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[0].Type);
            Assert.Equal(30, gameStateMessage.GameStateMessage.GameObjects[0].ZoneId);
            Assert.Equal(16650, gameStateMessage.GameStateMessage.GameObjects[0].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[0].ParentId);

            Assert.Equal(423, gameStateMessage.GameStateMessage.GameObjects[1].InstanceId);
            Assert.Equal(70404, gameStateMessage.GameStateMessage.GameObjects[1].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[1].Type);
            Assert.Equal(31, gameStateMessage.GameStateMessage.GameObjects[1].ZoneId);
            Assert.Equal(652, gameStateMessage.GameStateMessage.GameObjects[1].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[1].ParentId);

            Assert.Equal(424, gameStateMessage.GameStateMessage.GameObjects[2].InstanceId);
            Assert.Equal(67224, gameStateMessage.GameStateMessage.GameObjects[2].GroupId);
            Assert.Equal(GameObjectType.GameObjectType_Card, gameStateMessage.GameStateMessage.GameObjects[2].Type);
            Assert.Equal(33, gameStateMessage.GameStateMessage.GameObjects[2].ZoneId);
            Assert.Equal(16650, gameStateMessage.GameStateMessage.GameObjects[2].NameId);
            Assert.Null(gameStateMessage.GameStateMessage.GameObjects[2].ParentId);

            Assert.Equal(10, gameStateMessage.GameStateMessage.Annotations.Length);

            Assert.Equal(2132, gameStateMessage.GameStateMessage.Annotations[0].Id);
            Assert.Equal(421, gameStateMessage.GameStateMessage.Annotations[0].AffectorId);
            Assert.Equal(new[] { 352 }, gameStateMessage.GameStateMessage.Annotations[0].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_Scry }, gameStateMessage.GameStateMessage.Annotations[0].Types);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Annotations[0].Details.Length);
            Assert.Equal(AnnotationDetailKeyType.TopIds, gameStateMessage.GameStateMessage.Annotations[0].Details[0].Key);
            Assert.Equal(AnnotationDetailType.None, gameStateMessage.GameStateMessage.Annotations[0].Details[0].Type);
            Assert.Equal(AnnotationDetailKeyType.BottomIds, gameStateMessage.GameStateMessage.Annotations[0].Details[1].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[0].Details[1].Type);
            Assert.Equal(new[] { 352 }, gameStateMessage.GameStateMessage.Annotations[0].Details[1].ValueInt32);

            Assert.Equal(2133, gameStateMessage.GameStateMessage.Annotations[1].Id);
            Assert.Equal(421, gameStateMessage.GameStateMessage.Annotations[1].AffectorId);
            Assert.Equal(new[] { 353 }, gameStateMessage.GameStateMessage.Annotations[1].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_ObjectIdChanged }, gameStateMessage.GameStateMessage.Annotations[1].Types);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Annotations[1].Details.Length);
            Assert.Equal(AnnotationDetailKeyType.OriginalId, gameStateMessage.GameStateMessage.Annotations[1].Details[0].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[1].Details[0].Type);
            Assert.Equal(new[] { 353 }, gameStateMessage.GameStateMessage.Annotations[1].Details[0].ValueInt32);
            Assert.Equal(AnnotationDetailKeyType.NewId, gameStateMessage.GameStateMessage.Annotations[1].Details[1].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[1].Details[1].Type);
            Assert.Equal(new[] { 423 }, gameStateMessage.GameStateMessage.Annotations[1].Details[1].ValueInt32);

            Assert.Equal(2134, gameStateMessage.GameStateMessage.Annotations[2].Id);
            Assert.Equal(421, gameStateMessage.GameStateMessage.Annotations[2].AffectorId);
            Assert.Equal(new[] { 423 }, gameStateMessage.GameStateMessage.Annotations[2].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_ZoneTransfer }, gameStateMessage.GameStateMessage.Annotations[2].Types);
            Assert.Equal(3, gameStateMessage.GameStateMessage.Annotations[2].Details.Length);
            Assert.Equal(AnnotationDetailKeyType.ZoneSource, gameStateMessage.GameStateMessage.Annotations[2].Details[0].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[2].Details[0].Type);
            Assert.Equal(new[] { 32 }, gameStateMessage.GameStateMessage.Annotations[2].Details[0].ValueInt32);
            Assert.Equal(AnnotationDetailKeyType.ZoneDestination, gameStateMessage.GameStateMessage.Annotations[2].Details[1].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[2].Details[1].Type);
            Assert.Equal(new[] { 31 }, gameStateMessage.GameStateMessage.Annotations[2].Details[1].ValueInt32);
            Assert.Equal(AnnotationDetailKeyType.Category, gameStateMessage.GameStateMessage.Annotations[2].Details[2].Key);
            Assert.Equal(AnnotationDetailType.String, gameStateMessage.GameStateMessage.Annotations[2].Details[2].Type);
            Assert.Equal(new[] { "Draw" }, gameStateMessage.GameStateMessage.Annotations[2].Details[2].ValueString);

            Assert.Equal(2135, gameStateMessage.GameStateMessage.Annotations[3].Id);
            Assert.Equal(421, gameStateMessage.GameStateMessage.Annotations[3].AffectorId);
            Assert.Equal(new[] { 421 }, gameStateMessage.GameStateMessage.Annotations[3].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_ResolutionComplete }, gameStateMessage.GameStateMessage.Annotations[3].Types);
            Assert.Single(gameStateMessage.GameStateMessage.Annotations[3].Details);
            Assert.Equal(AnnotationDetailKeyType.GroupId, gameStateMessage.GameStateMessage.Annotations[3].Details[0].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[3].Details[0].Type);
            Assert.Equal(new[] { 67224 }, gameStateMessage.GameStateMessage.Annotations[3].Details[0].ValueInt32);

            Assert.Equal(2136, gameStateMessage.GameStateMessage.Annotations[4].Id);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Annotations[4].AffectorId);
            Assert.Equal(new[] { 421 }, gameStateMessage.GameStateMessage.Annotations[4].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_ObjectIdChanged }, gameStateMessage.GameStateMessage.Annotations[4].Types);
            Assert.Equal(2, gameStateMessage.GameStateMessage.Annotations[4].Details.Length);
            Assert.Equal(AnnotationDetailKeyType.OriginalId, gameStateMessage.GameStateMessage.Annotations[4].Details[0].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[4].Details[0].Type);
            Assert.Equal(new[] { 421 }, gameStateMessage.GameStateMessage.Annotations[4].Details[0].ValueInt32);
            Assert.Equal(AnnotationDetailKeyType.NewId, gameStateMessage.GameStateMessage.Annotations[4].Details[1].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[4].Details[1].Type);
            Assert.Equal(new[] { 424 }, gameStateMessage.GameStateMessage.Annotations[4].Details[1].ValueInt32);

            Assert.Equal(2137, gameStateMessage.GameStateMessage.Annotations[5].Id);
            Assert.Equal(1, gameStateMessage.GameStateMessage.Annotations[5].AffectorId);
            Assert.Equal(new[] { 424 }, gameStateMessage.GameStateMessage.Annotations[5].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_ZoneTransfer }, gameStateMessage.GameStateMessage.Annotations[5].Types);
            Assert.Equal(3, gameStateMessage.GameStateMessage.Annotations[5].Details.Length);
            Assert.Equal(AnnotationDetailKeyType.ZoneSource, gameStateMessage.GameStateMessage.Annotations[5].Details[0].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[5].Details[0].Type);
            Assert.Equal(new[] { 27 }, gameStateMessage.GameStateMessage.Annotations[5].Details[0].ValueInt32);
            Assert.Equal(AnnotationDetailKeyType.ZoneDestination, gameStateMessage.GameStateMessage.Annotations[5].Details[1].Key);
            Assert.Equal(AnnotationDetailType.Int32, gameStateMessage.GameStateMessage.Annotations[5].Details[1].Type);
            Assert.Equal(new[] { 33 }, gameStateMessage.GameStateMessage.Annotations[5].Details[1].ValueInt32);
            Assert.Equal(AnnotationDetailKeyType.Category, gameStateMessage.GameStateMessage.Annotations[5].Details[2].Key);
            Assert.Equal(AnnotationDetailType.String, gameStateMessage.GameStateMessage.Annotations[5].Details[2].Type);
            Assert.Equal(new[] { "Resolve" }, gameStateMessage.GameStateMessage.Annotations[5].Details[2].ValueString);

            Assert.Equal(2003, gameStateMessage.GameStateMessage.Annotations[6].Id);
            Assert.Equal(27, gameStateMessage.GameStateMessage.Annotations[6].AffectorId);
            Assert.Equal(new[] { 421 }, gameStateMessage.GameStateMessage.Annotations[6].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_EnteredZoneThisTurn }, gameStateMessage.GameStateMessage.Annotations[6].Types);
            Assert.Empty(gameStateMessage.GameStateMessage.Annotations[6].Details);

            Assert.Equal(2004, gameStateMessage.GameStateMessage.Annotations[7].Id);
            Assert.Equal(28, gameStateMessage.GameStateMessage.Annotations[7].AffectorId);
            Assert.Equal(new[] { 420 }, gameStateMessage.GameStateMessage.Annotations[7].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_EnteredZoneThisTurn }, gameStateMessage.GameStateMessage.Annotations[7].Types);
            Assert.Empty(gameStateMessage.GameStateMessage.Annotations[7].Details);

            Assert.Equal(2009, gameStateMessage.GameStateMessage.Annotations[8].Id);
            Assert.Equal(31, gameStateMessage.GameStateMessage.Annotations[8].AffectorId);
            Assert.Equal(new[] { 423, 419 }, gameStateMessage.GameStateMessage.Annotations[8].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_EnteredZoneThisTurn }, gameStateMessage.GameStateMessage.Annotations[8].Types);
            Assert.Empty(gameStateMessage.GameStateMessage.Annotations[8].Details);

            Assert.Equal(2011, gameStateMessage.GameStateMessage.Annotations[9].Id);
            Assert.Equal(33, gameStateMessage.GameStateMessage.Annotations[9].AffectorId);
            Assert.Equal(new[] { 424 }, gameStateMessage.GameStateMessage.Annotations[9].AffectedIds);
            Assert.Equal(new[] { AnnotationType.AnnotationType_EnteredZoneThisTurn }, gameStateMessage.GameStateMessage.Annotations[9].Types);
            Assert.Empty(gameStateMessage.GameStateMessage.Annotations[9].Details);
        }

        private ServerToClientMessage DeserealizeFromFile(string filepath)
        {
            var fileContent = !string.IsNullOrEmpty(filepath) ? File.ReadAllText(filepath) : string.Empty;

            return LogDeserializer.Deserialize(fileContent);
        }
    }
}
