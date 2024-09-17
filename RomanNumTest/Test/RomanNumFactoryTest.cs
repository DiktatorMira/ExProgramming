using App;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Test {
    class TestCase {
        public string? Source { get; set; }
        public int? Value { get; set; }
        public Type? ExceptionType { get; set; }
        public IEnumerable<string>? ExceptionMessageParts { get; set; }
    }
    [TestClass]
    public class RomanNumFactoryTest {
        private static readonly Dictionary<string, int> digitValues = new() {
            { "N", 0    },
            { "I", 1    },
            { "V", 5    },
            { "X", 10   },
            { "L", 50   },
            { "C", 100  },
            { "D", 500  },
            { "M", 1000 },
        };
        public static ReadOnlyDictionary<string, int> DigitValues => new(digitValues);
        [TestMethod]
        public void CheckSymbolsTest() => CheckPrivateMethod("CheckSymbols", ["IW"]);
        [TestMethod]
        public void CheckPairsTest() => CheckPrivateMethod("CheckPairs", ["IM"]);
        [TestMethod]
        public void CheckFormatTest() => CheckPrivateMethod("CheckFormat", ["IIX"]);
        [TestMethod]
        public void CheckValidityTest() =>CheckPrivateMethod("CheckValidity", ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL", "CMCD"]);
        private void CheckPrivateMethod(string methodName, string[] testCases) {
            Type? rnType = typeof(RomanNumFactory);
            MethodInfo? m1Info = rnType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            m1Info?.Invoke(null, ["IX"]);

            foreach (var testCase in testCases) {
                var ex = Assert.ThrowsException<TargetInvocationException>(
                    () => m1Info?.Invoke(null, [testCase]),
                    $"{methodName} '{testCase}' должен выдать FormatException"
                );
                Assert.IsInstanceOfType<FormatException>(
                    ex.InnerException,
                    "{methodName}: FormatException из InnerException"
                );
            }
        }
        [TestMethod]
        public void ParseTest() {
            var Assert_ThrowsException_Methods = typeof(Assert).GetMethods().Where(x => x.Name == "ThrowsException").Where(x => x.IsGenericMethod);
            var Assert_ThrowsException_Method = Assert_ThrowsException_Methods.Skip(3).FirstOrDefault();

            string ex1Template = "Неверный символ '{0}' в позиции {1}",
                exSrcTemplate = "Parse('{0}')",
                ex2Template = "Неверный порядок '{0}' перед '{1}' в позиции {2}";
            var formatExceptionType = typeof(FormatException);
            TestCase[] testCases = [
                new(){ Source = "N",    Value = 0    },
                new(){ Source = "I",    Value = 1    },
                new(){ Source = "II",   Value = 2    },
                new(){ Source = "III",  Value = 3    },
                new(){ Source = "IIII", Value = 4    },
                new(){ Source = "IV",   Value = 4    },
                new(){ Source = "VI",   Value = 6    },
                new(){ Source = "VII",  Value = 7    },
                new(){ Source = "VIII", Value = 8    },
                new(){ Source = "IX",   Value = 9    },
                new(){ Source = "D",    Value = 500  },
                new(){ Source = "M",    Value = 1000 },
                new(){ Source = "CM",   Value = 900  },
                new(){ Source = "MC",   Value = 1100 },
                new(){ Source = "MCM",  Value = 1900 },
                new(){ Source = "MM",   Value = 2000 },

                new(){ Source = "W",  ExceptionMessageParts = [string.Format(exSrcTemplate, "W" ), string.Format(ex1Template, 'W', 0), ], ExceptionType = formatExceptionType},
                new(){ Source = "Q",  ExceptionMessageParts = [string.Format(exSrcTemplate, "Q" ), string.Format(ex1Template, 'Q', 0), ], ExceptionType = formatExceptionType},
                new(){ Source = "s",  ExceptionMessageParts = [string.Format(exSrcTemplate, "s" ), string.Format(ex1Template, 's', 0), ], ExceptionType = formatExceptionType},
                new(){ Source = "sX", ExceptionMessageParts = [string.Format(exSrcTemplate, "sX"), string.Format(ex1Template, 's', 0), ], ExceptionType = formatExceptionType},
                new(){ Source = "Xd", ExceptionMessageParts = [string.Format(exSrcTemplate, "Xd"), string.Format(ex1Template, 'd', 1), ], ExceptionType = formatExceptionType},

                new(){ Source = "IM",  ExceptionMessageParts = [string.Format(ex2Template, 'I', 'M', 0) ], ExceptionType = formatExceptionType },
                new(){ Source = "XIM", ExceptionMessageParts = [string.Format(ex2Template, 'I', 'M', 1) ], ExceptionType = formatExceptionType },
                new(){ Source = "IMX", ExceptionMessageParts = [string.Format(ex2Template, 'I', 'M', 0) ], ExceptionType = formatExceptionType },
                new(){ Source = "XMD", ExceptionMessageParts = [string.Format(ex2Template, 'X', 'M', 0) ], ExceptionType = formatExceptionType },
                new(){ Source = "XID", ExceptionMessageParts = [string.Format(ex2Template, 'I', 'D', 1) ], ExceptionType = formatExceptionType },
                new(){ Source = "VX",  ExceptionMessageParts = [string.Format(ex2Template, 'V', 'X', 0) ], ExceptionType = formatExceptionType },
                new(){ Source = "VL",  ExceptionMessageParts = [string.Format(ex2Template, 'V', 'L', 0) ], ExceptionType = formatExceptionType },
                new(){ Source = "LC",  ExceptionMessageParts = [string.Format(ex2Template, 'L', 'C', 0) ], ExceptionType = formatExceptionType },
                new(){ Source = "DM",  ExceptionMessageParts = [string.Format(ex2Template, 'D', 'M', 0) ], ExceptionType = formatExceptionType },

                new() { Source = "IXC",   ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IIX",   ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "VIX",   ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "CIIX",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IIIX",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "VIIX",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IVIX",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IXXC",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "IXCM",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "CVIIX", ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "VIXC",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },
                new() { Source = "CIXC",  ExceptionMessageParts = [], ExceptionType = formatExceptionType },

                new() { Source = "IXIX", ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "IXX",  ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "IXIV", ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "XCXC", ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "CMM",  ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "CMCD", ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "XCXL", ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "XCC",  ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType },
                new() { Source = "XCCI", ExceptionMessageParts = [ "Invalid" ], ExceptionType = formatExceptionType }

            ];

            foreach (var testCase in testCases)  {
                if (testCase.Value is not null) {
                    RomanNum rn = RomanNumFactory.Parse(testCase.Source!);
                    Assert.IsNotNull(rn);
                    Assert.AreEqual(
                        testCase.Value,
                        rn.value,
                        $"{testCase.Source} -> {testCase.Value}"
                    );
                } else {
                    dynamic? ex = Assert_ThrowsException_Method? .MakeGenericMethod(testCase.ExceptionType!).Invoke(null, [() => RomanNumFactory.Parse(testCase.Source!), $"Parse('{testCase.Source}') должен выдать FormatException"]);
                    foreach (var exPart in testCase.ExceptionMessageParts ?? []) {
                        Assert.IsTrue(
                            ex!.Message.Contains(exPart),
                            $"Parse('{testCase.Source}') FormatException должно содержать '{exPart}'"
                        );
                    }
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
    }
}