using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class HealthSystem :  IReactiveSystem
    {

        public TriggerOnEvent trigger
        {
            get
            {
                return Matcher.AllOf(Matcher.Damage).OnEntityAddedOrRemoved();
            }
        }

        public void Execute(List<Entity> entities)
        {
            Console.Write("Update GameBoard");

            foreach (Entity e in entities)
            {
                if (e.hasDamage &&  e.health.Value <= e.damage.Value)
                {
                    Logger.LogEvent("entitiy " + e + " is dead");

                    e.isBattlefield = false;
                    e.isGraveyard = true;
                }
            }
        }

        public void Initialize()
        {
          //  throw new NotImplementedException();
        }

        public void SetPool(Pool pool)
        {
           
        }
    }

}