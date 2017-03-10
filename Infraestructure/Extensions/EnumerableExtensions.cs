using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enum;

namespace Infraestructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static Cell FindIndex(this IEnumerable<Cell> enumerable, int index)
        {
            var currentIndex = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (index.Equals(currentIndex))
                {
                    return enumerator.Current;
                }
                currentIndex++;
            }
            return default(Cell);
        }
        public static Cell FindPoint(this IEnumerable<Cell> enumerable, Point point)
        {
            var matrix = enumerable as IEnumerable<Cell>;
            if(matrix == null)
            {
                return default(Cell);
            }
            return matrix.FirstOrDefault(x => x.Position.Equals(point));
        }
        public static IEnumerable<Cell> MarkCell(this IEnumerable<Cell> matrix, Cell cell, int number)
        {
            return matrix.Select(x =>
                x.Position.Equals(cell.Position)
                    ? new Cell()
                    {
                        Position = x.Position,
                        ColumnGroup = 0,
                        RowGroup = 0,
                        Filling = Component.Space,
                        Number = number
                    }
                    : new Cell()
                    {
                        Position = x.Position,
                        ColumnGroup = x.ColumnGroup,
                        RowGroup = x.RowGroup,
                        Filling = x.Filling,
                        Number = x.Number
                    }
                );
        }
        public static IEnumerable<Cell> ChooseSide(this IEnumerable<Cell> matrix, Point pointInput, Point pointOutput)
        {
            return matrix.Select(x => new Cell()
            {
                Position = x.Position,
                ColumnGroup = x.ColumnGroup,
                RowGroup = x.RowGroup,
                Filling = x.Position.Equals(pointInput) ? Component.Input : x.Position.Equals(pointOutput) ? Component.OutPut : Component.Sharp,
                Number = x.Number
            });
        }
        public static IEnumerable<Cell> Initialize(this IEnumerable<Cell> enumerable, Dimension size)
        {
            var rangeGroup = GetRangeGroup(size);
            var rangeRow = rangeGroup.RangeRow;
            var rowGroup = 0;
            for (var row = 0; row < size.Rows; row++)
            {
                if (row == rangeRow || row >= size.Rows - 1)
                {
                    rangeRow += rangeRow;
                    rowGroup++;
                }
                var columnGroup = 0;
                var rangeColumn = rangeGroup.RangeColumn;
                for (var column = 0; column < size.Columns; column++)
                {
                    if (column == rangeColumn || column >= size.Columns - 1)
                    {
                        rangeColumn += rangeColumn;
                        columnGroup++;
                    }
                    yield return new Cell()
                    {
                        Position = new Point() { X = column, Y = row },
                        RowGroup = (column == 0 || column == size.Columns - 1 || row == 0 || row == size.Rows - 1) ? -1 : rowGroup,
                        ColumnGroup = (column == 0 || column == size.Columns - 1 || row == 0 || row == size.Rows - 1) ? -1 : columnGroup,
                        Number = -1
                    };
                }
            }
        }

        public static int Length(this IEnumerable<Cell> enumerable)
        {
            var currentIndex = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                currentIndex++;
            }
            return currentIndex;
        }
        private static dynamic GetRangeGroup(Dimension size)
        {
            var rangeRow = Math.Round(Math.Sqrt(size.Rows - 2.0), 0);
            var rangeColumn = Math.Round(Math.Sqrt(size.Columns - 2.0), 0);
            return new { RangeRow = rangeRow, RangeColumn = rangeColumn };
        }
    }
}