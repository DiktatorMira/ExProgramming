using App;

namespace Test {
    [TestClass]
    public class RomanTest {
        [TestMethod]
        public void ParseTest_EmptyString_ReturnsZero() {
            var numDictionary = new Dictionary<string, int>() {
                {"I", 1},
                {"II", 2},
                {"III", 3},
                {"IV", 4},
                {"V", 5},
                {"VI", 6},
                {"VII", 7},
                {"VIII", 8},
                {"IX", 9},
                {"X", 10},
                {"XL", 40},
                {"L", 50},
                {"XC", 90},
                {"C", 100},
                {"CD", 400},
                {"D", 500},
                {"CM", 900},
                {"M", 1000},
                {"MC", 1100},
                {"MCM", 1900},
                {"MM", 2000},
                {"MMM", 3000},
                {"IIII", 4},
                {"VIIII", 9},
                {"XXXX", 40},
                {"LXXXX", 90},
                {"CCCC", 400},
                {"DCCCC", 900},
            };
            foreach (var test in numDictionary) {
                RomanNum rNum = RomanNum.Parse(test.Key);
                Assert.IsNotNull(rNum);
                Assert.AreEqual(test.Value, rNum.Number, $"{test.Key} -> {test.Value}");
            }

            RomanNum rn = RomanNum.Parse("asdfasdf");
            Assert.IsNotNull(rn);
            Assert.AreEqual(0, rn.value, "Ожидаемое значение для пустой строки — 0.");
        }
        [TestMethod]
        public void InvalidParseTest() {
            string[] invDict = {
                "IIIIII",
                "VV",
                "LL",
                "DD",
                "MMMMM",
                "IC",
                "IM",
                "XD",
                "IL",
                ""
            };
            foreach (var invTemp in invDict) Assert.ThrowsException<ArgumentException>(() => RomanNum.Parse(invTemp), $"Ожидаемое исключение для {invTemp}");
        }
        [TestMethod]
        public void DigitalValueTest() {
            Dictionary<string, int> romNum = new() {
                {"N", 0},
                {"I", 1 },
                {"V", 5 },
                {"X", 10},
                {"L", 50},
                {"C", 100 },
                {"D", 500 },
                {"M", 1000}
            };
            foreach (var temp in romNum) Assert.AreEqual(temp.Value, RomanNum.DigitalValue(temp.Key), $"{temp.Key} -> {temp.Value}");
        }
    }
}