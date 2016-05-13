using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class PlayableSystem : ISetPool, IReactiveSystem
    {
        private Pool _pool;
        private Group _group;

        public TriggerOnEvent trigger
        {
            get
            {
                return Matcher.AllOf(Matcher.ManaPool).OnEntityAddedOrRemoved();
            }
        }


        public void SetPool(Pool pool)
        {
            _pool = pool;

            _group = _pool.GetGroup(Matcher.AllOf(Matcher.Card, Matcher.Hand, Matcher.Controller));
        }

        public void Execute(List<Entity> entities)
        {
            Logger.LogEvent("Update playbale state of cards");

            foreach (Entity playerEntity in entities)
            {
                foreach (Entity card in _group.GetEntities())
                {
                    if (playerEntity.player.Id == card.controller.Id)
                    {
                        if (playerEntity.manaPool.CurrentMana >= card.manaCost.Value)
                        {
                            card.isPlayable = true;
                        }
                        else
                        {
                            card.isPlayable = false;
                        }
                    }
                }
            }
        }
    }
}