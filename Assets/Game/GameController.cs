using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class GameController : MonoBehaviour {

    Systems _systems;
    Pool _pool;

    void Start()
    {
        _pool = Pools.pool;
        _systems = createSystems(_pool);
        _systems.Initialize();
        
    }

    void Update()
    {
        _systems.Execute();
    }


    Systems createSystems(Pool pool)
    {
#if (UNITY_EDITOR)
        return new DebugSystems()
#else
        return new Systems()
#endif
            .Add(pool.CreateSystem<NetherWars.InitializaeGameSystem>())

            .Add(pool.CreateSystem<NetherWars.UpkeepPhaseSystem>())
            .Add(pool.CreateSystem<NetherWars.DrawPhaseSystem>())
            .Add(pool.CreateSystem<NetherWars.MainPhaseSystem>())
            .Add(pool.CreateSystem<NetherWars.EndPhaseSystem>())
            .Add(pool.CreateSystem<NetherWars.PassTurnPahseSystem>())

            .Add(pool.CreateSystem<NetherWars.DrawSystem>())

            .Add(pool.CreateSystem<NetherWars.HealthSystem>())
        
            .Add(pool.CreateSystem<NetherWars.ManaPoolSystem>())
			.Add(pool.CreateSystem<NetherWars.PlayableSystem>())

			.Add(pool.CreateSystem<NetherWars.ChanedZoneSystem>())

            .Add(pool.CreateSystem<NetherWars.EnterBattlefieldSystem>());
        

    }    

}
