using Domain.Enum;

namespace Domain.Entities
{
    public class Cell
    {
        public Point Position { get; set; }
        public Component Filling { get; set; }
        public int RowGroup { get; set; }
        public int ColumnGroup { get; set; }
        public int Number { get; set; }
        public override string ToString()
        {
            return ((char)Filling).ToString();
        }
    }
}
