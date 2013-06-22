using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace LastPass.Test
{
    [TestFixture]
    class FetcherHelperTest
    {
        [Test]
        public void MakeKey()
        {
            var testCases = new Dictionary<int, string>
            {
                {1, "C/Bh2SGWxI8JDu54DbbpV8J9wa6pKbesIb9MAXkeF3Y="},
                {5, "pE9goazSCRqnWwcixWM4NHJjWMvB5T15dMhe6ug1pZg="},
                {10, "n9S0SyJdrMegeBHtkxUx8Lzc7wI6aGl+y3/udGmVey8="},
                {50, "GwI8/kNy1NjIfe3Z0VAZfF78938UVuCi6xAL3MJBux0="},
                {100, "piGdSULeHMWiBS3QJNM46M5PIYwQXA6cNS10pLB3Xf8="},
                {500, "OfOUvVnQzB4v49sNh4+PdwIFb9Fr5+jVfWRTf+E2Ghg="},
                {1000, "z7CdwlIkbu0XvcB7oQIpnlqwNGemdrGTBmDKnL9taPg="},
            };

            var username = "postlass@gmail.com";
            var password = "pl1234567890";

            foreach (var i in testCases)
            {
                var result = FetcherHelper.MakeKey(username, password, i.Key);
                Assert.AreEqual(Convert.FromBase64String(i.Value), result);
            }
        }

        [Test]
        public void MakeHash()
        {
            var testCases = new Dictionary<int, string>
            {
                {1, "a1943cfbb75e37b129bbf78b9baeab4ae6dd08225776397f66b8e0c7a913a055"},
                {5, "a95849e029a7791cfc4503eed9ec96ab8675c4a7c4e82b00553ddd179b3d8445"},
                {10, "0da0b44f5e6b7306f14e92de6d629446370d05afeb1dc07cfcbe25f169170c16"},
                {50, "1d5bc0d636da4ad469cefe56c42c2ff71589facb9c83f08fcf7711a7891cc159"},
                {100, "82fc12024acb618878ba231a9948c49c6f46e30b5a09c11d87f6d3338babacb5"},
                {500, "3139861ae962801b59fc41ff7eeb11f84ca56d810ab490f0d8c89d9d9ab07aa6"},
                {1000, "03161354566c396fcd624a424164160e890e96b4b5fa6d942fc6377ab613513b"},
            };

            var username = "postlass@gmail.com";
            var password = "pl1234567890";

            foreach (var i in testCases)
            {
                var result = FetcherHelper.MakeHash(username, password, i.Key);
                Assert.AreEqual(i.Value, result);
            }
        }

        [Test]
        public void ToHexString()
        {
            var testCases = new Dictionary<string, byte[]>
            {
                {"", new byte[] {}},
                {"00", new byte[] {0}},
                {"00ff", new byte[] {0, 255}},
                {"00010203040506070809", new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}},
                {"000102030405060708090a0b0c0d0e0f", new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}},
                {"8af633933e96a3c3550c2734bd814195", new byte[] {0x8a, 0xf6, 0x33, 0x93, 0x3e, 0x96, 0xa3, 0xc3, 0x55, 0x0c, 0x27, 0x34, 0xbd, 0x81, 0x41, 0x95}},
            };

            foreach (var i in testCases)
            {
                Assert.AreEqual(i.Key, FetcherHelper.ToHexString(i.Value));
            }
        }
    }
}