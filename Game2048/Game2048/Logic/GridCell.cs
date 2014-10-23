namespace Game2048.Core
{
    public class GridCell : Position
    {
        public GridCell()
        {
        }

        public GridCell(int row, int column, int value)
            : base(row, column)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }
}