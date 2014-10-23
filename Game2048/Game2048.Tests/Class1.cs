using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Core;
using NUnit.Framework;
using Moq;

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


            for (int i = 0; i < 10; i++)
            {
                var direction = ai.Move();
                
                gameView.Move(direction);
                GridCell cell = gameView.AddRandomTile();

                ai.AddTile(cell);

                Assert.IsNotNull(cell);
            }
        }
    }
}
