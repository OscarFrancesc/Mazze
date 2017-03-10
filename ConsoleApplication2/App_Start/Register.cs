using Autofac;
using Domain.Operations;
using Infraestructure.Operations;
using Service;

namespace ConsoleApplication2
{
    public class Register
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Key>().As<IKey>();
            builder.RegisterType<GenerateNumber>().As<IGenerateNumber>();
            builder.RegisterType<Move>().As<IMove>();
            builder.RegisterType<Maze>().As<IMaze>();
            builder.RegisterType<ServiceMaze>().As<IServiceMaze>();
            return builder.Build();
        }
    }
}
