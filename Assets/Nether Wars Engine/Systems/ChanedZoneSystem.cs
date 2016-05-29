using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;
using NetherWars.Powers;

namespace NetherWars
{
	public class ChanedZoneSystem : ISetPool, IReactiveSystem 
	{

		Pool _pool;

		Group _group;

		public TriggerOnEvent trigger
		{
			get
			{
				return Matcher.AnyOf(Matcher.Battlefield, Matcher.Graveyard).OnEntityAddedOrRemoved();
			}
		}

		public void Execute(List<Entity> entities)
		{
            foreach (Entity triggerCard in _group.GetEntities())
            {
                ChangedZoneTrigger trigger = triggerCard.changedZoneTrigger.Trigger;
                if (trigger != null)
                {
                    eZoneType zone = GameplayActions.GetCardZone(triggerCard);
                    eZoneType previusZone = eZoneType.None;
                    if (triggerCard.hasPreviusZone)
                    {
                        previusZone = triggerCard.previusZone.Zone;
                    }
                    if (trigger.FromZone == previusZone && trigger.ToZone == zone)
                    {
                        foreach (Entity card in entities)
                        {
                            bool targetMatched = GameplayActions.MatchTarget(trigger.ValidTarget, triggerCard, card);
                            if (targetMatched)
                            {
                                Debug.Log("Execute trigger");
                            }

                        }

                    }
                }
            }

		}

		public void SetPool(Pool pool)
		{
			_pool = pool;

            _group = _pool.GetGroup(Matcher.ChangedZoneTrigger);
        }
	}
}
