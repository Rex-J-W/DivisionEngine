using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine.Components
{
    public struct Transform : IComponent
    {
        public float3 position;
        public float4 rotation;
    }
}
