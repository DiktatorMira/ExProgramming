using App;

namespace Test {
    [TestClass]
    public class RomanNumMathTest {
        [TestMethod]
        public void PlusTest() {
            RomanNum rn1 = new(1), rn2 = new(2), rn3 = new(3);
            Assert.AreEqual(6, RomanNumMath.Plus(rn1, rn2, rn3).value);
            Assert.AreEqual(6, RomanNumMath.Plus([rn1, rn2, rn3]).value);
        }
    }
}