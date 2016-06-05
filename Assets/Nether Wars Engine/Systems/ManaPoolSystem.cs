using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class ManaPoolSystem : ISetPool, IReactiveSystem
    {
        private Pool _pool;
        private Group _manapoolGroup;


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

            _manapoolGroup = _pool.GetGroup(Matcher.ManaPool);

        }

        public void Execute(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                if (entity.isResource)
                {
                    Logger.LogEvent("Card played as resource: " + entity.ToString());

                    foreach (Entity playerEntity in _manapoolGroup.GetEntities())
                    {
                        if (playerEntity.player.Id == entity.controller.Id && entity.hasResourceGeneration)
                        {
                            int resourceGenerationAmount = entity.resourceGeneration.Amount;
                            playerEntity.ReplaceManaPool(playerEntity.manaPool.CurrentMana + resourceGenerationAmount, playerEntity.manaPool.MaxMana + resourceGenerationAmount);
                        }
                    }
                }
                else
                {
                    Logger.LogEvent("Card removed from resource: " + entity.ToString());

                    foreach (Entity playerEntity in _manapoolGroup.GetEntities())
                    {
                        if (playerEntity.player.Id == entity.controller.Id && entity.hasResourceGeneration)
                        {
                            int resourceGenerationAmount = entity.resourceGeneration.Amount;
                            playerEntity.ReplaceManaPool(playerEntity.manaPool.CurrentMana, playerEntity.manaPool.MaxMana - resourceGenerationAmount);
                        }
                    }

                }
              
            }
        }
    }
}