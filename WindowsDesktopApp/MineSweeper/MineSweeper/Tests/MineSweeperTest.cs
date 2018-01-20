using NUnit.Framework;
namespace MineSweeper
{
	[TestFixture]
	class MineSweeperTest
	{
		[Test]
		public void TestDefaultConstructor()
		{
			MineSweeper mineSweeper = new MineSweeper();
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(5));
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(5));
		}

		[Test]
		public void TestSquareConstructor()
		{
			MineSweeper mineSweeper = new MineSweeper(3);
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(3));
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(3));

			mineSweeper = new MineSweeper(4);
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(4));
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(4));

			mineSweeper = new MineSweeper(10);
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(10));
			Assert.That(mineSweeper.FieldHeight, Is.EqualTo(10));
		}
	}
}
