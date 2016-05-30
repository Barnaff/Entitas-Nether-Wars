using System.Collections.Generic;

namespace NetherWars.Powers
{
	[System.Flags]
    public enum eTargetType
    {
        This = 0x0001,
        ThisController = 0x0002,
        FriendlyCreature = 0x0004,
        EnemyCreature = 0x0008,
        EnemyPlayer = 0x0010,
        Friendly = (FriendlyCreature | ThisController),
        AnyPlayer = (ThisController | EnemyPlayer),
        AnyCreature = (EnemyCreature | FriendlyCreature),
        Enemy = (EnemyCreature | EnemyPlayer),
        Any = (Friendly | Enemy)
    }


	[System.Serializable]
    public class Target
    {
		public eTargetType ValidTargets;


    }

}
