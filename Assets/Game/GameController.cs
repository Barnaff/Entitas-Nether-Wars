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


    private void EndTurnAction()
    {
        _pool.ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase.End);
    }

    private void PlayFirstCardAsResource()
    {
        Entity activePlayer = _pool.activePlayerEntity;

        Entity[] cardsInHands = _pool.GetEntities(Matcher.AllOf(Matcher.Card, Matcher.Hand, Matcher.Controller));

        Entity firstCardInhand = null;
        for (int i=0; i< cardsInHands.Length; i++)
        {
            if (cardsInHands[i].controller.Id == activePlayer.player.Id)
            {
                firstCardInhand = cardsInHands[i];
                break;
            }
        }

        if (firstCardInhand != null)
        {
            if (activePlayer.isPlayedResource)
            {
                Debug.LogWarning("Player already played resource this turn!");
            }
            else
            {
                firstCardInhand.isHand = false;

                firstCardInhand.isResource = true;

                activePlayer.isPlayedResource = true;
            }
           
        }
        else
        {
            Debug.LogError("No more cards in hand");
        }

    }


    private string actionString = "";

    /*
    void OnGUI()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("End Turn"))
        {
            EndTurnAction();
        }

        if (GUILayout.Button("Play first card as resource"))
        {
            PlayFirstCardAsResource();
        }

        actionString = GUILayout.TextField(actionString);

        if (GUILayout.Button("Execute Action"))
        {
            NetherWars.ActionResolver.ExecuteAction(actionString);
        }

        GUILayout.EndVertical();
    }
    */
}
