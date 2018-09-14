﻿using Controller;
using DW.ELA.Interfaces;
using DW.ELA.Utility.Json;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DW.ELA.UnitTests
{
    [TestFixture]
    public class JournalMonitorTests
    {
        [Test]
        public async Task ShouldPickUpEvents()
        {
            var directoryProvider = new TestDirectoryProvider();
            Directory.Delete(directoryProvider.Directory, true);
            var events = new ConcurrentBag<LogEvent>();
            CollectionAssert.IsEmpty(events);

            string testFile1 = Path.Combine(directoryProvider.Directory, "Journal.1234.log");
            string testFile2 = Path.Combine(directoryProvider.Directory, "Journal.2345.log");

            File.WriteAllText(testFile1, EventsAsJson.Skip(5).First());
            var journalMonitor = new JournalMonitor(directoryProvider, 5);
            journalMonitor.Subscribe(events.Add);

            File.AppendAllText(testFile1, EventsAsJson.Skip(8).First());
            await Delay;
            CollectionAssert.IsNotEmpty(events);

            while (events.Count > 0)
                events.TryTake(out var e);

            File.WriteAllText(testFile2, EventsAsJson.Skip(9).First());
            await Delay;
            CollectionAssert.IsNotEmpty(events);

            while (events.Count > 0)
                events.TryTake(out var e);

            await Delay;
            CollectionAssert.IsEmpty(events);
        }

        private static IEnumerable<string> EventsAsJson => TestEventSource.LogEvents.Select(Serialize.ToJson);

        private Task Delay => Task.Delay(50);

        private class TestDirectoryProvider : ILogDirectoryNameProvider
        {
            public string Directory
            {
                get
                {
                    string dir = Path.Combine(Path.GetTempPath(), "ELA-TEST");
                    System.IO.Directory.CreateDirectory(dir);
                    return dir;
                }
            }
        }
    }
}
