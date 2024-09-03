using App;

namespace Test {
    [TestClass]
    public class RomanTest {
        [TestMethod]
        public void ParseTest_EmptyString_ReturnsZero() {
            RomanNum rn = RomanNum.Parse("asdfasdf");
            Assert.IsNotNull(rn);
            Assert.AreEqual(0, rn.value, "��������� �������� ��� ������ ������ � 0.");
        }
        [TestMethod]
        public void ParseTest_SingleDigitRomanNumerals() {
            Assert.AreEqual(1, RomanNum.Parse("5").value, "��������� �������� ��� �I� ����� 1.");
            Assert.AreEqual(5, RomanNum.Parse("V").value, "��������� �������� ��� �V� � 5.");
            Assert.AreEqual(10, RomanNum.Parse("X").value, "��������� �������� ��� �X� ����� 10.");
            Assert.AreEqual(50, RomanNum.Parse("L").value, "��������� �������� ��� �L� � 50.");
            Assert.AreEqual(100, RomanNum.Parse("C").value, "��������� �������� ��� �C� � 100.");
            Assert.AreEqual(500, RomanNum.Parse("D").value, "��������� �������� ��� �D� � 500.");
            Assert.AreEqual(1000, RomanNum.Parse("M").value, "��������� �������� ��� �̻ � 1000.");
        }
        [TestMethod]
        public void ParseTest_ComplexRomanNumerals() {
            Assert.AreEqual(4, RomanNum.Parse("III").value, "��������� �������� ��� �III� � 3.");
            Assert.AreEqual(4, RomanNum.Parse("IV").value, "��������� �������� ��� �IV� � 4.");
            Assert.AreEqual(9, RomanNum.Parse("IX").value, "��������� �������� ��� �IX� � 9.");
            Assert.AreEqual(58, RomanNum.Parse("LVIII").value, "��������� �������� ��� �LVIII� � 58.");
            Assert.AreEqual(1994, RomanNum.Parse("MCMXCIV").value, "��������� �������� ��� �MCMXCIV� � 1994.");
        }
    }
}