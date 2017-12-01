using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MMAPI.Models.Tests.Models
{
    [TestClass]
    public class FighterRecordTest
    {
        #region ToString Override Tests
        
        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_Default_ShouldReturn00()
        {
            var fr = new FighterRecord();

            var result = fr.ToString();

            Assert.AreEqual("0-0", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_1Win0Loss_ShouldReturn10()
        {
            var fr = new FighterRecord
            {
                Wins = 1
            };

            var result = fr.ToString();

            Assert.AreEqual("1-0", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_0Win1Loss_ShouldReturn01()
        {
            var fr = new FighterRecord
            {
                Losses = 1
            };

            var result = fr.ToString();

            Assert.AreEqual("0-1", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_0Win0Loss1Draw_ShouldReturn001()
        {
            var fr = new FighterRecord
            {
                Draws = 1
            };

            var result = fr.ToString();

            Assert.AreEqual("0-0-1", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_0Win0Loss0Draw1NC_ShouldReturn0001()
        {
            var fr = new FighterRecord
            {
                NoContests = 1
            };

            var result = fr.ToString();

            Assert.AreEqual("0-0-0 (1 NC)", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_1Win0Loss1Draw_ShouldReturn101()
        {
            var fr = new FighterRecord
            {
                Wins = 1,
                Draws = 1,
            };

            var result = fr.ToString();

            Assert.AreEqual("1-0-1", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_24Win20Loss5Draw100NC_ShouldReturn24205100()
        {
            var fr = new FighterRecord
            {
                Wins = 24,
                Losses = 20,
                Draws = 5,
                NoContests = 100,
            };

            var result = fr.ToString();

            Assert.AreEqual("24-20-5 (100 NC)", result);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > ToString")]
        public void ToString_MaxInts_ShouldReturnMaxIntResult()
        {
            var fr = new FighterRecord
            {
                Wins = int.MaxValue,
                Losses = int.MaxValue,
                Draws = int.MaxValue,
                NoContests = int.MaxValue
            };

            var result = fr.ToString();

            Assert.AreEqual($"{int.MaxValue}-{int.MaxValue}-{int.MaxValue} ({int.MaxValue} NC)", result);
        }

        #endregion

        #region Equal Override Tests
        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_Default_Default_ShouldBeEqual()
        {
            var fr1 = new FighterRecord();
            var fr2 = new FighterRecord();

            Assert.IsTrue(fr1.Equals(fr2));
            Assert.IsTrue(fr1 == fr2);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_1Win_Default_ShouldNotBeEqual()
        {
            var fr1 = new FighterRecord { Wins = 1 };
            var fr2 = new FighterRecord();

            Assert.IsFalse(fr1.Equals(fr2));
            Assert.IsFalse(fr1 == fr2);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_1Loss_Default_ShouldNotBeEqual()
        {
            var fr1 = new FighterRecord { Losses = 1 };
            var fr2 = new FighterRecord();

            Assert.IsFalse(fr1.Equals(fr2));
            Assert.IsFalse(fr1 == fr2);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_1Draw_Default_ShouldNotBeEqual()
        {
            var fr1 = new FighterRecord { Draws = 1 };
            var fr2 = new FighterRecord();

            Assert.IsFalse(fr1.Equals(fr2));
            Assert.IsFalse(fr1 == fr2);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_1NC_Default_ShouldNotBeEqual()
        {
            var fr1 = new FighterRecord { Draws = 1 };
            var fr2 = new FighterRecord();

            Assert.IsFalse(fr1.Equals(fr2));
            Assert.IsFalse(fr1 == fr2);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_DifferentTypes_ShouldNotBeEqual()
        {
            var fr1 = new FighterRecord();
            object o = new object();

            Assert.IsFalse(fr1.Equals(o));
            Assert.IsFalse(fr1 == o as FighterRecord);
        }

        [TestMethod]
        [TestCategory("Models > FighterRecord > Equal")]
        public void Equal_Default_Null_ShouldBeFalse()
        {
            var fr1 = new FighterRecord();
            FighterRecord fr2 = null;

            Assert.IsFalse(fr1.Equals(fr2));
            Assert.IsFalse(fr1 == fr2);
        }
        #endregion
    }
}
