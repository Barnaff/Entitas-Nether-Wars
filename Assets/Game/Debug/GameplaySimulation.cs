﻿using UnityEngine;
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


        GUILayout.BeginHorizontal("Box");

        Entity[] cardsInHand = Pools.pool.GetEntities(Matcher.AllOf(Matcher.Hand, Matcher.Card, Matcher.Controller));

        List<Entity> cardsInPlayerHands = new List<Entity>();

        for (int i=0; i < cardsInHand.Length; i++)
        {
            if (cardsInHand[i].controller.Id == currentPlayer.player.Id)
            {
                cardsInPlayerHands.Add(cardsInHand[i]);
            }
        }

        for (int i =0; i < cardsInPlayerHands.Count; i++)
        {
            GUILayout.BeginVertical("Box");

            GUILayout.Label("Card " + cardsInPlayerHands[i].card.CardID);

            if (GUILayout.Button("Cast"))
            {

            }

            if (GUILayout.Button("Resource"))
            {
                PlayAsResource(cardsInPlayerHands[i]);
            }

            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();


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

    private void CaesCard(Entity card)
    {

    }

    #endregion
}
