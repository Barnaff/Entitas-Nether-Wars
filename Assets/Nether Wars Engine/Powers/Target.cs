using System.Collections.Generic;

namespace NetherWars.Powers
{
	[System.Flags]
    public enum eTargetType
    {
        None,
        This,
        ThisController,
        FriendlyCreature,
        EnemyCreature,
        EnemyPlayer,
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
