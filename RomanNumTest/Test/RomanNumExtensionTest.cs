using App;

namespace Test {
    [TestClass]
    public class RomanNumExtensionTest {
        [TestMethod]
        public void PlusTest() {
            RomanNum rn1 = new(1), rn2 = new(2);
            Assert.AreEqual(3, rn2.Plus(rn1).value);
            Assert.AreEqual(7, rn2.Plus(rn1, rn2, rn2).value);
            List<object[]> testCases = new List<object[]>() {
                new object[] {"IV","VI","X" },
                new object[] {"IV","N","IV" },
                new object[] {"XXX","XX","L" },
                new object[] {"XL","LX","C" },
            };
            foreach (var item in testCases) {
                var ron1 = RomanNumFactory.Parse(item[0].ToString());
                var ron2 = RomanNumFactory.Parse(item[1].ToString());
                Assert.AreEqual(item[2].ToString(), RomanNumMath.Plus(ron1, ron2).ToString());
            }
        }
    }
}