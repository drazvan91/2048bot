using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Core
{
    public class PositionWithNext: Position
    {
        public Position Next { get; set; }
    }
}
