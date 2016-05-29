using System.Collections.Generic;

namespace NetherWars.Powers
{
	[System.Flags]
	public enum eTargetType
	{
		None 				= 0x000,
        This			    = 0x001,
        ThisController 		= 0x002,
        FriendlyCreature    = 0x004,
        EnemyCreature       = 0x008,
        EnemyPlayer         = 0x010,
        Friendly            = (FriendlyCreature | ThisController),
        AnyPlayer			= (ThisController | EnemyPlayer),
		AnyCreature			= (EnemyCreature |FriendlyCreature),
		Enemy 				= (EnemyCreature | EnemyPlayer),
        Any                 = (Friendly | Enemy)
    }

	[System.Serializable]
    public class Target
    {
		public eTargetType ValidTargets;


    }

}
