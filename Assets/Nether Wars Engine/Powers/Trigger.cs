using System.Collections.Generic;
using System.Collections;
using System;

namespace NetherWars.Powers
{

    [System.Serializable]
    public enum eZoneType
    {
        Hand,
        Battlefield,
    }

    [System.Serializable]
    public abstract class TriggerAbstract
    {
        public string TriggerDisplayName;

		public abstract Dictionary <string, Type> Fields { get; }
    }

    public enum eTriggerType
    {
        None,
        ChangedZone, 
        DealDamage,
    }

    [System.Serializable]
    public class ChangedZoneTrigger : TriggerAbstract
    {
        public eZoneType FromZone;

        public eZoneType ToZone;

        public Target ValidTarget;

		public override Dictionary<string, Type> Fields {
			get {
				Dictionary<string, Type> fields = new Dictionary<string, Type>();
				fields.Add("target", typeof(Target));
				fields.Add("zone", typeof(eZoneType));
				return fields;
			}
		}
    }

    [System.Serializable]
    public class DealDamageTrigger : TriggerAbstract
    {
        public Target ValidAttacker;

        public Target ValidTarget;

        public eZoneType ValidZone;

        public bool RequireCombatDamage;

        public int MinRequireDamage;
    }
}
