using Entitas;
using Entitas.CodeGenerator;

namespace NetherWars
{
    [SingleEntity]
    public class TurnPhase : IComponent
    {
       public enum eTurnPhase
        {
            Upkeep,
            Draw,
            Main,
            End,
            PassTurn,
        }

        public eTurnPhase Phase;
    }
}
