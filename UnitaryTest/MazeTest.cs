using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Operations;
using Infraestructure.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using UnitaryTest.ValuesDefault;

namespace UnitaryTest
{
    [TestClass]
    public class MazeTest
    {
        private MockFactory _factory = new MockFactory();
        private IGenerateNumber _generateNumber;
        private IMove _move;
        private IMaze _maze;
        [TestInitialize]
        public void Initialize()
        {
            _generateNumber = new GenerateNumber();
            _move = new Move();
            _maze = new Maze(_generateNumber, _move);
        }
        [TestMethod]
        public void TestingMazeInitialize()
        {
            //Arrange
            _maze.SetSize(new Dimension() {Rows = 5, Columns = 5});
            //Act
            var mazeInitialize = _maze.Initialize();
            //Assert   
            Assert.IsTrue(mazeInitialize);
        }
        [TestMethod]
        public void TestingMazeInitialize_WhenDimensionNotHaveValues()
        {
            //Arrange
            _maze.SetSize(new Dimension());
            //Act
            var mazeInitialize = _maze.Initialize();
            //Assert   
            Assert.IsFalse(mazeInitialize);
        }
        [TestMethod]
        public void TestingMazeInitialize_WhenDimensionIsNull()
        {
            //Arrange
            _maze.SetSize(null);
            //Act
            var mazeInitialize = _maze.Initialize();
            //Assert   
            Assert.IsFalse(mazeInitialize);
        }

        [TestMethod]
        public void TestingMazeGenerate()
        {
            //Arrange
            _maze.SetSize(new Dimension() {Rows = 5, Columns = 5 });
            _maze.Matrix = MazeValues.GetMatrix();
            //Act
            var mazeGenerate = _maze.Generate();
            //Assert   
            Assert.IsTrue(mazeGenerate);
        }
    }
}
