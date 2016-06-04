using Entitas;
using System.Collections.Generic;

namespace NetherWars
{
    public class ResourceGeneration : IComponent
    {
        public Dictionary<eColorType, int> Thrashold;

        public int Amount;
    }
}

