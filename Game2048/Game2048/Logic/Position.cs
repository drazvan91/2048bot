namespace Game2048.Core
{
    public class Position
    {
        public Position()
        {
        }

        public Position(int row, int column)
        {
            this.Column = column;
            this.Row = row;
        }

        public int Column { get; set; }
        public int Row { get; set; }
    }
}