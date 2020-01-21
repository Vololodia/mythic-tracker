using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MythicTracker.Application.GameStateObserver;
using Xunit;

namespace MythicTracker.Application.Tests
{
    public class LogObserverTests
    {
        private const int WriteDelayInMilliseconds = 1000;

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
                        writer.WriteLine("1");
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
                        await writer.WriteLineAsync("1");
                        await writer.WriteLineAsync("2");
                        await writer.WriteLineAsync("3");
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
                var newLines = new List<GameStateChangedEventArgs>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => newLines.Add(@event);
                await writer.WriteLineAsync("0");
                await writer.WriteLineAsync("1");
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");
                await writer.WriteLineAsync("4");
                await writer.WriteLineAsync("5");
                await writer.WriteLineAsync("6");
                await writer.WriteLineAsync("7");
                await writer.WriteLineAsync("8");
                await writer.WriteLineAsync("9");
                await writer.WriteLineAsync("10");
                await writer.WriteLineAsync("11");
                await writer.WriteLineAsync("12");
                await writer.WriteLineAsync("13");
                await writer.WriteLineAsync("14");
                watcher.Start();
                await Task.Delay(WriteDelayInMilliseconds);
                Assert.Equal(2, newLines.Count);
                Assert.Equal(new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, newLines[0].Data);
                Assert.Equal(new[] { "10", "11", "12", "13", "14"}, newLines[1].Data);
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
                        await writer.WriteLineAsync("2");
                        await writer.WriteLineAsync("3");
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
                var watcher = new LogObserver(filepath);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        await writer.WriteLineAsync("1");
                        watcher.Start();
                        await writer.WriteLineAsync("2");
                        await writer.WriteLineAsync("3");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1", "2", "3" }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldNotFireEventForPreviouslyTrackedLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var watcher = new LogObserver(filepath);
                watcher.Start();
                await writer.WriteLineAsync("1");
                await Task.Delay(100);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        await writer.WriteLineAsync("2");
                        await writer.WriteLineAsync("3");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "2", "3" }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldNotIncludeEmptyLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);
                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        watcher.Start();
                        await writer.WriteAsync(" ");
                        await writer.WriteAsync("1");
                        await writer.WriteAsync("      ");
                        await writer.WriteAsync("2");
                        await writer.WriteLineAsync("  ");
                        await writer.WriteAsync("3");
                        await writer.WriteAsync(string.Empty);
                        await writer.WriteAsync("4");
                        await writer.WriteLineAsync(string.Empty);
                        await writer.WriteAsync("5");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1", "2", "3", "4", "5"}, raisedEvent.Arguments.Data);
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
                await writer.WriteAsync(" ");
                await writer.WriteAsync(string.Empty);
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
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);

                var raisedEvent = await Assert.RaisesAsync<GameStateChangedEventArgs>(
                    handler => watcher.Notify += handler,
                    handler => watcher.Notify -= handler,
                    async () =>
                    {
                        watcher.Start();
                        await writer.WriteAsync("1");
                        await Task.Delay(WriteDelayInMilliseconds);
                        await writer.WriteLineAsync("2");
                        await writer.WriteLineAsync("3");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new[] { "1", "2", "3" }, raisedEvent.Arguments.Data);
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
