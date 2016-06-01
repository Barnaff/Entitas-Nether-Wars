using System;
using System.Collections.Generic;
using Entitas;
using NetherWars.Powers;

namespace NetherWars
{
    public class DealDamageSystem : ISetPool, IReactiveSystem
    {
        Pool _pool;

        Group _group;

        public TriggerOnEvent trigger
        {
            get
            {
                return Matcher.AllOf(Matcher.DealDamage).OnEntityAddedOrRemoved();
            }
        }

        public void Execute(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
               

                foreach (Entity triggerCard in _group.GetEntities())
                {
                     
                    DealDamageTrigger trigger = triggerCard.dealDamageTrigger.Trigger;
                    eZoneType zone = GameplayActions.GetCardZone(triggerCard);

                    if (trigger.ValidZone == zone && 
                        ((trigger.RequireCombatDamage && entity.dealDamage.IsCombatDamage) || !trigger.RequireCombatDamage)
                        && trigger.MinRequireDamage <= entity.dealDamage.Amount
                        && GameplayActions.MatchTarget(trigger.ValidAttacker, entity.dealDamage.Dealer, triggerCard)
                        && GameplayActions.MatchTarget(trigger.ValidTarget, entity, triggerCard)
                        )
                        
                    {
                        Dictionary<string, object> pointers = new Dictionary<string, object>();

                        pointers.Add("attacker", entity.dealDamage.Dealer);
                        pointers.Add("target", entity);
                        pointers.Add("zone", zone);
                        pointers.Add("isCombatDamage", entity.dealDamage.IsCombatDamage);
                        pointers.Add("damageDelt", zone);

                        foreach (EffectAbstract effect in trigger.Effects)
                        {
                            GameplayActions.ExecuteEffect(effect, pointers);
                        }

                    }
                }

                if (entity.hasDamage)
                {
                    entity.ReplaceDamage(entity.damage.Value + entity.dealDamage.Amount);
                }
                else
                {
                    entity.AddDamage(entity.dealDamage.Amount);
                }

            }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;

            _group = _pool.GetGroup(Matcher.DealDamageTrigger);
        }
    }
}