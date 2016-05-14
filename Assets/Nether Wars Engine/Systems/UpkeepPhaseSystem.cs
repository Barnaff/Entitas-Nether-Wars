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
                Logger.LogEvent("Upkeep");

                Entity currentPlayer = _pool.activePlayerEntity;

                // get the current player's turn id
                int currentPlayerTurnId = currentPlayer.player.Id;

                // untap all that player cards
                foreach (Entity e in _group.GetEntities())
                {
                    // untap tapped cards
                    if (e.isTapped && e.controller.Id == currentPlayerTurnId)
                    {
                        e.isTapped = false;

                        Logger.LogAction("Untap " + e.card.CardID);
                    }

                    if (e.isSummoningSickness &&  e.controller.Id == currentPlayerTurnId)
                    {
                        e.isSummoningSickness = false;

                        Logger.LogAction("Clear Summoning Sickness " + e.card.CardID);
                    }

                    // do any upkeep stuff
                }

                // reset the resource played per turn for the player.
                currentPlayer.isPlayedResource = false;
                currentPlayer.ReplaceManaPool(currentPlayer.manaPool.MaxMana, currentPlayer.manaPool.MaxMana);

                Logger.LogAction("reset player " + currentPlayer.player.Id + " resources pool");

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
