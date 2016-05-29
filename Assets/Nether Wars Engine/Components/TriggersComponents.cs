using UnityEngine;
using System.Collections;
using Entitas;
using NetherWars.Powers;

namespace NetherWars
{

	public abstract class TriggersComponentAbstract : IComponent
	{
		
	}


	public class ChangedZoneTriggerComponent : TriggersComponentAbstract
	{
		public ChangedZoneTrigger Trigger;
	}


	public class DealDamageTriggerComponent : TriggersComponentAbstract
	{
		public DealDamageTrigger Trigger;
	}
}