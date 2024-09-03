using App;

namespace Test {
    [TestClass]
    public class RomanTest {
        [TestMethod]
        public void ParseTest_EmptyString_ReturnsZero() {
            RomanNum rn = RomanNum.Parse("asdfasdf");
            Assert.IsNotNull(rn);
            Assert.AreEqual(0, rn.value, "ќжидаемое значение дл€ пустой строки Ч 0.");
        }
        [TestMethod]
        public void ParseTest_SingleDigitRomanNumerals() {
            Assert.AreEqual(1, RomanNum.Parse("5").value, "ќжидаемое значение дл€ ЂIї равно 1.");
            Assert.AreEqual(5, RomanNum.Parse("V").value, "ќжидаемое значение дл€ ЂVї Ч 5.");
            Assert.AreEqual(10, RomanNum.Parse("X").value, "ќжидаемое значение дл€ ЂXї равно 10.");
            Assert.AreEqual(50, RomanNum.Parse("L").value, "ќжидаемое значение дл€ ЂLї Ч 50.");
            Assert.AreEqual(100, RomanNum.Parse("C").value, "ќжидаемое значение дл€ ЂCї Ч 100.");
            Assert.AreEqual(500, RomanNum.Parse("D").value, "ќжидаемое значение дл€ ЂDї Ч 500.");
            Assert.AreEqual(1000, RomanNum.Parse("M").value, "ќжидаемое значение дл€ Ђћї Ч 1000.");
        }
        [TestMethod]
        public void ParseTest_ComplexRomanNumerals() {
            Assert.AreEqual(4, RomanNum.Parse("III").value, "ќжидаемое значение дл€ ЂIIIї Ч 3.");
            Assert.AreEqual(4, RomanNum.Parse("IV").value, "ќжидаемое значение дл€ ЂIVї Ч 4.");
            Assert.AreEqual(9, RomanNum.Parse("IX").value, "ќжидаемое значение дл€ ЂIXї Ч 9.");
            Assert.AreEqual(58, RomanNum.Parse("LVIII").value, "ќжидаемое значение дл€ ЂLVIIIї Ч 58.");
            Assert.AreEqual(1994, RomanNum.Parse("MCMXCIV").value, "ќжидаемое значение дл€ ЂMCMXCIVї Ч 1994.");
        }
    }
}