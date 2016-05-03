using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class PassTurnPahseSystem : ISetPool, IReactiveSystem
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
            if (_pool.turnPhase.Phase == TurnPhase.eTurnPhase.PassTurn)
            {
                Entity[] players = _pool.GetEntities(Matcher.Player);
                foreach (Entity player in players)
                {
                    player.isActivePlayer = !player.isActivePlayer;
                }

                // go to the next phase
                _pool.ReplaceTurnPhase(TurnPhase.eTurnPhase.Upkeep);
            }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
    }
}