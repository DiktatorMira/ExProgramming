using App;

namespace Test {
    [TestClass]
    public class RomanNumberTest {
        Dictionary<string, int> digitValues = new() {
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
        public void ParseTest() {
            //Dictionary<string, int> testCases1 = new() {
            //    {"N", 0},
            //    {"I", 1},
            //    {"II", 2},
            //    {"III", 3},
            //    {"IIII", 4},
            //    {"IV", 4},
            //    {"V", 5},
            //    {"VI", 6},
            //    {"VII", 7},
            //    {"VIII", 8},
            //    {"VIIII", 9},
            //    {"IX", 9},
            //    {"X", 10},
            //    {"XI", 11},
            //    {"XII", 12},
            //    {"XIII", 13},
            //    {"XIIII", 14},
            //    {"XIV", 14},
            //    {"XV", 15},
            //    {"XVI", 16},
            //    {"XX", 20},
            //    {"XXX", 30},
            //    {"XL", 40},
            //    {"XXXX", 40},
            //    {"L", 50},
            //    {"LX", 60},
            //    {"LXXXX", 90},
            //    {"XC", 90},
            //    {"C", 100},
            //    {"CC", 200},
            //    {"CCC", 300},
            //    {"CD", 400},
            //    {"D", 500},
            //    {"DC", 600},
            //    {"DCCC", 800},
            //    {"CM", 900},
            //    {"M", 1000},
            //    {"MC", 1100},
            //    {"MCM", 1900},
            //    {"MM", 2000},
            //    {"MMM", 3000},
            //    {"MMMM", 4000}
            //};
            //foreach (var testCase in testCases1) {
            //    RomanNum rn = RomanNum.Parse(testCase.Key);
            //    Assert.IsNotNull(rn);
            //    Assert.AreEqual(testCase.Value, rn.Number, $"{testCase.Key} -> {testCase.Value}");
            //}

            //Dictionary<string, (char, int)[]> testCases2 = new() {
            //    {"W", new[] {('W', 0)}},
            //    {"Q", new[] {('Q', 0)}},
            //    {"s", new[] {('s', 0)}},
            //    {"Xd", new[] {('d', 1)}},
            //    {"SWXF", new[] { ('S', 0), ('W', 1), ('F', 3) }},
            //    {"AIXL", new[] { ('A', 0) }},
            //    {"MMQ", new[] { ('Q', 2) }},
            //    {"XDDX", new[] { ('D', 1), ('D', 2) }},
            //    {"YIYV", new[] { ('Y', 0), ('Y', 2) }},
            //};
            //foreach (var testCase in testCases2) {
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNum.Parse(testCase.Key),
            //        $"{nameof(FormatException)} анализ '{testCase.Key}' должен выдать"
            //    );
            //    foreach (var (symbol, position) in testCase.Value) {
            //        Assert.IsTrue(ex.Message.Contains($"Недопустимый символ '{symbol}' в позиции {position}"),
            //            $"{nameof(FormatException)} должен содержать данные о символе '{symbol}' в позиции {position}. " +
            //            $"Case: '{testCase.Key}', ex.Message: '{ex.Message}'"
            //        );
            //    }
            //}

            //Dictionary<string, Object[]> invTestCases = new() {
            //    { "IM",  ['I', 'M', 0] },
            //    { "XIM", ['I', 'M', 1] },
            //    { "IMX", ['I', 'M', 0] },
            //    { "XMD", ['X', 'M', 0] },
            //    { "XID", ['I', 'D', 1] },
            //    { "ID", ['I', 'D', 0] },
            //    { "VX", ['V', 'X', 0] },
            //    { "LC", ['L', 'C', 0] },
            //    { "VV", ['V', 'V', 0] },
            //    { "LL", ['L', 'L', 0] }
            //};
            //foreach (var testCase in invTestCases) {
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNum.Parse(testCase.Key),
            //        $"{nameof(FormatException)} Парсинг '{testCase.Key}' должен выбросить исключение"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"Неверный порядок '{testCase.Value[0]}' перед '{testCase.Value[1]}' в позиции {testCase.Value[2]}"),
            //        $"FormatException должен содержать данные о неправильно упорядоченных символах. TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
            //    );
            //}

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