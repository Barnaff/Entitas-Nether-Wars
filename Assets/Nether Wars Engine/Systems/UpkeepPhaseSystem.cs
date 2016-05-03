using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class UpkeepPhaseSystem : ISetPool, IReactiveSystem
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
            if (_pool.turnPhase.Phase == TurnPhase.eTurnPhase.Upkeep)
            {
                // get the current player's turn id
                int currentPlayerTurnId = _pool.activePlayerEntity.player.Id;

                // untap all that player cards
                foreach (Entity e in _group.GetEntities())
                {
                    // untap tapped cards
                    if (e.isTapped && e.controller.Id == currentPlayerTurnId)
                    {
                        e.isTapped = false;
                    }

                    // do any upkeep stuff
                }

                // go to the next phase
                _pool.ReplaceTurnPhase(_pool.turnPhase.Phase + 1);
            }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;

            _group = _pool.GetGroup(Matcher.Card);
        }
    }
}
