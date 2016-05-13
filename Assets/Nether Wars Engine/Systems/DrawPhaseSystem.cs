using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class DrawPhaseSystem : ISetPool, IReactiveSystem
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
            if (_pool.turnPhase.Phase == TurnPhase.eTurnPhase.Draw)
            {
                // make the active player draw a card.
                if (_pool.activePlayerEntity.hasDraw)
                {
                    _pool.activePlayerEntity.ReplaceDraw(_pool.activePlayerEntity.draw.CardsToDraw + 1);

                    Logger.LogAction("add draw " + (_pool.activePlayerEntity.draw.CardsToDraw + 1));
                }
                else
                {
                    _pool.activePlayerEntity.AddDraw(1);

                    Logger.LogAction("add draw 1");
                }

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