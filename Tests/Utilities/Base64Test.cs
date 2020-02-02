// ---------------------------------------------------------------------
// <copyright file="Base64Test.cs" company="zwei222">
// Copyright (c) zwei222. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
using DotNetCommons.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommons.Tests.Utilities
{
    /// <summary>
    /// Test class for ExpressionExtension class.
    /// </summary>
    [TestClass]
    public class Base64Test
    {
        /// <summary>
        /// Test method for Encode method.
        /// </summary>
        [TestMethod]
        public void EncodeTest_001()
        {
            var source = "qwertyuiop";
            var encoding = Encoding.UTF8;
            var expected = Convert.ToBase64String(encoding.GetBytes(source));
            var actual = Base64.Encode(source, encoding);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method for Decode method.
        /// </summary>
        [TestMethod]
        public void DecodeTest_001()
        {
            var original = "qwertyuiop";
            var encoding = Encoding.UTF8;
            var source = Convert.ToBase64String(encoding.GetBytes(original));
            var expected = encoding.GetString(Convert.FromBase64String(source));
            var actual = Base64.Decode(source, encoding);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(original, actual);
        }

        /// <summary>
        /// Test method for EncodeUrl method.
        /// </summary>
        [TestMethod]
        public void EncodeUrlTest_001()
        {
            var source = @"https://example.com?id=001&name=foo bar";
            var encoding = Encoding.UTF8;
            var expected = Convert.ToBase64String(encoding.GetBytes(source)).TrimEnd('=').Replace('+', '-')
                .Replace('/', '_');
            var actual = Base64.EncodeUrl(source, encoding);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method for DecodeUrl method.
        /// </summary>
        [TestMethod]
        public void DecodeUrlTest_001()
        {
            var original = "https://example.com?id=001&name=foo bar";
            var encoding = Encoding.UTF8;
            var source = Convert.ToBase64String(encoding.GetBytes(original)).TrimEnd('=').Replace('+', '-')
                .Replace('/', '_');
            var expectedSource = new StringBuilder(source);

            expectedSource.Append(Enumerable.Range(0, source.Length % 4).Select(_ => '=').ToArray().AsSpan())
                .Replace('-', '+').Replace('_', '/');

            var expected = encoding.GetString(Convert.FromBase64String(expectedSource.ToString()));
            var actual = Base64.DecodeUrl(expectedSource.ToString(), encoding);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(original, actual);
        }

        /// <summary>
        /// Test method for EncodeUtf8 method.
        /// </summary>
        [TestMethod]
        public void EncodeUtf8Test_001()
        {
            var source = "qwertyuiop";
            var encoding = Encoding.UTF8;
            var expected = Convert.ToBase64String(encoding.GetBytes(source));
            var actual = Base64.EncodeUtf8(source);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method for DecodeUtf8 method.
        /// </summary>
        [TestMethod]
        public void DecodeUtf8Test_001()
        {
            var original = "qwertyuiop";
            var encoding = Encoding.UTF8;
            var source = Convert.ToBase64String(encoding.GetBytes(original));
            var expected = encoding.GetString(Convert.FromBase64String(source));
            var actual = Base64.DecodeUtf8(source);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(original, actual);
        }

        /// <summary>
        /// Test method for EncodeUtf8Url method.
        /// </summary>
        [TestMethod]
        public void EncodeUtf8UrlTest_001()
        {
            var source = @"https://example.com?id=001&name=foo bar";
            var encoding = Encoding.UTF8;
            var expected = Convert.ToBase64String(encoding.GetBytes(source)).TrimEnd('=').Replace('+', '-')
                .Replace('/', '_');
            var actual = Base64.EncodeUtf8Url(source);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method for DecodeUtf8Url method.
        /// </summary>
        [TestMethod]
        public void DecodeUtf8UrlTest_001()
        {
            var original = "https://example.com?id=001&name=foo bar";
            var encoding = Encoding.UTF8;
            var source = Convert.ToBase64String(encoding.GetBytes(original)).TrimEnd('=').Replace('+', '-')
                .Replace('/', '_');
            var expectedSource = new StringBuilder(source);

            expectedSource.Append(Enumerable.Range(0, source.Length % 4).Select(_ => '=').ToArray().AsSpan())
                .Replace('-', '+').Replace('_', '/');

            var expected = encoding.GetString(Convert.FromBase64String(expectedSource.ToString()));
            var actual = Base64.DecodeUtf8Url(expectedSource.ToString());

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(original, actual);
        }
    }
}