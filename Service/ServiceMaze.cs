using Domain.Entities;
using Domain.Operations;
using Infraestructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceMaze : IServiceMaze
    {
        private readonly IMaze _maze;
        public ServiceMaze(IMaze maze)
        {
            _maze = maze;
        }
        public bool SetSize(Dimension dimension)
        {
            try
            {
                _maze.SetSize(dimension);
            }
            catch(System.Exception ex)
            {
                //Log ex
                return false;
            }
            return true;
        }
        public string GetMaze()
        {
            var maze = string.Empty; 
            try
            {
                var initialize = _maze.Initialize();
                if (initialize)
                {
                    _maze.Generate();
                    _maze.Resolve();
                    maze = _maze.GetMaze();
                }
            }
            catch (System.Exception ex)
            {
                //Log ex
            }
            return maze;
        }
    }
}
