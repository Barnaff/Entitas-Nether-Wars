using Entitas;
using System.Collections.Generic;

namespace NetherWars
{
    public class ManaCost : IComponent
    {
        public int Value;

        public Dictionary<eColorType, int> Thrashold;
    }
}

