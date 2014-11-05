using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Core
{
    public enum Direction: byte
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public static class DirectionHelper
    {
        private static Position[] vectors;

        static DirectionHelper()
        {
            vectors = new Position[4];
            vectors[0] = new Position(0, -1);
            vectors[1] = new Position(1, 0);
            vectors[2] = new Position(0, 1);
            vectors[3] = new Position(-1, 0);
        }

        public static Position GetDirectionVector(Direction direction)
        {
            return GetDirectionVector((byte)direction);
        }

        public static Position GetDirectionVector(int direction)
        {
            return vectors[direction];
        }
    }
}
