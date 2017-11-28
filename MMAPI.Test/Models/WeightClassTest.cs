using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MMAPI.Models.Test.Models
{
    [TestClass]
    public class WeightClassTest
    {
        #region Equal Override Tests
        [TestMethod]
        [TestCategory("WeightClass_Equal")]
        public void Equal_Default_Default_ShouldBeEqual()
        {
            var wc1 = new WeightClass();
            var wc2 = new WeightClass();

            Assert.IsTrue(wc1 == wc2);
            Assert.IsFalse(wc1 != wc2);
            Assert.IsTrue(wc1.Equals(wc2));
        }

        [TestMethod]
        [TestCategory("WeightClass_Equal")]
        public void Equal_1_Default_ShouldNotBeEqual()
        {
            var wc1 = new WeightClass() { UpperWeightLimit = 1 };
            var wc2 = new WeightClass();

            Assert.IsTrue(wc1 != wc2);
            Assert.IsFalse(wc1 == wc2);
            Assert.IsFalse(wc1.Equals(wc2));
        }

        [TestMethod]
        [TestCategory("WeightClass_Equal")]
        public void Equal_Default_Null_ShouldNotBeEqual()
        {
            var wc1 = new WeightClass();
            WeightClass wc2 = null;

            Assert.IsTrue(wc1 != wc2);
            Assert.IsFalse(wc1 == wc2);
            Assert.IsFalse(wc1.Equals(wc2));
        }
        #endregion
    }
}
