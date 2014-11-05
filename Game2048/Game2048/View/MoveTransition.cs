using Game2048.Core;

namespace Game2048.View
{
    public class MoveTransition
    {
        public MoveTransition(IGameGrid next, Direction direction)
        {
            State = next;
            Direction = direction;
        }

        public Direction Direction { get; private set; }

        public IGameGrid State { get; private set; }
    }
}