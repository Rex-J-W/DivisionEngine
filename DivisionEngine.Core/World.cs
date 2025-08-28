using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine
{
    public class World
    {
        private int nextEntityId = 0;
        private readonly Dictionary<Type, Dictionary<int, IComponent>> componentStorage = new();



    }
}
