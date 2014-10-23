using Game2048.Core;

namespace Game2048.Tests
{
    public class MoveTransition
    {
        public MoveTransition(GameGrid next, Direction direction)
        {
            this.State = next;
            this.Direction = direction;
        }

        public Direction Direction { get; private set; }

        public GameGrid State { get; private set; }
    }
}