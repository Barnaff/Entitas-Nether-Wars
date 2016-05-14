using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class HealthSystem : ISetPool, IInitializeSystem
    {
        Pool _pool;
        Group _healthElements;

      

        public void Execute(List<Entity> entities)
        {
            Console.Write("Update GameBoard");

            foreach (Entity e in _healthElements.GetEntities())
            {
                if (e.health.Value <= 0)
                {
                    Console.Write("entitiy " + e + " is dead");
                }
            }
        }

        public void Initialize()
        {
          //  throw new NotImplementedException();
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _healthElements = _pool.GetGroup(Matcher.AllOf(Matcher.Health));
        }
    }

}