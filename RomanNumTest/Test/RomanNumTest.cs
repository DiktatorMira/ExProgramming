using App;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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