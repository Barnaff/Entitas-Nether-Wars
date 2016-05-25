using System.Collections.Generic;

namespace NetherWars.Powers
{
	[System.Flags]
	public enum eTargetType
	{
		None 				= 0,
		ThisCard			= 0xFFFF,
		ThisController 		= 2,
		Any 				= 4,
		AnyPlayer			= 8,
		AnyCreature			= 16,
		Enemy 				= 32,
		EnemyCreature		= 64,
		EnemyPlayer			= 128,
		Friendly			= 256, 
		FriendlyCreature 	= 512,
		FriendlyPlayer		= 1024,
	}

	[System.Serializable]
    public class Target
    {
		public eTargetType ValidTargets;


    }

}
