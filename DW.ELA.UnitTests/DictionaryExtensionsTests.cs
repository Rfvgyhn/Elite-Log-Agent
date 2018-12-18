﻿namespace DW.ELA.UnitTests
{
    using System;
    using System.Collections.Generic;
    using DW.ELA.Utility.Extensions;
    using NUnit.Framework;

    public class DictionaryExtensionsTests
    {
        [Test]
        public void GetOrDefaultShouldReturnValue()
        {
            var result = GetTestDictionary().GetValueOrDefault("A");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetOrDefaultShouldReturnNull()
        {
            var result = GetTestDictionary().GetValueOrDefault("D");
            Assert.IsNull(result);
        }

        [Test]
        public void AddIfNotNullShouldAdd()
        {
            var dictionary = GetTestDictionary();
            dictionary.AddIfNotNull("D", 4);
            Assert.AreEqual(4, dictionary["D"]);
        }

        [Test]
        public void AddIfNotNullShouldThrow()
        {
            var dictionary = GetTestDictionary();
            Assert.Throws<ArgumentException>(() => dictionary.AddIfNotNull("C", 4));
        }

        private IDictionary<string, int?> GetTestDictionary() => new Dictionary<string, int?> { { "A", 1 }, { "B", null }, { "C", 3 } };
    }
}
