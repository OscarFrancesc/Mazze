using System.Collections.Generic;
using Domain.Entities;
using Domain.Enum;

namespace Domain.Operations
{
    public interface IMove
    {
        Cell NextPosition(Cell currentPoint, Direction direction);
        Cell NextPosition(Cell currentPoint, Direction direction, IEnumerable<Cell> cells);
    }
}
