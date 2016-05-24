
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
