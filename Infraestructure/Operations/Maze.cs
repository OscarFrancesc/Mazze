using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Constants;
using Domain.Entities;
using Domain.Enum;
using Domain.Operations;
using Infraestructure.Extensions;

namespace Infraestructure.Operations
{
    public class Maze : IMaze
    {
        private readonly IGenerateNumber _generateNumber;
        private readonly IMove _move;
        private Dimension _size;
        private IEnumerable<Cell> _matrix;
        public Maze(IGenerateNumber generateNumber, IMove move)
        {
            _generateNumber = generateNumber;
            _move = move;
        }
        public void SetSize(Dimension dimension)
        {
            _size = dimension ?? new Dimension();
        }
        public IEnumerable<Cell> Matrix {
            get { return _matrix; }
            set { _matrix = value; }
        }
        public bool Initialize()
        {
            _matrix = _matrix.Initialize(_size);
            if (_matrix.Length() > 0 || _matrix == null)
            {
                return true;
            }
            return false;
        }
        public bool Generate()
        {
            var pointsDoors = GetDoors();
            var pointInput = pointsDoors.Input.PointDoor;
            var pointOutput = pointsDoors.Output.PointDoor;
            var nextLeft = new Cell() { Position = new Point() { X = pointInput.X, Y = pointInput.Y } };
            var nextRight = new Cell() { Position = new Point() { X = pointOutput.X, Y = pointOutput.Y } };
            //
            //algoritmo de generacion while - Pendiente de finalizar
            ChooseSide(pointInput, pointOutput);
            BeginMove(ref nextLeft, pointsDoors.Input.Side);
            BeginMove(ref nextRight, pointsDoors.Output.Side);
            _matrix = _matrix.MarkCell(nextLeft, 1);
            _matrix = _matrix.MarkCell(nextRight, 2);
            return true;
        }
        public bool Resolve()
        {
            //Algoritmo de Dijkstra
            return true;
        }
        public string GetMaze()
        {
            var matrixString = new StringBuilder();
            for (var i = 0; i < _size.Rows; i++)
            {
                for (var j = 0; j < _size.Columns; j++)
                {
                    matrixString.Append(_matrix.FindPoint(new Point() { X = j, Y = i }));
                }
                matrixString.Append("\n");
            }
            return matrixString.ToString();
        }
        #region Private Methods
        private Point GetPointDoor(int side)
        {
            var pointDoor = new Point() { X = 0, Y = 0 };
            switch (side)
            {
                case 0:
                    {
                        pointDoor.X = _generateNumber.Next(1, _size.Columns - 1);
                    }
                    break;
                case 1:
                    {
                        pointDoor.X = _size.Columns - 1;
                        pointDoor.Y = _generateNumber.Next(1, _size.Rows - 1);
                    }
                    break;
                case 2:
                    {
                        pointDoor.X = _generateNumber.Next(1, _size.Columns - 1);
                        pointDoor.Y = _size.Rows - 1;
                    }
                    break;
                case 3:
                    {
                        pointDoor.Y = _generateNumber.Next(1, _size.Rows - 1);
                    }
                    break;
            }
            return pointDoor;
        }
        private Cell NextRandom(Cell cell)
        {
            var north = _move.NextPosition(cell, Direction.North, _matrix);
            var west = _move.NextPosition(cell, Direction.West, _matrix);
            var south = _move.NextPosition(cell, Direction.South, _matrix);
            var east = _move.NextPosition(cell, Direction.East, _matrix);
            var attachments = _matrix.Where(x =>
                                        (
                                        (x.Position.Equals(north.Position) && (north.RowGroup != -1 && north.ColumnGroup != -1)) ||
                                        (x.Position.Equals(west.Position) && (west.RowGroup != -1 && west.ColumnGroup != -1)) ||
                                        (x.Position.Equals(south.Position) && (south.RowGroup != -1 && south.ColumnGroup != -1)) ||
                                        (x.Position.Equals(east.Position) && (east.RowGroup != -1 && east.ColumnGroup != -1)))
                                        ).OrderBy(x => x.Number);
            var indexRandom = _generateNumber.Next(attachments.Count());
            return attachments.FindIndex(indexRandom);
        }
        private void BeginMove(ref Cell cell, int side)
        {
            switch (side)
            {
                case 0:
                    cell = _move.NextPosition(cell, Direction.South);
                    break;
                case 1:
                    cell = _move.NextPosition(cell, Direction.West);
                    break;
                case 2:
                    cell = _move.NextPosition(cell, Direction.North);
                    break;
                case 3:
                    cell = _move.NextPosition(cell, Direction.East);
                    break;
            }
        }
        private void ChooseSide(Point pointInput, Point pointOutput)
        {
            _matrix = _matrix.ChooseSide(pointInput, pointOutput);
        }
        private dynamic GetDoors()
        {
            var sideInput = GetSideInput();
            var sideOuput = GetSideOuput(sideInput);
            return new
            {
                Input = new { PointDoor = GetPointDoor(sideInput), Side = sideInput },
                Output = new { PointDoor = GetPointDoor(sideOuput), Side = sideOuput }
            };
        }
        private int GetSideInput()
        {
            return _generateNumber.Next(Constants.MaxSides);
        }
        private int GetSideOuput(int sideInput)
        {
            var sideOuput = sideInput;
            while (sideOuput == sideInput)
            {
                sideOuput = _generateNumber.Next(Constants.MaxSides);
            }
            return sideOuput;
        }
        #endregion
    }
}
