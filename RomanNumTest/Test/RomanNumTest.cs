using App;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Test {
    [TestClass]
    public class RomanNumberTest {
        private readonly Dictionary<string, int> digitValues = new() {
            {"N", 0},
            {"I", 1 },
            {"V", 5 },
            {"X", 10},
            {"L", 50},
            {"C", 100 },
            {"D", 500 },
            {"M", 1000}
        };
        [TestMethod]
        public void CheckSubsTest() {
            Type? rnType = typeof(RomanNum);
            MethodInfo? m1Info = rnType.GetMethod("CheckSubs", BindingFlags.NonPublic | BindingFlags.Static);

            string[] validCases = { "IV", "IX", "XL", "XC", "CD", "CM", "MCMXCIV" };
            foreach (var validCase in validCases) m1Info?.Invoke(null, [validCase]);

            string[] invalidCases = { "IIV", "IIX", "XXL", "XXC", "CCD", "CCM", "IIVX", "IIXX", "IVIV", "IXIX" };
            foreach (var invalidCase in invalidCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [invalidCase]), $"CheckSubs '{invalidCase}' должен выдавать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, "CheckSubs: FormatException из InnerException");
            }
        }
        [TestMethod]
        public void CheckSymbolsTest()  {
            Type? rnType = typeof(RomanNum);
            MethodInfo? m1Info = rnType.GetMethod("CheckSymbols", BindingFlags.NonPublic | BindingFlags.Static);

            string[] validCases = { "I", "V", "X", "L", "C", "D", "M", "MCMLIV", "MMXXIII" };
            foreach (var validCase in validCases) m1Info?.Invoke(null, [validCase]);

            string[] invalidCases = { "A", "E", "K", "O", "P", "R", "S", "T", "U", "W", "Y", "Z", "MCMLIVA", "MM23" };
            foreach (var invalidCase in invalidCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [invalidCase]),
                    $"CheckSymbols '{invalidCase}' должен выдать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(ex.InnerException,  "CheckSymbols: FormatException из InnerException");
            }
        }
        [TestMethod]
        public void CheckPairsTest() {
            Type? rnType = typeof(RomanNum);
            MethodInfo? m1Info = rnType.GetMethod("CheckPairs", BindingFlags.NonPublic | BindingFlags.Static);

            string[] validCases = { "IV", "IX", "XL", "XC", "CD", "CM", "MCMLIV", "MMXXIII" };
            foreach (var validCase in validCases) m1Info?.Invoke(null, [validCase]);

            string[] invalidCases = { "IC", "ID", "IM", "XM", "VX", "VL", "VC", "VD", "VM", "LC", "LD", "LM", "DM" };
            foreach (var invalidCase in invalidCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [invalidCase]),
                    $"CheckPairs '{invalidCase}' должен выдать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, "CheckPairs: FormatException из InnerException");
            }
        }
        [TestMethod]
        public void CheckFormatTest() {
            Type? rnType = typeof(RomanNum);
            MethodInfo? m1Info = rnType.GetMethod("CheckFormat", BindingFlags.NonPublic | BindingFlags.Static);

            string[] validCases = { "IV", "IX", "XL", "XC", "CD", "CM", "MCMLIV", "MMXXIII", "III", "VIII" };
            foreach (var validCase in validCases) m1Info?.Invoke(null, [validCase]);

            string[] invalidCases = { "IIII", "XXXX", "CCCC", "MMMM", "VV", "LL", "DD", "IIV", "IIX", "XXL", "XXC", "CCD", "CCM" };
            foreach (var invalidCase in invalidCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [invalidCase]),
                    $"CheckFormat '{invalidCase}' должен выдать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, "CheckFormat: FormatException из InnerException");
            }
        }
        [TestMethod]
        public void CheckValidityTest() {
            Type? rnType = typeof(RomanNum);
            MethodInfo? m1Info = rnType.GetMethod("CheckValidity", BindingFlags.NonPublic | BindingFlags.Static);
            m1Info?.Invoke(null, ["IX"]);

            string[] testCases = ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL", "CMCD"];
            foreach (var testCase in testCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [testCase]),
                    $"CheckValidity '{testCase}' должен выдать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, "CheckValidity: FormatException из InnerException");
            }
        }
        [TestMethod]
        public void ParseTest() {
            string[] testCases3 = {
                "IXC", "IIX", "VIX",
                "IIXC", "IIIX", "VIIX",
                "VIXC", "IVIX", "CVIIX",
                "IXCC", "IXCM", "IXXC"
            };
            foreach (var testCase in testCases3) {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNum.Parse(testCase),
                    $"{nameof(FormatException)} Parse '{testCase}' must throw"
                );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNum)) &&
                    ex.Message.Contains(nameof(RomanNum.Parse)) &&
                    ex.Message.Contains($"Недопустимая последовательность: более 1 цифры меньше перед '{testCase[^1]}'"),
                    $"ex.Message должно содержать информацию о происхождении, причине и данных. {ex.Message}");
            }
        }
        [TestMethod]
        public void InvalidParseTest()  {
            string[] invalidTestCases = {
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
            foreach (var invalidCase in invalidTestCases)Assert.ThrowsException<ArgumentException>(() => RomanNum.Parse(invalidCase), $"Ожидаемое исключение для {invalidCase}");
        }
        [TestMethod]
        public void DigitalValueTest() {
            foreach (var test in digitValues) Assert.AreEqual(test.Value, RomanNum.DigitalValue(test.Key), $"{test.Key} -> {test.Value}");
            Random rand = new();
            for (int i = 0; i < 100; ++i) {
                string invDigit = ((char)rand.Next(256)).ToString();
                if (digitValues.ContainsKey(invDigit)) {
                    --i;
                    continue;
                }
                ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => RomanNum.DigitalValue(""), "Пустая строка -> ArgumentException");
                Assert.IsFalse(string.IsNullOrEmpty(ex.Message), "ArgemntException должен иметь сообщение");
                Assert.IsTrue(ex.Message.Contains($"«digit» имеет недопустимое значение «{invDigit}»'"), $"Сообщение ArgemntException должно содержать <'digit' имеет недопустимое значение '{invDigit}'>");
                Assert.IsTrue(ex.Message.Contains(nameof(RomanNum)) && ex.Message.Contains(nameof(RomanNum.DigitalValue)), "Сообщение ArgemntException должно содержать «digit»");
            }
        }
        [TestMethod]
        public void ToStringTest() {
            Dictionary<int, String> testCases = new() {
                {2, "II"},
                {3343, "MMMCCCXLIII"},
                {4, "IV" },
                {44, "XLIV" },
                {9,"IX" },
                {90, "XC" },
                {1400, "MCD" },
                {999, "CMXCIX" },
                {444, "CDXLIV" },
                {990, "CMXC" }
            };
            digitValues.Keys.ToList().ForEach(i => testCases.Add(digitValues[i], i));
            foreach (var test in testCases) Assert.AreEqual(test.Value, new RomanNum(test.Key).ToString(), $"ToString({test.Key}) --> {test.Value}");
        }
        [TestMethod]
        public void PlusTest() {
            RomanNum num1 = new(1), num2 = new(2), num3 = num1.Plus(num2);
            Assert.IsNotNull(num3);
            Assert.IsInstanceOfType(num3, typeof(RomanNum));
            Assert.AreNotSame(num3, num1);
            Assert.AreNotSame(num3, num2);
            Assert.AreEqual(num1.Number + num2.Number, num3.Number);
            var testCases = new[] {
                (first: "IV", second: "VI", expected: "X"),
                (first: "X", second: "V", expected: "XV"),
                (first: "XL", second: "IX", expected: "XLIX"),
                (first: "L", second: "L", expected: "C"),
                (first: "C", second: "D", expected: "DC"),
                (first: "MMM", second: "MMM", expected: "MMMMMM")
            };
            foreach (var testCase in testCases) {
                num1 = RomanNum.Parse(testCase.first);
                num2 = RomanNum.Parse(testCase.second);
                num3 = num1.Plus(num2);
                Assert.AreEqual(testCase.expected, num3.ToString(), $"Ожидалось {testCase.first} + {testCase.second} = {testCase.expected}, но получено {num3}");
            }
        }
    }
}