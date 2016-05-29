using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

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
				return Matcher.AllOf(Matcher.Battlefield, Matcher.Graveyard).OnEntityAddedOrRemoved();
			}
		}

		public void Execute(List<Entity> entities)
		{
			foreach (Entity entity in entities)
			{
				if (entity.hasChangedZoneTrigger)
				{
					
				}
			}
		}

		public void SetPool(Pool pool)
		{
			_pool = pool;
		}
	}
}
