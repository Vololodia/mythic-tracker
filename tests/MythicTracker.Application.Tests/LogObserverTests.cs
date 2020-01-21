using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MythicTracker.Application.GameStateObserver;
using Xunit;

namespace MythicTracker.Application.Tests
{
    public class LogObserverTests
    {
        private const int WriteDelayInMilliseconds = 500;

        [Fact]
        public async Task ShouldFireEventOnNewLogLine()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var watcher = new LogObserver(filepath);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        watcher.Start();
                        await writer.WriteLineAsync("1");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1" }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldFireEventOnNewLogLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var watcher = new LogObserver(filepath);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        watcher.Start();
                        await writer.WriteLineAsync("1\r\n2\r\n3");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1", "2", "3" }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldFireEventsWithGroupedLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var raisedEvents = new List<GameStateChangedEventArgs>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => raisedEvents.Add(@event);

                watcher.Start();
                await writer.WriteLineAsync("0\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14");
                await Task.Delay(WriteDelayInMilliseconds);

                Assert.Equal(2, raisedEvents.Count);
                Assert.Equal(new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, raisedEvents[0].Data);
                Assert.Equal(new[] { "10", "11", "12", "13", "14" }, raisedEvents[1].Data);
            }
        }

        [Fact]
        public async Task ShouldNotFireEventOnNewLogLinesAfterObservingFinish()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var watcher = new LogObserver(filepath);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        watcher.Start();
                        await writer.WriteLineAsync("1");
                        watcher.Finish();
                        await writer.WriteLineAsync("2\r\n3");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1" }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldFireEventForPreviouslyNotTrackedLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var raisedEvents = new List<GameStateChangedEventArgs>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => raisedEvents.Add(@event);

                await writer.WriteLineAsync("1");
                watcher.Start();
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");
                await Task.Delay(WriteDelayInMilliseconds);

                Assert.True(raisedEvents.Count > 0);
                Assert.Equal(new[] { "1", "2", "3" }, raisedEvents.SelectMany(x => x.Data).ToArray());
            }
        }

        [Fact]
        public async Task ShouldNotFireEventForPreviouslyTrackedLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var raisedEvents = new List<GameStateChangedEventArgs>();
                var watcher = new LogObserver(filepath);

                watcher.Start();
                await writer.WriteLineAsync("1");
                watcher.Notify += (sender, @event) => raisedEvents.Add(@event);
                await Task.Delay(WriteDelayInMilliseconds);

                await writer.WriteLineAsync("2\r\n3");
                await Task.Delay(WriteDelayInMilliseconds);

                Assert.True(raisedEvents.Count == 1);
                Assert.Equal(new[] { "2", "3" }, raisedEvents[0].Data);
            }
        }

        [Fact]
        public async Task ShouldNotIncludeEmptyLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var watcher = new LogObserver(filepath);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        watcher.Start();
                        await writer.WriteLineAsync("1\r\n\r\n2\r\n \r\n  3  \r\n      \r\n4\r\n      5\r\n6      ");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1", "2", "  3  ", "4", "      5", "6      " }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldNotFireEventOnEmptyLinesOnly()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                bool isEventRaised = false;
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => isEventRaised = true;

                watcher.Start();
                await writer.WriteLineAsync(" ");
                await writer.WriteLineAsync(string.Empty);
                await Task.Delay(WriteDelayInMilliseconds);
                Assert.False(isEventRaised);
            }
        }

        [Fact]
        public async Task ShouldFireEventForNewCharsOnAlreadyTrackedLine()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var raisedEvents = new List<GameStateChangedEventArgs>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => raisedEvents.Add(@event);

                watcher.Start();
                await writer.WriteAsync("1");
                await Task.Delay(WriteDelayInMilliseconds);
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");
                await Task.Delay(WriteDelayInMilliseconds);

                Assert.True(raisedEvents.Count > 0);
                Assert.Equal(new[] { "1", "2", "3" }, raisedEvents.SelectMany(x => x.Data).ToArray());
            }
        }

        [Fact]
        public async Task ShouldNotFireEventWhenObserverNotStarted()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                bool isEventRaised = false;
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => isEventRaised = true;

                await writer.WriteLineAsync("1");
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");
                await Task.Delay(WriteDelayInMilliseconds);

                Assert.False(isEventRaised);
            }
        }

        private StreamWriter CreateConcurrentWriter(string filepath) =>
            new StreamWriter(
                new FileStream(
                    filepath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.ReadWrite))
            {
                AutoFlush = true,
            };
    }
}
