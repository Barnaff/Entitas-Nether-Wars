using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class EnterBattlefieldSystem : ISetPool, IReactiveSystem
    {
        Pool _pool;

        Group _group;

        public TriggerOnEvent trigger
        {
            get
            {
                return Matcher.AllOf(Matcher.Battlefield).OnEntityAddedOrRemoved();
            }
        }

        public void Execute(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Logger.LogEvent("Entered the battlefield: " + entity.ToString());

                entity.isSummoningSickness = true;
            }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
    }
}