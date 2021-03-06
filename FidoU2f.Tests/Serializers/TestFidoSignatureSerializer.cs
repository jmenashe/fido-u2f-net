﻿//
// The MIT License(MIT)
//
// Copyright(c) 2015 Hans Wolff
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Linq;
using System.Security.Cryptography;
using FidoU2f.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FidoU2f.Tests.Serializers
{
    [TestFixture]
    public class TestFidoSignatureSerializer
    {
        [Test]
        public void SerializeObject()
        {
            var randomBytes = new byte[256];
            RandomNumberGenerator.Create().GetBytes(randomBytes);

            var value = new FidoSignature(randomBytes);
            var serialized = JsonConvert.SerializeObject(value);

            var bytes = WebSafeBase64Converter.FromBase64String(serialized.Trim('"'));
            Assert.IsTrue(randomBytes.SequenceEqual(bytes));
        }

        [Test]
        public void DeserializeObject()
        {
            var publicKey = new byte[256];
            RandomNumberGenerator.Create().GetBytes(publicKey);

            var value = new FidoSignature(publicKey);
            var serialized = JsonConvert.SerializeObject(value);

            var deserialized = JsonConvert.DeserializeObject<FidoSignature>(serialized);

            Assert.AreEqual(value, deserialized);
        }
    }
}
