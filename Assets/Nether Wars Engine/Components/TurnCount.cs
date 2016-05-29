using Entitas;
using Entitas.CodeGenerator;

namespace NetherWars
{
    [SingleEntity]
    public class TurnCount : IComponent
    {
        public int Value;
    }
}
