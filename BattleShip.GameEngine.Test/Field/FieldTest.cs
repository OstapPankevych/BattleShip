using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.GameEngineTest.Field
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void InitCorrectData()
        {
            var field = new GameEngine.Field.Field(11);

            Assert.IsTrue(field.Size == 11);
        }

        [TestMethod]
        public void InitIncorrectData()
        {
            var field = new GameEngine.Field.Field(5);

            Assert.IsTrue(field.Size == 10);
        }

        [TestMethod]
        public void InitCellsOfFieldData()
        {
            var field = new GameEngine.Field.Field(5);

            foreach (var x in field)
            {
                Assert.IsNotNull(x);
            }
        }

        [TestMethod]
        public void InitEmptyAllCells()
        {
            var field = new GameEngine.Field.Field(5);

            foreach (var x in field)
            {
                Assert.IsTrue(field.IsCellEmpty(x.Location));
            }
        }

        [TestMethod]
        public void IsFieldRegion()
        {
            var field = new GameEngine.Field.Field(10);

            Assert.IsTrue(field.IsFielRegion(9, 9));
        }

        [TestMethod]
        public void IsNotFieldRegion()
        {
            var field = new GameEngine.Field.Field(10);

            Assert.IsFalse(field.IsFielRegion(10, 10) & field.IsFielRegion(0, -1));
        }
    }
}