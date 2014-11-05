using Game2048.Core;
using Game2048.View;

namespace Game2048.Bot
{
    public interface IGameAi
    {
        Direction Move();
        void AddTile(GridCell cell);
    }
}