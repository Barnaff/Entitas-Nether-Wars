using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class EndPhaseSystem : ISetPool, IReactiveSystem
    {
        Pool _pool;

        Group _group;

        public TriggerOnEvent trigger
        {
            get
            {
                return Matcher.AllOf(Matcher.TurnPhase).OnEntityAddedOrRemoved();
            }
        }

        public void Execute(List<Entity> entities)
        {
            if (_pool.turnPhase.Phase == TurnPhase.eTurnPhase.End)
            {
                // end the turn.

                // do cleanup and other end phase stuff

                // go to the next phase
                _pool.ReplaceTurnPhase(_pool.turnPhase.Phase + 1);
            }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
    }
}