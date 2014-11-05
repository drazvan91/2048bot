using Game2048.Bot;
using Game2048.Core;
using Game2048.Utils;
using Game2048.View;
using NUnit.Framework;

namespace Game2048.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void LeftBottomTests()
        {
            var gameView = new GameGrid();

            gameView.SetTile(0, 0, 1);
            gameView.SetTile(1, 0, 1);

            var ai = new GameAi(gameView);

            DebugOutputLogger logger = new DebugOutputLogger();

            for (int i = 0; i < 100; i++)
            {
                Direction direction = ai.Move();
                logger.WriteLine("{0}. {1}", i, direction);

                gameView.Move(direction);
                GridCell cell = gameView.AddRandomTile(); //.AddRightBottomTile();
                
                Assert.IsNotNull(cell, "Game was over");

                ai.AddTile(cell);

                Assert.IsNotNull(cell);
            }

            gameView.Print();
        }
    }
}