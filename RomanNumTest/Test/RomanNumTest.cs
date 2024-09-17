using App;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Reflection;

namespace Test {
    class TestCase {
        public string? Source { get; set; }
        public int? Value { get; set; }
        public Type? ExceptionType { get; set; }
        public IEnumerable<string>? ExceptionMessageParts { get; set; }
    }
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
            var Assert_ThrowsException_Methods = typeof(Assert).GetMethods().Where(x => x.Name == "ThrowsException").Where(x => x.IsGenericMethod);
            var Assert_ThrowsException_Method = Assert_ThrowsException_Methods.Skip(3).FirstOrDefault();

            TestCase[] testCases1 = [
                new(){Source ="N", Value = 0},
                new(){Source ="I", Value = 1},
                new(){Source ="II", Value = 2},
                new(){Source ="III", Value = 3},
                new(){Source ="IV", Value = 4},
                new(){Source ="V", Value = 5},
                new(){Source ="VI", Value = 6},
                new(){Source ="VII", Value = 7},
                new(){Source ="VIII", Value = 8},
                new(){Source ="D", Value = 500},
                new(){Source ="CM", Value = 900},
                new(){Source ="M", Value = 1000},
                new(){Source ="MC", Value = 1100},
                new(){Source ="MCM", Value = 1900},
                new(){Source ="MM", Value = 2000},
            ];
            foreach (TestCase testCase in testCases1) {
                RomanNum rn = RomanNumFactory.Parse(testCase.Source!);
                Assert.IsNotNull(rn);
                Assert.AreEqual(testCase.Value, rn.value, $"{testCase.Source} синтаксический анализ не удался. Ожидалось {testCase.Value}, получено {rn.value}.");
            }

            var formatExceptionType = typeof(FormatException);
            string part1Template = "Неверный символ '{0}' в позиции {1}";
            TestCase[] testCases2 = [
                new(){Source = "W", ExceptionType = formatExceptionType, ExceptionMessageParts=[string.Format(part1Template, 'W', 0)]},
                new(){Source = "Q", ExceptionType = formatExceptionType, ExceptionMessageParts=[string.Format(part1Template, 'Q', 0)]},
                new(){Source = "s", ExceptionType = formatExceptionType, ExceptionMessageParts=[string.Format(part1Template, 's', 0)]},
                new(){Source = "sX", ExceptionType = formatExceptionType, ExceptionMessageParts=[string.Format(part1Template, 's', 0)]},
                new(){Source = "Xd", ExceptionType = formatExceptionType, ExceptionMessageParts=[string.Format(part1Template, 'd', 1)]},
            ];

            foreach (TestCase testCase in testCases2) {
                dynamic? ex = Assert_ThrowsException_Method?.MakeGenericMethod(testCase.ExceptionType!).Invoke(null, [() => RomanNumFactory.Parse(testCase.Source!), $"Parse('{testCase.Source}') должен выдать FormatException"]);
                Assert.IsTrue(ex!.Message.Contains(testCase.ExceptionMessageParts!.First()),
                    $"Parse('{testCase.Source}') FormatException должен содержать '{testCase.ExceptionMessageParts!.First()}'");
            }

            TestCase[] testCases3 = {
                new() { Source = "IM",  ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "XIM", ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 1" }, ExceptionType = formatExceptionType },
                new() { Source = "IMX", ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "XMD", ExceptionMessageParts = new[] { "Invalid order 'X' before 'M' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "XID", ExceptionMessageParts = new[] { "Invalid order 'I' before 'D' in position 1" }, ExceptionType = formatExceptionType },
                new() { Source = "VX",  ExceptionMessageParts = new[] { "Invalid order 'V' before 'X' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "VL",  ExceptionMessageParts = new[] { "Invalid order 'V' before 'L' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "LC",  ExceptionMessageParts = new[] { "Invalid order 'L' before 'C' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "DM",  ExceptionMessageParts = new[] { "Invalid order 'D' before 'M' in position 0" }, ExceptionType = formatExceptionType }
            };
            foreach (var testCase in testCases3) {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumFactory.Parse(testCase.Source!),
                    $"Parse '{testCase.Source}' должен выдать FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains(testCase.ExceptionMessageParts!.First()),
                    "FormatException должен содержать данные о неправильно упорядоченных символах и их положении"
                );
            }

            TestCase[] testCases4 = {
                new() { Source = "IXIX", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "IXX",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "IXIV", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCXC", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "CMM",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "CMCD", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCXL", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCC",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCCI", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType }
            };
            foreach (TestCase testCase in testCases4) {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumFactory.Parse(testCase.Source!),
                    $"Parse('{testCase.Source}') должен выдать FormatException"
                );
                foreach (var part in testCase.ExceptionMessageParts!) {
                    Assert.IsTrue(ex.Message.Contains(part),
                        $"Parse('{testCase.Source}') FormatException должен создержать '{part}'. Нынешнее сообщение: {ex.Message}");
                }
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