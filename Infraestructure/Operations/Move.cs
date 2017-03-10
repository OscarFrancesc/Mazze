using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enum;
using Domain.Operations;

namespace Infraestructure.Operations
{
    public class Move : IMove
    {
        public Cell NextPosition(Cell currentPoint, Direction direction)
        {
            var cell = new Cell()
            {
                Position = new Point() { X = currentPoint.Position.X, Y = currentPoint.Position.Y },
            };
            switch (direction)
            {
                case Direction.North:
                    {
                        cell.Position.Y = cell.Position.Y - 1;
                    }
                    break;
                case Direction.East:
                    {
                        cell.Position.X = cell.Position.X + 1;
                    }
                    break;
                case Direction.South:
                    {
                        cell.Position.Y = cell.Position.Y + 1;
                    }
                    break;
                case Direction.West:
                    {
                        cell.Position.X = cell.Position.X - 1;
                    }
                    break;
            }

            return cell;
        }
        public Cell NextPosition(Cell currentPoint, Direction direction, IEnumerable<Cell> cells)
        {
            var cell = NextPosition(currentPoint, direction);
            if (cell == null)
            {
                return default(Cell);
            }
            return cells.FirstOrDefault(x => x.Position.Equals(cell.Position)); ;
        }
    }
}
