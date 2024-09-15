using App;

namespace Test {
    [TestClass]
    public class RomanNumFactoryTest {
        [DataRow("IIV", "Неверная последовательность цифр: I меньше 1 и 5")]
        [DataRow("XXL", "Неверная числовая последовательность: X меньше 10 и 50")]
        [TestMethod]
        public void ParseAsIntTest_FormatException(string input, string message) {
            var ex = Assert.ThrowsException<FormatException>(() => { RomanNumFactory.ParseAsInt(input); });
            Assert.AreEqual(message, ex.Message);
        }
    }
}