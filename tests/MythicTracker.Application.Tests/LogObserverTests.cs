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
                await Task.Delay(WriteDelayInMilliseconds);

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
        public async Task ShouldNotFireEventForNewCharsOnAlreadyTrackedLine()
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

                Assert.Equal(new[] { "1", "3" }, raisedEvent.Arguments.Data);
            }
        }

        [Fact]
        public async Task ShouldNotFireEventWhenObserverNotStarted()
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
                        await writer.WriteLineAsync("1");
                        await writer.WriteLineAsync("2");
                        await writer.WriteLineAsync("3");
                        await Task.Delay(WriteDelayInMilliseconds);
                    });

                Assert.Equal(new string[0], raisedEvent.Arguments.Data);
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
