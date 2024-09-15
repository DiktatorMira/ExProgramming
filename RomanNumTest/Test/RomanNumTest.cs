using App;
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
        public void ConstructorTest() {
            var rn = new RomanNum("IX");
            Assert.IsNotNull(rn);
            rn = new RomanNum(3);
            Assert.IsNotNull(rn);
        }
        [TestMethod]
        public void ConvertTest() {
            var rn = new RomanNum("IX");
            Assert.IsInstanceOfType<Int32>(rn.ToInt());
            Assert.IsInstanceOfType<UInt32>(rn.ToUnsignedInt());
            Assert.IsInstanceOfType<Int16>(rn.ToShort());
            Assert.IsInstanceOfType<UInt16>(rn.ToUnsignedShort());
            Assert.IsInstanceOfType<Single>(rn.ToFloat());
            Assert.IsInstanceOfType<Double>(rn.ToDouble());
        }
        [TestMethod]
        public void CheckSymbolsTest() {
            MethodInfo? m1Info = typeof(RomanNumFactory).GetMethod("CheckSymbols",
            BindingFlags.NonPublic | BindingFlags.Static);
            m1Info?.Invoke(null, ["IX"]);

            var ex = Assert.ThrowsException<TargetInvocationException>(
                () => m1Info?.Invoke(null, ["IW"]),
                $"Анализ «IW» должен выдать FormatException"
            );
            Assert.IsInstanceOfType<FormatException>(ex.InnerException, $"FormatException из InnerException");
        }
        [TestMethod]
        public void CheckFormatTest() {
            MethodInfo? m1Info = typeof(RomanNumFactory).GetMethod("CheckFormat",
            BindingFlags.NonPublic | BindingFlags.Static);
            m1Info?.Invoke(null, ["IX"]);

            var ex = Assert.ThrowsException<TargetInvocationException>(
                () => m1Info?.Invoke(null, ["IIX"]),
                $"CheckFormat 'IIX' должен выдать FormatException"
            );
            Assert.IsInstanceOfType<FormatException>(ex.InnerException, $"CheckFormat:FormatException из InnerException");
        }
        [TestMethod]
        public void CheckValidityTest()  {
            MethodInfo? m1Info = typeof(RomanNumFactory).GetMethod("CheckValidity",
            BindingFlags.NonPublic | BindingFlags.Static);
            m1Info?.Invoke(null, ["IX"]);

            string[] testCases = ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL", "CMCD"];
            foreach (var testCase in testCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [testCase]),
                    $"CheckValidity '{testCase}' должен выдать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, $"CheckValidity:FormatException из InnerException");
            }
        }
        [TestMethod]
        public void ParseTest() {
            var testCases = new Dictionary<string, int>() {
                { "N",      0 },
                { "I",      1 },
                { "II",     2 },
                { "III",    3 },
                { "IV",     4 },
                { "V",      5 },
                { "VI",     6 },
                { "VII",    7 },
                { "VIII",   8 },
                { "D",      500 },
                { "CM",     900 },
                { "M",      1000 },
                { "MC",     1100 },
                { "MCM",    1900 },
                { "MM",     2000 },
            };
            foreach (var testCase in testCases) {
                RomanNum rn = RomanNumFactory.Parse(testCase.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(
                    testCase.Value,
                    rn.value,
                    $"Ошибка разбора {testCase.Key}. Ожидалось {testCase.Value}, получено {rn.value}."
                );
            }
        }
        [TestMethod]
        public void DigitalValueTest() {
            Dictionary<string, int> testCases = new() {
                {"N", 0 },
                {"I", 1 },
                {"V", 5 },
                {"X", 10 },
                {"L", 50 },
                {"C", 100 },
                {"D", 500 },
                {"M", 1000 },
            };
            foreach (var testCase in testCases) {
                Assert.AreEqual(
                    testCase.Value,
                    RomanNumFactory.DigitValue(testCase.Key),
                    $"{testCase.Key} -> {testCase.Value}"
                );
            }

            Random random = new Random();
            for (int i = 0; i < 100; i++) {
                string invalidDigit = ((char)random.Next(256)).ToString();
                if (testCases.ContainsKey(invalidDigit)) {
                    i--;
                    continue;
                }
                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                    () => RomanNumFactory.DigitValue(invalidDigit),
                    $"ArgumentException ожидается для цифры = '{invalidDigit}'"
                );
                Assert.IsFalse(
                    string.IsNullOrEmpty(ex.Message),
                    "ArgumnetException должно иметь сообщение"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'digit' имеет недопустимое значение '{invalidDigit}'"),
                    "ArgumnetException должен содержать <'digit' имеет недопустимое значение ''>"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'digit'"),
                    "ArgumnetException должен содержать 'digit'"
                );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNum)) &&
                    ex.Message.Contains(nameof(RomanNumFactory.DigitValue)),
                    $"ArgumnetExceptionmust должен содеражть '{nameof(RomanNum)}' и '{nameof(RomanNumFactory.DigitValue)}'"
                    );
                var ex2 = Assert.ThrowsException<FormatException>(() => RomanNumFactory.Parse("W"), "Неверный формат");
                Assert.IsTrue(
                    ex2.Message.Contains("Недопустимый символ «W» в позиции 0"),
                    "FormatException должен содержать данные о символе и его позиции"
                );
            }
        }
        [TestMethod]
        public void ToStringTest() {
            Dictionary<int, string> testCases = new() {
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
    }
}