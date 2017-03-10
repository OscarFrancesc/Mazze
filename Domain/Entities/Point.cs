namespace Domain.Entities
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null && (X == point.X && Y == point.Y);
        }
    }
}
