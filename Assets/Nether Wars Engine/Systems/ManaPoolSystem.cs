using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class ManaPoolSystem : ISetPool, IReactiveSystem
    {
        private Pool _pool;
        private Group _group;

        public TriggerOnEvent trigger
        {
            get
            {
                return Matcher.AllOf(Matcher.Resource).OnEntityAddedOrRemoved();
            }
        }

     
        public void SetPool(Pool pool)
        {
            _pool = pool;

            _group = _pool.GetGroup(Matcher.ManaPool);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Logger.LogEvent("Card played as resource: " + entity.ToString());

                foreach (Entity playerEntity in _group.GetEntities())
                {
                    if (playerEntity.player.Id == entity.controller.Id)
                    {
                        playerEntity.ReplaceManaPool(playerEntity.manaPool.CurrentMana + 1, playerEntity.manaPool.MaxMana + 1);
                    }
                }
            }
        }
    }
}