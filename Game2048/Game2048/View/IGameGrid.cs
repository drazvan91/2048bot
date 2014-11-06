using System.Collections.Generic;
using Game2048.Core;

namespace Game2048.View
{
    public interface IGameGrid
    {
        bool SetTile(int row, int column, int value);

        void Move(Direction direction);

        List<MoveTransition> GetAllMoveStates();
        double rate();

        GridCell AddRandomTile();
        GridCell AddRightBottomTile();

        void Print();
        List<BaseGameGrid> GetAllRandom();
        BaseGameGrid GetCopy();

        GridCell UpdateFromState(Models.GameState state);
        bool HasGreaterTile(int nextTileTarget);
    }
}