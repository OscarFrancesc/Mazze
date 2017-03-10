using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Service;
using Domain.Operations;
using Infraestructure.Operations;
using Domain.Entities;

namespace ConsoleApplication2
{
    class Program
    {
        private static void Main(string[] args)
        {
            var container = Register.BuildContainer();
            var key = container.Resolve<IKey>();
            var service = container.Resolve<IServiceMaze>();
            var n = key.ReadKey("Ingrese la dimension ", "N");
            var m = key.ReadKey("Ingrese la dimension ", "M");
            var dimension = new Dimension() { Columns = n, Rows = m };
            service.SetSize(dimension);
            Console.WriteLine(service.GetMaze());
            Console.ReadLine();
        }
    }
}
