using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class GameplaySimulation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnGUI()
    {
        Entity currentPlayer = Pools.pool.activePlayerEntity;

        GUILayout.BeginVertical();

        GUILayout.BeginVertical("Box");

        GUILayout.Label("Current Player: " + currentPlayer.player.Id + " : " + currentPlayer.player.Name);


        if (GUILayout.Button("End Turn"))
        {
            EndTurn();
        }

        GUILayout.EndHorizontal();


        Entity[] players = Pools.pool.GetEntities(Matcher.Player);
        Entity[] cardsInHand = Pools.pool.GetEntities(Matcher.AllOf(Matcher.Hand, Matcher.Card, Matcher.Controller));

        foreach (Entity playerEntity in players)
        {
            GUILayout.BeginHorizontal("Box");

            for (int i = 0; i < cardsInHand.Length; i++)
            {
                if (cardsInHand[i].controller.Id == playerEntity.player.Id)
                {
                    if (cardsInHand[i].isPlayable)
                    {
                        GUI.color = Color.green;
                    }

                    GUILayout.BeginVertical("Box");

                    GUILayout.Label("Card " + cardsInHand[i].card.CardID);

                    GUILayout.Label("Cost " + cardsInHand[i].manaCost.Value);

                    if (currentPlayer == playerEntity)
                    {
                        if (GUILayout.Button("Cast"))
                        {
                            CastCard(cardsInHand[i]);
                        }

                        if (GUILayout.Button("Resource"))
                        {
                            PlayAsResource(cardsInHand[i]);
                        }
                    }

                    GUI.color = Color.white;

                    GUILayout.EndVertical();
                }
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }


    #region Actions

    private void EndTurn()
    {
        Pools.pool.ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase.End);
    }

    private void PlayAsResource(Entity card)
    {
        if (Pools.pool.activePlayerEntity.isPlayedResource)
        {
            Debug.LogWarning("Player already played resource this turn!");
        }
        else
        {
            card.isHand = false;
            card.isResource = true;

            Pools.pool.activePlayerEntity.isPlayedResource = true;
        }
    }

    private void CastCard(Entity card)
    {
        if (card.isPlayable)
        {
            card.isHand = false;
            card.isBattlefield = true;

            Entity player = Pools.pool.activePlayerEntity;

            player.ReplaceManaPool(player.manaPool.CurrentMana - card.manaCost.Value, player.manaPool.MaxMana);
        }
    }

    #endregion
}
