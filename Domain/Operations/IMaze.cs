using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Operations
{
    public interface IMaze
    {
        IEnumerable<Cell> Matrix { get; set; }
        void SetSize(Dimension dimension);
        bool Initialize();
        bool Generate();
        bool Resolve();
        string GetMaze();
    }
}
