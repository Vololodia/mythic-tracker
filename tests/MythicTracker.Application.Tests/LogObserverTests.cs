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
        [Fact]
        public async Task ShouldFireEventOnNewLogLine()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);

                watcher.Start();
                await writer.WriteLineAsync("1");

                Assert.Equal(new[] { "1" }, newLines);
            }
        }

        [Fact]
        public async Task ShouldFireEventOnNewLogLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);

                watcher.Start();
                await writer.WriteLineAsync("1");
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");

                Assert.Equal(new[] { "1", "2", "3" }, newLines);
            }
        }

        [Fact]
        public async Task ShouldNotFireEventOnNewLogLinesAfterObservingFinish()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);

                watcher.Start();
                await writer.WriteLineAsync("1");
                watcher.Finish();
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");

                Assert.Equal(new[] { "1" }, newLines);
            }
        }

        [Fact]
        public async Task ShouldFireEventForPreviouslyNotTrackedLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);

                await writer.WriteLineAsync("1");
                watcher.Start();
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");

                Assert.Equal(new[] { "1", "2", "3" }, newLines);
            }
        }

        [Fact]
        public async Task ShouldNotFireEventForPreviouslyTrackedLines()
        {
            var filepath = $"./{Guid.NewGuid()}";
            using (var writer = CreateConcurrentWriter(filepath))
            {
                var newLines = new List<string>();
                var watcher = new LogObserver(filepath);
                watcher.Start();

                await writer.WriteLineAsync("1");
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");

                Assert.Equal(new[] { "2", "3" }, newLines);
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
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);

                watcher.Start();
                await writer.WriteAsync("1");
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");

                Assert.Equal(new[] { "1", "3" }, newLines);
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
                watcher.Notify += (sender, @event) => newLines.AddRange(@event.Data);

                await writer.WriteLineAsync("1");
                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync("3");

                Assert.Equal(new string[0], newLines);
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
