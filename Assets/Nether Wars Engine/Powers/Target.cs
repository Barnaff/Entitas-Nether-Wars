using System.Collections.Generic;

namespace NetherWars.Powers
{
	[System.Flags]
    public enum eTargetType
    {
        This                = 1,
        ThisController      = 1 << 1,
        FriendlyCreature    = 1 << 2,
        EnemyCreature       = 1 << 3,
        EnemyPlayer         = 1 << 4,
    }


	[System.Serializable]
    public class Target
    {
		public eTargetType ValidTargets;

    }

}
